using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterPortrait { 
        public string name;
        public char identifier;
        public Sprite[] portraitSprites;
        [HideInInspector] public bool leftSide;
        [HideInInspector] public GameObject portraitObject;
    }
    public CharacterPortrait[] characterPortraits;
    public List<CharacterPortrait> charactersInScene;
}
