using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public InventoryGridView view;
        public string owner;
        public Vector2Int size;
        public InventoryGrid _inventory;
        
        public void Initialize(InventoryService service)
        {
            _inventory = service.RegisterInventory(CreateInventory());
            view.Setup(_inventory);
        }

        public void AddItem(string itemId)
        {
            _inventory.AddItems(itemId, 1);
        }
        
        private InventoryGridData CreateInventory()
        {
            var size = this.size;
            var createdInventorySlots = new List<InventorySlotData>();
            var length = size.x * size.y;
            for (var i = 0; i < length; i++)
            {
                createdInventorySlots.Add(new InventorySlotData());
            }

            var createdInventoryData = new InventoryGridData
            {
                OwnerId = owner,
                Size = size,
                Slots = createdInventorySlots,
            };
            return createdInventoryData;
        }
    }
}