using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //temporary also for the crappy test
    [SerializeField] private DialogueUIManager dialogueUIManager;
    [SerializeField] private CharacterPortrait character1;
    [SerializeField] private CharacterPortrait character2;
    
    [Serializable]
    public struct CharacterPortrait { 
        public string name;
        public char identifier;
        public Sprite[] portraitSprites;
        /*[HideInInspector]*/ public bool leftSide;
    }
    public CharacterPortrait[] characterPortraits;
    public List<CharacterPortrait> charactersInScene;

    // most crappy test code ever, please ignore (I will delete it soon)
    private void Start()
    {
        dialogueUIManager.SetCharacterPortrait(character1);
        dialogueUIManager.SetCharacterPortrait(character2);
    }
    private int i;
    private void Update()
    {
        if (!Input.anyKeyDown) return;
        i ++;
        switch (i)
        {
            case 1: 
                dialogueUIManager.SetEmotion(Emotion.Sad, true);
                dialogueUIManager.SetEmotion(Emotion.Surprised, false);
                break;
            case 2: 
                dialogueUIManager.Speak("test1", Emotion.Happy, false);
                break;
            case 3: 
                dialogueUIManager.Speak("test2", Emotion.Angry, true);
                break;
            case 4: 
                dialogueUIManager.Speak("test3", Emotion.Neutral, true);
                break;
            case 5: 
                dialogueUIManager.Speak("test4", Emotion.Neutral, false);
                break;
            case 6: 
                dialogueUIManager.Speak("test3", Emotion.Neutral, true);
                break;
            case 7: 
                dialogueUIManager.SetEmotion(Emotion.Surprised, false);
                break;
            default:
                break;
        }
    }
    // (please remember to delete this crap on top)

    public enum Emotion
    {
        Neutral,
        Angry,
        Happy,
        Sad,
        Surprised
    }

    public static int GetValueFromEmotion(DialogueManager.Emotion emotion)
    {
        return emotion switch
        {
            Emotion.Neutral => 0,
            Emotion.Angry => 1,
            Emotion.Happy => 2,
            Emotion.Sad => 3,
            Emotion.Surprised => 4,
            _ => 0
        };
    }
}
