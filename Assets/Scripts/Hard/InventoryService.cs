using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class InventoryService
    {
        private readonly Dictionary<string, InventoryGrid> _inventoriesMap = new();

        public InventoryGrid RegisterInventory(InventoryGridData data)
        {
            var inventory = new InventoryGrid(data);
            _inventoriesMap[inventory.OwnerId] = inventory;
            return inventory;
        }
        
        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(itemId, amount);
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId,
            Vector2Int slotPos,
            string itemId,
            int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(slotPos, itemId, amount);
        }

        public RemoveItemsFromInventoryGrid RemoveItems(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(itemId, amount);
        }

        public RemoveItemsFromInventoryGrid RemoveItems(string ownerId,
            Vector2Int slotPos,
            string itemId,
            int amount = 1)
        {

            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(slotPos, itemId, amount);
        }

        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }

        public IReadOnlyInventoryGrid GetInventory(string ownerId)
        {
            return _inventoriesMap[ownerId];
        }
    }
}