using System;
using UnityEngine;

namespace Lite
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private string itemId;
        public string ItemId => itemId;
        public IAbilityItem ability;
        public bool inArm = false;

        private void Start()
        {
            transform.TryGetComponent(out IAbilityItem ability);
            this.ability = ability;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(inArm)
                return;
            if (!other.CompareTag("Player"))
                return;
            if(!other.TryGetComponent(out ArmContainer container))
                return;
            container.inventory.PutItemInInventory(this);
        }
    }
}