using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameEvidence : MonoBehaviour
{
    public enum Quality
    {
        Great,
        Good,
        Bad
    }
    public const float WeightRatio = 100.0f;

    public float weight;
    public Vector2 size;
    public Quality quality;
    public Sprite sprite;

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
        rigidBody.mass = weight/WeightRatio;
        gameObject.transform.localScale = new Vector3(size.x, size.y, 1);
        spriteRenderer.sprite = sprite;
        objectCollider.size = sprite.bounds.size;
    }
}
