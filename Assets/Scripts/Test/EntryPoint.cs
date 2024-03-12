using System;
using Scripts.Player;
using Scripts.View;
using UnityEngine;

namespace Scripts.Test
{
    public class EntryPoint : MonoBehaviour
    {
        private InventoryService _inventoryService;
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private ScreenView _screenView;
        
        private void Start()
        {
            _inventoryService = new InventoryService();
            _playerInventory.Initialize(_inventoryService);
            
        }
    }
}