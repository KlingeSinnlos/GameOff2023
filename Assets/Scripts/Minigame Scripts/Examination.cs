using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using TMPro;

[CreateAssetMenu(fileName = "Examination", menuName = "ScriptableObjects/Examination", order = 1)]
public class Examination : ScriptableObject
{
    [System.Serializable]
    public struct Baggage 
    {
        public string name;
        public string text;
        public float weight;
        public Vector2 size;
        public ItemData.Quality quality;
        public Color color;
        [HideInInspector] public GameObject gameObject;
        [HideInInspector] public Rigidbody2D rigidBody;
        [HideInInspector] public BoxCollider2D collider;
        [HideInInspector] public TextMeshProUGUI textObject;
        [HideInInspector] public SpriteRenderer spriteRenderer;
    }

    [System.Serializable]
    public struct Round
    {
        public float timeLimit;
        [Separator("Adding Baggage")]
        public bool addBaggage;
        [ConditionalField(nameof(addBaggage))] public Baggage[] baggageToBeAdded;
        [Separator("Change Baggage")]
        public bool changeBaggage;
        [ConditionalField(nameof(changeBaggage))] public Baggage[] baggageToBeChanged;
        [Separator("Remove Baggage")]
        public bool removeBaggage;
        [ConditionalField(nameof(removeBaggage))] public string[] baggageToBeRemoved;
    }
    public List<Round> rounds;

    public Baggage AddBaggage(Baggage baggage)
    {
        Baggage newBaggage = baggage;
        newBaggage.gameObject = Instantiate((GameObject)Resources.Load("Prefabs/Baggage", typeof(GameObject)));
        newBaggage.gameObject.name = newBaggage.name;
        newBaggage.rigidBody = newBaggage.gameObject.GetComponent<Rigidbody2D>();
        newBaggage.collider = newBaggage.gameObject.GetComponent<BoxCollider2D>();
        newBaggage.spriteRenderer = newBaggage.gameObject.GetComponent<SpriteRenderer>();
        newBaggage.textObject = newBaggage.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        newBaggage.textObject.text = newBaggage.text;
        newBaggage.textObject.gameObject.transform.localScale = new Vector3(1 / newBaggage.size.x, 1 / newBaggage.size.y, 1);
        newBaggage.rigidBody.mass = newBaggage.weight / MinigameEvidence.weightRatio;
        newBaggage.gameObject.transform.localScale = (Vector3)newBaggage.size + Vector3.forward;
        newBaggage.spriteRenderer.color = newBaggage.color;
        newBaggage.collider.size = newBaggage.spriteRenderer.bounds.size;
        return newBaggage;
    }

}
