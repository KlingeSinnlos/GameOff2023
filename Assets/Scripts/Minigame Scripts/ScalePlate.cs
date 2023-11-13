using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlate : MonoBehaviour
{
    [SerializeReference]Collider2D[] itemsOnScale;
    [SerializeReference]int numberOfItems = 0;

    // Update is called once per frame
    void Update()
    {
        itemsOnScale = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y-0.5f), new Vector2(2.5f, 2.5f), transform.rotation.z, LayerMask.GetMask("Items"));
        numberOfItems = itemsOnScale.Length;
    }
}
