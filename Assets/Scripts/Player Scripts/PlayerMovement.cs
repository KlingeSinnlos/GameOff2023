using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    
    private Vector2 moveDirection;
    private PlayerAnimation animations;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animations = GetComponent<PlayerAnimation>();
    }
    private void Update()
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

    private void FixedUpdate()
    {
        print(moveDirection.normalized);
        rb.MovePosition(rb.position + moveDirection.normalized * (speed * Time.fixedDeltaTime));
    }
}
