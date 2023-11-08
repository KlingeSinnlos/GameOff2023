using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //temporary also for the crappy test
    [SerializeField] private DialogueUIManager dialogueUIManager;
    [SerializeField] private CharacterPortrait character1;
    [SerializeField] private CharacterPortrait character2;
    [SerializeField] private CharacterPortrait character3;
    
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
        dialogueUIManager.SetCharacterPortrait(character3);
    }
    private int i;
    private void Update()
    {
        if (!Input.anyKeyDown) return;
        i ++;
        switch (i)
        {
            case 1: 
                dialogueUIManager.SetEmotion(Emotion.Sad, 'E');
                dialogueUIManager.SetEmotion(Emotion.Surprised, 'C');
                break;
            case 2: 
                dialogueUIManager.Speak("test1", Emotion.Happy, 'C');
                break;
            case 3: 
                dialogueUIManager.Speak("test2", Emotion.Angry, 'E');
                break;
            case 4: 
                dialogueUIManager.Speak("test3", Emotion.Neutral, 'B');
                break;
            case 5: 
                dialogueUIManager.Speak("test4", Emotion.Neutral, 'C');
                break;
            case 6: 
                dialogueUIManager.Speak("test3", Emotion.Neutral, 'E');
                break;
            case 7: 
                dialogueUIManager.SetEmotion(Emotion.Surprised, 'C');
                break;
            case 8: 
                dialogueUIManager.Speak("test4", Emotion.Sad, 'B');
                break;
            case 9: 
                dialogueUIManager.Speak("test5", Emotion.Angry, 'E');
                break;
            case 10: 
                dialogueUIManager.SetEmotion(Emotion.Surprised, 'C');
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
