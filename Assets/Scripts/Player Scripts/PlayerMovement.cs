using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    private Vector2 moveDirection;
    private PlayerAnimation animations;
    public Rigidbody2D rb;

    void Start() 
    {
        animations = GetComponent<PlayerAnimation>();
    }
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        if (moveDirection.normalized.y > 0.75f)
            animations.ChangeAnimationState(PlayerAnimation.AnimationState.RUNNING_UP);
        else if (moveDirection.normalized.y < -0.75f)
            animations.ChangeAnimationState(PlayerAnimation.AnimationState.IDLE);
        else if (moveDirection.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            animations.ChangeAnimationState(PlayerAnimation.AnimationState.RUNNING_HORIZONTAL);
        }
        else if (moveDirection.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            animations.ChangeAnimationState(PlayerAnimation.AnimationState.RUNNING_HORIZONTAL);
        }
        else
            animations.ChangeAnimationState(PlayerAnimation.AnimationState.IDLE);
    }

    void FixedUpdate()
    {
        print(moveDirection.normalized);
        rb.MovePosition(rb.position + moveDirection.normalized * (speed * Time.fixedDeltaTime));
    }
}
