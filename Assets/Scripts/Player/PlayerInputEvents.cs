using System;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerInputEvents : MonoBehaviour
    {
        public Action inventoryClick;
        public Action<Vector2> move;
        public Action dropItem;
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.I)) inventoryClick?.Invoke();
            if(Input.GetKeyDown(KeyCode.G)) dropItem?.Invoke();
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            move?.Invoke(new Vector2(vertical, horizontal));
        }
    }
}