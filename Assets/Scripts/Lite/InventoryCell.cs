using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Lite
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemId;
        [SerializeField] private ArmContainer arm;
        [SerializeField] private Button btn;

        private Item _item;
        public bool IsEmpty { get; private set; } = true;

        private void Start()
        {
            btn.onClick.AddListener(GetItem);
        }

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
                arm.DropItem();
            
            arm.GetItem(_item);
            _itemId.text = "";
        }
    }
}