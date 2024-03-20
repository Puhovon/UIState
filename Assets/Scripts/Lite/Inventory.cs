using System;
using System.Collections.Generic;
using Lite;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryCell> cells;

    public void PutItemInInventory(Item item)
    {
        print($"Item {item.ItemId}");
        foreach (var cell in cells)
        {
            if(!cell.IsEmpty) continue;
            cell.PutItem(item);
            print($"item {item.ItemId} added to inventory");
            item.gameObject.SetActive(false);
            return;
        }
    }
}
