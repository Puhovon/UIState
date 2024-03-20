using Lite;
using Scripts.Player;
using UnityEngine;

public class ArmContainer : MonoBehaviour
{
    [SerializeField] private Transform itemPlace;
    [SerializeField] private PlayerInputEvents input;

    private Rigidbody itemRb;
    private Collider itemCollider;
    
    public Inventory inventory;
    
    [HideInInspector] public Item Item;
    [HideInInspector] public bool IsEmpty = true;

    private void Start()
    {
        input.dropItem += DropItem;
    }

    public void GetItem(Item item)
    {
        Item = item;
        if (Item.ability != null)
        {
            Item.ability.Interact();
            Destroy(Item.gameObject);
            return;
        }
        Item.inArm = true;
        itemRb = Item.GetComponent<Rigidbody>();
        itemCollider = Item.GetComponent<Collider>();
        itemRb.isKinematic = true;
        itemCollider.isTrigger = true;
        Item.transform.parent = itemPlace;
        Item.transform.position = itemPlace.position;
        Item.gameObject.SetActive(true);
    }

    public void DropItem()
    {
        if(Item == null) return;
        Item.transform.parent = null;
        Item.inArm = false;
    }
}
