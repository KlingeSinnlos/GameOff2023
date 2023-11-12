using UnityEngine;

public class ItemInWorld : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    
    public ItemData GetItemData()
    {
        return itemData;
    }
    
    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
    }
}
