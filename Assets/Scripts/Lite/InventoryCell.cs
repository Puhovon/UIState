using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Lite
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemId;
        [SerializeField] private ArmContainer arm;

        private Item _item;
        public bool IsEmpty { get; private set; } = true;
        
        public void PutItem(Item item)
        {
            _item = item;
            IsEmpty = false;
            _itemId.text = _item.ItemId;
        }

        public void GetItem()
        {
            IsEmpty = true;
            if (!arm.IsEmpty)
            {
                arm.GetItem(_item);
            }
        }
    }
}