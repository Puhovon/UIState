namespace Scripts
{
    public struct RemoveItemsFromInventoryGrid
    {
        public readonly string InventoryOwnerId;
        public readonly int ItemsToRemoveAmount;
        public readonly bool Success;

        public RemoveItemsFromInventoryGrid(
            string inventoryOwnerId,
            int itemsToRemoveAmount,
            bool success)
        {
            InventoryOwnerId = inventoryOwnerId;
            ItemsToRemoveAmount = itemsToRemoveAmount;
            Success = success;
        }
    }
}