using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

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
        public MinigameEvidence.Quality quality;
        public Color color;
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

    public void AddBaggage(string name, string text, float weight, Vector2 size, MinigameEvidence.Quality quality, Color color)
    {
        Baggage newBaggage = new Baggage
        {
            name = name,
            text = text,
            weight = weight,
            size = size,
            quality = quality,
            color = color
        };

        // Assuming you want to add baggage to the last round, you can modify this logic accordingly.
        if (rounds.Count > 0)
        {
            Round lastRound = rounds[rounds.Count - 1];
            if (lastRound.addBaggage)
            {
                // Instantiate a GameObject based on the baggage data.
                GameObject newBaggageObject = new GameObject(newBaggage.name);
                newBaggageObject.transform.position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            }
        }
    }

}
