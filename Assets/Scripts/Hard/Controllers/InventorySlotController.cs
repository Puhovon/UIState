using Scripts.View;

namespace Scripts.Controllers
{
    public class InventorySlotController
    {
        private InventorySlotView _view;
        public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view)
        {
            _view = view;

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;

            view.Title = slot.ItemId;
            view.Amount = slot.Amount;
        }

        private void OnSlotItemAmountChanged(int newAmount)
        {
            _view.Amount = newAmount;
        }

        private void OnSlotItemIdChanged(string newId)
        {
            _view.Title = newId;
        }
    }
}