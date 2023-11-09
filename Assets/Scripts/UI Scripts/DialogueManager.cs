using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Serializable]
    public struct CharacterPortrait { 
        public string name;
        public char identifier;
        public Sprite[] portraitSprites;
        [HideInInspector] public bool leftSide;
    }
    public enum Emotion
    {
        Neutral,
        Angry,
        Happy,
        Sad,
        Panicked
    }
    public static int GetValueFromEmotion(Emotion emotion)
    {
        return emotion switch
        {
            Emotion.Neutral => 0,
            Emotion.Angry => 1,
            Emotion.Happy => 2,
            Emotion.Sad => 3,
            Emotion.Panicked => 4,
            _ => 0
        };
    }
    private Dictionary<string, Emotion> scriptEmotionShortcuts = new Dictionary<string, Emotion>()
    {   
        { "neu", Emotion.Neutral},
        { "ang", Emotion.Angry},
        { "hap", Emotion.Happy},
        { "sad", Emotion.Sad},
        { "pan", Emotion.Panicked},
    };
    [SerializeField]private DialogueUIManager dialogueUIManager;
    private FlagManager flagManager;
    public List<CharacterPortrait> characterPortraits;

    public TextAsset currentDialogueFile;
    private string dialogueRawText;
    private List<string> dialogueFormatted = new List<string>();

    private void Start()
    {
        flagManager = GetComponent<FlagManager>();
        //LoadDialogue(currentDialogueFile);
    }
    /// <summary>
    /// Loads the text file, formats it, and starts the dialogue. If using path, start the path at the resources folder with no file extension: "Dialogue/TestDialogue"
    /// </summary>
    /// <param name="dialogueFilePath"></param>
    public void LoadDialogue(string dialogueFilePath)
    {
        try
        {
            currentDialogueFile = Resources.Load<TextAsset>(dialogueFilePath);
            print("Loading Dialogue: " + Resources.Load<TextAsset>(dialogueFilePath).name);
        }
        catch
        {
            Debug.LogError("Dialogue File Path Not Valid");
            return;
        }
        dialogueRawText = currentDialogueFile.ToString();
        dialogueFormatted = dialogueRawText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

        dialogueUIManager.gameObject.SetActive(true);
        StartCoroutine(StartDialogue());
    }
    /// <summary>
    /// Loads the text file, formats it, and starts the dialogue
    /// </summary>
    /// <param name="dialogueFile"></param>
    public void LoadDialogue(TextAsset dialogueFile)
    {
        try
        {
            currentDialogueFile = dialogueFile;
            print("Loading Dialogue: " + dialogueFile.name);
        }
        catch
        {
            Debug.LogError("Dialogue File Not Valid");
            return;
        }
        dialogueRawText = currentDialogueFile.ToString();
        dialogueFormatted = dialogueRawText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

        dialogueUIManager.gameObject.SetActive(true);
        StartCoroutine(StartDialogue());
    }
    /// <summary>
    /// Starts the dialogue, going through each line of the script and translating it to speach, emotions, and flags
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartDialogue()
    {
        string[] splitString;
        string[] flagStrings;
        //Going thorugh each line of dialogue
        for (int i = 0; i < dialogueFormatted.Count; i++)
        {
            //This splits between the first part which delaminates between the portrait/speaker identification and the spoken dialogue
            splitString = dialogueFormatted[i].Split(':');
            //Flag Checker & Setter
            if (splitString.Length >= 3)
            {
                flagStrings = splitString[2].Replace(" ", "").Split(',');
                if (!CheckFlags(flagStrings))
                    continue;
                SetFlags(flagStrings);
            }
            string[] characters = splitString[0].Replace(" ", "").Split(',');
            foreach (string s in characters)
            {
                if (dialogueUIManager.characterPortraits.Any(x => x.identifier == s[0]))
                    dialogueUIManager.SetEmotion(scriptEmotionShortcuts[s.Substring(1,3).ToLower()], s[0]);
                else
                {
                    try
                    {
                        dialogueUIManager.SetCharacterPortrait(characterPortraits.Find(x => x.identifier == s[0]), s[4] == 'L');
                    }
                    catch(IndexOutOfRangeException)
                    {
                        dialogueUIManager.SetCharacterPortrait(characterPortraits.Find(x => x.identifier == s[0]));
                    }
                    dialogueUIManager.SetEmotion(scriptEmotionShortcuts[s.Substring(1, 3)], s[0]);
                }
            }
            dialogueUIManager.Speak(splitString[1].Replace("\"", ""), characters[characters.Length-1][0]);
            yield return AdvanceDialogue();
        }
        EndDialogue();
    }
    /// <summary>
    /// Checks for ckecker flags and finds if said flags are in the FlagManager
    /// </summary>
    /// <param name="flagsToCheck"></param>
    /// <returns></returns>
    private bool CheckFlags(string[] flagsToCheck)
    {
        bool flagsFound = true;
        foreach (string flag in flagsToCheck)
        {
            string flagName = flag.Substring(flag.IndexOf("\"")+1).TrimEnd('"');
            if (flag.Contains("check") && flagManager.flags.Contains(flagName))
            {
                flagsFound = true;
                Debug.Log("Found " + flagName + " flag");
            }
            else if (flag.Contains("check") && !flagManager.flags.Contains(flagName))
            {
                flagsFound = false;
                Debug.Log("Did not find " + flagName + " flag");
            }
        }
        return flagsFound;
    }
    /// <summary>
    /// Sets flags in the FlagManager if not already set
    /// </summary>
    /// <param name="flagsToSet"></param>
    private void SetFlags(string[] flagsToSet)
    {
        foreach (string flag in flagsToSet)
        {
            string flagName = flag.Substring(flag.IndexOf("\"")+1).TrimEnd('"');
            if (flag.Contains("set") && !flagManager.flags.Contains(flagName))
            {
                flagManager.flags.Add(flagName);
                Debug.Log("Flag " + flagName + " added to flags");
            }
        }
    }
    /// <summary>
    /// Waits for input to Advance/Skip dialogue
    /// </summary>
    /// <returns></returns>
    private IEnumerator AdvanceDialogue()
    {
        bool buttonPressed = false;
        while (!buttonPressed)
        {
            if (Input.anyKeyDown && dialogueUIManager.hasTextRolledOut == false)
                dialogueUIManager.SkipTypewriterAnimation();
            else if (Input.anyKeyDown && dialogueUIManager.hasTextRolledOut == true)
                buttonPressed = true;
            yield return null;
        }
    }
    /// <summary>
    /// Ends the dialogue
    /// </summary>
    public void EndDialogue()
    {
        dialogueUIManager.characterPortraits.Clear();
        dialogueUIManager.gameObject.SetActive(false);
    }
}
