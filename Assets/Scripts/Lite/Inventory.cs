using System.Collections.Generic;
using Lite;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryCell> cells;

    private void PutItemInInventory(Item item)
    {
        foreach (var cell in cells)
        {
            if(!cell.IsEmpty) continue;
            cell.PutItem(item);
        }
    }
}
