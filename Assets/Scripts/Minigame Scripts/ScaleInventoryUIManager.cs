using UnityEngine;

public class ScaleInventoryUIManager : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }

    private bool inventoryOpened;
    private void ToggleInventory()
    {
        if (!inventoryOpened) ShowInventory();
        else HideInventory();
    }

    private void ShowInventory()
    {
        inventoryOpened = true;
    }

    private void HideInventory()
    {
        inventoryOpened = false;
    }
}
