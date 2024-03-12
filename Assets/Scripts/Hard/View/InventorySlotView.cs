﻿using System;
using TMPro;
using UnityEngine;

namespace Scripts.View
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _amount;
        
        public string Title
        {
            get => _title.text;
            set => _title.text = value;
        }

        public int Amount
        {
            get => Convert.ToInt32(_amount.text);
            set => _amount.text = value == 0 ? "" : value.ToString();
        }
    }
}