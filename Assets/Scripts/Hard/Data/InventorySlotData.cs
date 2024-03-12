using System;
using UnityEngine.Serialization;

namespace Scripts
{
    [Serializable]
    public class InventorySlotData
    {
        public string itemId;
        public int Amount;
    }
}