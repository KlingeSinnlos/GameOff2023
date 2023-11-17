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
        //print(moveDirection.normalized);
        rb.MovePosition(rb.position + moveDirection.normalized * (speed * Time.fixedDeltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider){

        if(collider.gameObject.CompareTag("Item")){
            Debug.Log("Press E to pick up");
            Item itemScript = collider.gameObject.GetComponent<Item>();
            PressButtonText.text = "Press E to pick up item";
            if(Input.GetKeyDown(KeyCode.E)){
                CollectedItemsValues.Add(itemScript.getValue());
                Debug.Log("Item added ");
            }
        }
        else if(collider.gameObject.CompareTag("Scale")){
            Debug.Log("Press SpaceBar to scale");
            PressButtonText.text = "Press SpaceBar to scale";
            if(Input.GetKeyDown(KeyCode.Space)){
                SceneManager.LoadScene("WeightScaleScene");
            }
        }
        else if(collider.gameObject.CompareTag("Character")){
            Debug.Log("Press Z to interact");
            PressButtonText.text = "Press Z to interact";
            if(Input.GetKeyDown(KeyCode.Z)){
                GameObject.FindGameObjectWithTag("Dialogue UI").SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        PressButtonText.text = "";
    }
}
