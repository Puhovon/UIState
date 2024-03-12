using System;
using Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class Buttons : MonoBehaviour
    {
        [SerializeField] private Button apple;
        [SerializeField] private Button banana;
        [SerializeField] private Button cucamber;
        [SerializeField] private PlayerInventory _playerInventory;
        
        
        private void Start()
        {
            apple.onClick.AddListener(AddBanana);
        }

        private void AddBanana()
        {
            _playerInventory.AddItem("Banana");
            Debug.Log(_playerInventory._inventory.Has("Banana", 1));
        }
    }
}