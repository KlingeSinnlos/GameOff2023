using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlate : MonoBehaviour
{
    [MyBox.ReadOnly] public Collider2D[] itemsOnScale;
    [MyBox.ReadOnly] public int numberOfItems = 0;

    // Update is called once per frame
    void Update()
    {
        itemsOnScale = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y-0.5f), new Vector2(2.5f, 2.5f), transform.rotation.z, LayerMask.GetMask("Item"));
        numberOfItems = itemsOnScale.Length;
    }
}
