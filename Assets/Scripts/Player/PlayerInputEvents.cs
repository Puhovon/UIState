using System;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInputEvents : MonoBehaviour
    {
        public Action inventoryClick;
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I)) inventoryClick?.Invoke();
        }
    }
}