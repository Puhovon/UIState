using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        public event Action<Vector2Int> SizeChanged;

        public string OwnerId => _data.OwnerId;

        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                {
                    _data.Size = value;
                    SizeChanged?.Invoke(value);
                }
            }
        }

        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;

            var size = data.Size;
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    int index = i * size.y + j;
                    var slotData = data.Slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);
                    _slotsMap[position] = slot;
                }
            }
        }

        public AddItemsToInventoryGridResult AddItems(string itemId, int amout = 1)
        {
            var remainingAmount = amout;
            var itemsAddedToSlotsWithSameItemsAmount =
                AddToSlotWithSameItems(itemId, remainingAmount, out remainingAmount);
            if (remainingAmount <= 0)
            {
                return new AddItemsToInventoryGridResult(OwnerId, amout, itemsAddedToSlotsWithSameItemsAmount);
            }

            var itemsAddedToAvailableSlotsAmount =
                AddToFirstAvailableSlots(itemId, remainingAmount, out remainingAmount);
            var totalAddedItemsAmount = itemsAddedToAvailableSlotsAmount += itemsAddedToSlotsWithSameItemsAmount;
            return new AddItemsToInventoryGridResult(OwnerId, amout, totalAddedItemsAmount);
        }


        public AddItemsToInventoryGridResult AddItems(Vector2Int slotPosition, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotPosition];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty)
            {
                slot.ItemId = itemId;
            }

            var itemSlotCapacity = GetItemSlotCapacity(itemId);
            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemId, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
            }

            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
        }

        public RemoveItemsFromInventoryGrid RemoveItems(string itemId, int amount = 1)
        {
            if (!Has(itemId, amount))
            {
                return new RemoveItemsFromInventoryGrid(OwnerId, amount, false);
            }

            var amountToRemove = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var slotPos = new Vector2Int(i, j);
                    var slot = _slotsMap[slotPos];
                    
                    if(slot.ItemId != itemId) continue;

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;
                        RemoveItems(slotPos, itemId, slot.Amount);
                        
                    }
                    else
                    {
                        RemoveItems(slotPos, itemId, amountToRemove);
                        return new RemoveItemsFromInventoryGrid(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception("Something went wrong ;(");
        }

        public RemoveItemsFromInventoryGrid RemoveItems(Vector2Int slotPosition, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotPosition];
            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
            {
                return new RemoveItemsFromInventoryGrid(OwnerId, amount, false);
            }

            slot.Amount -= amount;
            if (slot.Amount == 0)
            {
                slot.ItemId = null;
            }

            return new RemoveItemsFromInventoryGrid(OwnerId, amount, true);
        }

        public int GetAmount(string itemId)
        {
            var amount = 0;
            var slots = _data.Slots;

            foreach (var slot in slots)
            {
                if (slot.itemId == itemId)
                {
                    amount += slot.Amount;
                }
            }

            return amount;
        }

        public bool Has(string itemId, int amount)
        {
            var amountExist = GetAmount(itemId);
            return amountExist >= amount;
        }

        public void SwitchSlots(Vector2Int slotPosA, Vector2Int slotPosB)
        {
            var slotA = _slotsMap[slotPosA];
            var slotB = _slotsMap[slotPosB];
            var tempSlotItemId = slotA.ItemId;
            var tempSlotItemAmount = slotA.Amount;
            slotA.ItemId = slotB.ItemId;
            slotA.Amount = slotB.Amount;
            slotB.ItemId = tempSlotItemId;
            slotB.Amount = tempSlotItemAmount;
        }
        
        public IReadOnlyInventorySlot[,] GetSlots()
        {
            var array = new IReadOnlyInventorySlot[Size.x, Size.y];
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var pos = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[pos];
                }
            }

            return array;
        }

        private int AddToSlotWithSameItems(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var pos = new Vector2Int(i, j);
                    var slot = _slotsMap[pos];

                    if (slot.IsEmpty) continue;

                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);
                    if (slot.Amount >= slotItemCapacity) continue;
                    if (slot.ItemId != itemId) continue;

                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;
                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int AddToFirstAvailableSlots(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;
            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var pos = new Vector2Int(i, j);
                    var slot = _slotsMap[pos];

                    if (!slot.IsEmpty) continue;

                    slot.ItemId = itemId;
                    var newValue = remainingAmount;
                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);
                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int GetItemSlotCapacity(string itemId)
        {
            return 99;
        }
    }
}