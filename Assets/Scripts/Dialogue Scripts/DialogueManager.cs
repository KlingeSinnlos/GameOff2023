using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public enum Emotion
	{
		Neutral,
		Angry,
		Happy,
		Sad,
		Panicked
	}

	[SerializeField] private DialogueUIManager dialogueUIManager;
	public List<CharacterPortrait> characterPortraits;

	public TextAsset currentDialogueFile;

	private readonly Dictionary<string, Emotion> scriptEmotionShortcuts = new Dictionary<string, Emotion>()
	{
		{ "neu", Emotion.Neutral },
		{ "ang", Emotion.Angry },
		{ "hap", Emotion.Happy },
		{ "sad", Emotion.Sad },
		{ "pan", Emotion.Panicked },
	};

	private List<string> dialogueFormatted = new List<string>();
	private string dialogueRawText;

	private FlagManager flagManager;

	private void Start()
	{
		flagManager = GetComponent<FlagManager>();
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
	/// <summary>
	/// Loads the current text file, formats it, and starts the dialogue. This method without parameters is mainly for debudding 
	/// </summary>
	[MyBox.ButtonMethod]
	public void LoadDialogue()
	{
		try
		{
			LoadDialogue(currentDialogueFile);
		}
		catch
		{
			Debug.LogError("Current Dialogue File Not Valid");
		}
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
			LoadDialogue(currentDialogueFile);
		}
		catch
		{
			Debug.LogError("Dialogue File Path Not Valid");
		}
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
	/// Starts the dialogue, going through each line of the script and translating it to speech, emotions, and flags
	/// </summary>
	/// <returns></returns>
	private IEnumerator StartDialogue()
	{
		string[] splitString;
		string[] flagStrings;
		// Going through each line of dialogue
		foreach (var dialogue in dialogueFormatted)
		{
			// This splits between the first part which de-laminates between the portrait/speaker identification and the spoken dialogue
			splitString = dialogue.Split(':');
			// Flag Checker & Setter
			if (splitString.Length >= 3)
			{
				flagStrings = splitString[2].Replace(" ", "").Split(',');
				if (!CheckFlags(flagStrings))
					continue;
				SetFlags(flagStrings);
			}

			var characters = splitString[0].Replace(" ", "").Split(',');
			foreach (var character in characters)
			{
				if (dialogueUIManager.characterPortraits.Any(x => x.identifier == character[0]))
					dialogueUIManager.SetEmotion(scriptEmotionShortcuts[character.Substring(1, 3).ToLower()], character[0]);
				else
				{
					try
					{
						dialogueUIManager.SetCharacterPortrait(characterPortraits.Find(x => x.identifier == character[0]),
							character[4] == 'L');
					}
					catch (IndexOutOfRangeException)
					{
						dialogueUIManager.SetCharacterPortrait(characterPortraits.Find(x => x.identifier == character[0]));
					}

					dialogueUIManager.SetEmotion(scriptEmotionShortcuts[character.Substring(1, 3)], character[0]);
				}
			}

			dialogueUIManager.Speak(splitString[1].Replace("\"", ""), characters[^1][0]);
			yield return AdvanceDialogue();
		}

		EndDialogue();
	}

	/// <summary>
	/// Checks for checked flags and finds if said flags are in the FlagManager
	/// </summary>
	/// <param name="flagsToCheck"></param>
	/// <returns></returns>
	private bool CheckFlags(IEnumerable<string> flagsToCheck)
	{
		var flagsFound = true;
		foreach (var flag in flagsToCheck)
		{
			var flagName = flag[(flag.IndexOf("\"", StringComparison.Ordinal) + 1)..].TrimEnd('"');
			if (flag.Contains("check") && flagManager.flags.Contains(flagName))
			{
				flagsFound = true;
				print("Found " + flagName + " flag");
			}
			else if (flag.Contains("check") && !flagManager.flags.Contains(flagName))
			{
				flagsFound = false;
				print("Did not find " + flagName + " flag");
			}
		}

		return flagsFound;
	}

	/// <summary>
	/// Sets flags in the FlagManager if not already set
	/// </summary>
	/// <param name="flagsToSet"></param>
	private void SetFlags(IEnumerable<string> flagsToSet)
	{
		foreach (var flag in flagsToSet)
		{
			var flagName = flag[(flag.IndexOf("\"", StringComparison.Ordinal) + 1)..].TrimEnd('"');
			if (!flag.Contains("set") || flagManager.flags.Contains(flagName)) continue;
			flagManager.flags.Add(flagName);
			print("Flag " + flagName + " added to flags");
		}
	}

	/// <summary>
	/// Waits for input to Advance/Skip dialogue
	/// </summary>
	/// <returns></returns>
	private IEnumerator AdvanceDialogue()
	{
		var buttonPressed = false;
		while (!buttonPressed)
		{
			if (Input.anyKeyDown && !dialogueUIManager.hasTextRolledOut)
				dialogueUIManager.SkipTypewriterAnimation();
			else if(Input.anyKeyDown && dialogueUIManager.hasTextRolledOut)
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

	[Serializable]
	public struct CharacterPortrait
	{
		public string name;
		public char identifier;
		public Sprite[] portraitSprites;
		[HideInInspector] public bool leftSide;
	}
}