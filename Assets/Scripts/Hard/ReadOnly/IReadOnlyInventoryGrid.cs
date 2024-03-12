using System;
using UnityEngine;

namespace Scripts
{
    public interface IReadOnlyInventoryGrid : IReadOnlyInventory
    {
        event Action<Vector2Int> SizeChanged;
        
        Vector2Int Size { get; }

        IReadOnlyInventorySlot[,] GetSlots();
    }
}