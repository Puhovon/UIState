using UnityEngine;

namespace Scripts
{
    public class InventoryGridView : MonoBehaviour
    {
        private IReadOnlyInventoryGrid _inventoryGrid;
        public void Setup(IReadOnlyInventoryGrid inventory)
        {
            _inventoryGrid = inventory;
        }
        
    }
}