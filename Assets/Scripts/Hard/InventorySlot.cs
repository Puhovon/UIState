using System;
using Scripts;

namespace Scripts
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        public event Action<string> ItemIdChanged;
        public event Action<int> ItemAmountChanged;

        public string ItemId
        {
            get => _data.itemId;
            set
            {
                if (_data.itemId != value)
                {
                    _data.itemId = value;
                    ItemIdChanged?.Invoke(value);
                }
            }
        }

        public int Amount
        {
            get => _data.Amount;
            set
            {
                if (_data.Amount != value)
                {
                    _data.Amount = value;
                    ItemAmountChanged?.Invoke(value);
                }
            }
        }

        public bool IsEmpty { get => Amount == 0 && string.IsNullOrEmpty(ItemId); }
        
        private readonly InventorySlotData _data;

        public InventorySlot(InventorySlotData data)
        {
            _data = data;
        }
        
    }
}