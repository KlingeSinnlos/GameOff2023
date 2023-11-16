using UnityEngine;

public class MinigameEvidence : MonoBehaviour
{
    public const float weightRatio = 10;

    public ItemData itemData;

    private Rigidbody2D rigidBody;
    private BoxCollider2D objectCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadEvidence();
    }

    public void LoadEvidence()
    {
        rigidBody.mass = itemData.weight / weightRatio;
        gameObject.transform.localScale = (Vector3) itemData.sizeOnScale + Vector3.forward;
        spriteRenderer.sprite = itemData.icon;
        objectCollider.size = itemData.icon.bounds.size;
    }
}
