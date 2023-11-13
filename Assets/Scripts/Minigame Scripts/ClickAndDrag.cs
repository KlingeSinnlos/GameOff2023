using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    private Vector3 worldPosition;
    private Collider2D itemCollider;
    private Rigidbody2D rb;
    private bool itemSelected = false;
    private void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        print(gameObject.name);
    }
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
        try
        {
        if (hitData.rigidbody.gameObject == gameObject && Input.GetMouseButtonDown(0))
            itemSelected = true;
        if (Input.GetMouseButtonUp(0))
            itemSelected = false;
        }
        catch
        {
        }

        if (itemSelected)
        {
            gameObject.transform.position = worldPosition;
            rb.bodyType = RigidbodyType2D.Static;
            itemCollider.isTrigger = true;
            gameObject.layer = 0;
        } else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            itemCollider.isTrigger = false;
            gameObject.layer = 6;
        }
    }
}
