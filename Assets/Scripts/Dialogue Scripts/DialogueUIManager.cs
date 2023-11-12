using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private float animationSpeed;
    [SerializeField] private float skipSpeed;
    
    public readonly List<DialogueManager.CharacterPortrait> characterPortraits = new();

    private PortraitUIComponents portraitUIComponentsLeft;
    private PortraitUIComponents portraitUIComponentsRight;
    
    private void Awake()
    {
        var portraitObjectLeft = GameObject.FindWithTag("DialogueUIPortrait");
        var portraitObjectRight = Instantiate(portraitObjectLeft, portraitObjectLeft.transform.parent);
        portraitObjectRight.transform.rotation = Quaternion.Euler(0, 180, 0);
        portraitObjectRight.name = "Right";

        GameObject[] portraitObjects = {portraitObjectLeft, portraitObjectRight};
        var leftSide = true;
        foreach (var portraitObject in portraitObjects)
        {
            var currentPortraitUIComponent = new PortraitUIComponents(
                portraitObject.transform.Find("Dialogue Display").Find("Text").GetComponent<TMP_Text>(),
                portraitObject.transform.Find("Dialogue Display").Find("Name Tag").GetComponent<TMP_Text>(),
                portraitObject.transform.Find("Portrait").GetComponent<Image>(),
                portraitObject.transform.Find("Dialogue Display").gameObject,
                portraitObject.transform.Find("Dialogue Display").Find("Text Box").gameObject,
                portraitObject.transform.Find("Dialogue Display").Find("Text Box").Find("Next Symbol").gameObject);
            if (leftSide) portraitUIComponentsLeft = currentPortraitUIComponent;
            else portraitUIComponentsRight = currentPortraitUIComponent;
            
            leftSide = false;
        }
        
        portraitUIComponentsRight.text.transform.rotation = Quaternion.Euler(0, -180, 0) * Quaternion.Euler(0, 180, 0);
        portraitUIComponentsRight.nameTag.transform.rotation = Quaternion.Euler(0, -180, 0) * Quaternion.Euler(0, 180, 0);
    }

    /// <summary>
    /// assigns a CharacterPortrait to specific UI Elements for the Dialogue on either the left or right side
    /// </summary>
    /// <param name="characterPortrait"></param>
    /// <param name="setLeftSide">declares if the character is portrait on the left or right side, default = false</param>
    public void SetCharacterPortrait(DialogueManager.CharacterPortrait characterPortrait, bool setLeftSide = false)
    {
        if (setLeftSide)
            characterPortrait.leftSide = true;
        var currentPortraitUIComponent = characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        characterPortraits.Add(characterPortrait);
        currentPortraitUIComponent.nameTag.text = characterPortrait.name;
        currentPortraitUIComponent.portraitImage.sprite = characterPortrait.portraitSprites[0];
    }

    /// <summary>
    /// changes the text of the talking dialogue character and manages animations accordingly
    /// </summary>
    /// <param name="text"></param>
    /// <param name="identifier"></param>
    public void Speak(string text, char identifier)
    {
        var characterPortrait = GetCharacterPortraitFromIdentifier(identifier);
        var currentPortraitUIComponent = characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        (!characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight).dialogueDisplay.SetActive(false);
        currentPortraitUIComponent.dialogueDisplay.SetActive(true);
        currentPortraitUIComponent.text.text = text;
        TypewriterAnimation(currentPortraitUIComponent.text);
    }

    /// <summary>
    /// changes the text and emotion of the talking dialogue character and manages animations accordingly
    /// </summary>
    /// <param name="text"></param>
    /// <param name="emotion"></param>
    /// <param name="identifier"></param>
    public void Speak(string text, DialogueManager.Emotion emotion, char identifier)
    {
        Speak(text, identifier);
        SetEmotion(emotion, identifier);
    }

    /// <summary>
    /// changes the emotion of a specific dialogue character and manages animations accordingly
    /// </summary>
    /// <param name="emotion"></param>
    /// <param name="identifier"></param>
    public void SetEmotion(DialogueManager.Emotion emotion, char identifier)
    {
        var characterPortrait = GetCharacterPortraitFromIdentifier(identifier);
        var currentPortraitUIComponent = characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        currentPortraitUIComponent.nameTag.text = characterPortrait.name;
        currentPortraitUIComponent.portraitImage.sprite = characterPortrait.portraitSprites[DialogueManager.GetValueFromEmotion(emotion)];
    }

    private LTDescr typeWriterAnimation;
    public bool hasTextRolledOut { get; private set; }
    
    /// <summary>
    /// rolls out the text faster by a factor of skipSpeed
    /// </summary>
    public void SkipTypewriterAnimation()
    {
        typeWriterAnimation.setTime(typeWriterAnimation.time / skipSpeed);
    }

    private void TypewriterAnimation(TMP_Text text)
    {
        text.maxVisibleCharacters = 0;
        hasTextRolledOut = false;
        typeWriterAnimation = LeanTween.value(0, text.text.Length, text.text.Length * 0.05f / animationSpeed)
            .setOnUpdate(length => text.maxVisibleCharacters = (int)length).setOnComplete(_ => hasTextRolledOut = true);
    }

    public DialogueManager.CharacterPortrait GetCharacterPortraitFromIdentifier(char identifier)
    {
        return characterPortraits.Find(characterPortrait => characterPortrait.identifier == identifier);
    }
    
    private class PortraitUIComponents
    {
        public readonly TMP_Text text;
        public readonly TMP_Text nameTag;
        public readonly Image portraitImage;
        public readonly GameObject dialogueDisplay;
        public readonly GameObject textBox;
        public readonly GameObject nextArrow;

        public PortraitUIComponents(TMP_Text text, TMP_Text nameTag, Image portraitImage, GameObject dialogueDisplay, GameObject textBox, GameObject nextArrow)
        {
            this.text = text;
            this.nameTag = nameTag;
            this.portraitImage = portraitImage;
            this.dialogueDisplay = dialogueDisplay;
            this.textBox = textBox;
            this.nextArrow = nextArrow;
        }
    }
}
