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

    public void AddBaggage()
    {

    }
}
