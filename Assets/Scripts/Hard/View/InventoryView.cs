using TMPro;
using UnityEngine;

namespace Scripts.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private InventorySlotView[] _slots;
        [SerializeField] private TMP_Text _owner;

        public string OwnerId
        {
            get => _owner.text;
            set => _owner.text = value;
        }

        public InventorySlotView GetInventorySlotView(int index)
        {
            return _slots[index];
        }
    }
}