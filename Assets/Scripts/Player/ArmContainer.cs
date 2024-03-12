using System;
using Lite;
using Scripts.Player;
using UnityEngine;

public class ArmContainer : MonoBehaviour
{
    [SerializeField] private Transform itemPlace;

    private Rigidbody itemRb;
    private Collider itemCollider;
    [HideInInspector] public Item Item;
    [HideInInspector] public bool IsEmpty = true;

    public void GetItem(Item item)
    {
        Item = item;
        itemRb = Item.GetComponent<Rigidbody>();
        itemCollider = Item.GetComponent<Collider>();
        itemRb.isKinematic = true;
        itemCollider.isTrigger = true;
        Item.transform.parent = transform;
        Item.transform.position = itemPlace.position;
    }

    private void DropItem()
    {
        Item.transform.parent = null;
        itemRb.isKinematic = false;
        itemCollider.isTrigger = false;
    }
}
