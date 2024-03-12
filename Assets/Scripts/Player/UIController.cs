using System;
using UnityEngine;

namespace Scripts.Player
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject inventory;
        [SerializeField] private PlayerInputEvents _inputEvents;
        private bool isInventoryActive = false;
        private void Start()
        {
            _inputEvents.inventoryClick += OnInvenroryClick;
        }

        private void OnInvenroryClick()
        {
            isInventoryActive = !isInventoryActive;
            inventory.SetActive(isInventoryActive);    
        }
    }
}