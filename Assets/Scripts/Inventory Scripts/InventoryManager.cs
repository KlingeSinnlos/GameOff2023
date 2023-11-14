using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private float pickUpRange;
    
    public readonly List<ItemData> inventory = new();

    private Collider2D itemCollectionTrigger;
    
    private void Awake()
    {
        var circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = pickUpRange;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var item in inventory)
            {
                print(item.displayName);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpItem();
        }
    }
    
    private bool waitingForInput;
    private readonly List<ItemInWorld> itemsInRange = new();
    private ItemInWorld nearestItem;
    private void PickUpItem()
    {
        var shortestDistance = pickUpRange;
        foreach (var item in itemsInRange)
        {
            print(item.GetItemData().displayName);
            var currentDistance = ((Vector2) item.transform.position - (Vector2) transform.position).sqrMagnitude;
            print(currentDistance);
            if (currentDistance > pickUpRange) itemsInRange.Remove(item);
            if (!(currentDistance < shortestDistance)) continue;
            shortestDistance = currentDistance;
            nearestItem = item;
        }
        
        print(inventory.Count);
        print(nearestItem.GetItemData().name);
        inventory.Add(nearestItem.GetItemData());
        Destroy(nearestItem.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<ItemInWorld>();
        if (!item) return;
        itemsInRange.Add(item);
    }
}
