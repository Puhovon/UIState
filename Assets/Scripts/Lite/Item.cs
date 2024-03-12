using UnityEngine;

namespace Lite
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private string itemId;
        public string ItemId => itemId;
    }
}