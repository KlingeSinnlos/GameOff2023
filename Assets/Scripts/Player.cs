using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 4f;
    private Vector2 moveDirection;
    public Rigidbody2D rb;

    void Start() {
        
    }
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection.normalized * (speed * Time.fixedDeltaTime));
    }
}
