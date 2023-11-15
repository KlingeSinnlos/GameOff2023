using System;
using System.Collections.Generic;
using System.Linq;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUpItem();
        }
    }
    
    private bool waitingForInput;
    public List<int> itemsInRange = new();
    private void PickUpItem()
    {
        ItemInWorld nearestItem = null;
        var shortestDistance = pickUpRange;
        var itemsToRemove = new List<int>();
        foreach (var item in itemsInRange.Select(itemInRange => GameObject.Find(itemInRange.ToString()).GetComponent<ItemInWorld>()))
        {
            var currentDistance = ((Vector2) item.transform.position - (Vector2) transform.position).sqrMagnitude;

            if (currentDistance > pickUpRange)
            {
                itemsToRemove.Add(item.GetInstanceID());
            }
            if (!(currentDistance < shortestDistance)) continue;
            shortestDistance = currentDistance;
            nearestItem = item;
        }
        itemsInRange.RemoveAll(itemID => itemsToRemove.Contains(itemID));
        if (nearestItem == null) return;
        
        inventory.Add(nearestItem.GetItemData());
        itemsInRange.Remove(nearestItem.GetInstanceID());
        Destroy(nearestItem.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<ItemInWorld>();
        if (!item) return;
        item.gameObject.name = item.GetInstanceID().ToString();
        if (!itemsInRange.Contains(item.GetInstanceID()))
            itemsInRange.Add(item.GetInstanceID());
    }
}
