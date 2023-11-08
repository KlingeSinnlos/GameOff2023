using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private float animationSpeed;
    
    private List<DialogueManager.CharacterPortrait> characterPortraits = new();

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
                portraitObject.transform.Find("Dialogue Display").Find("Text").GetComponent<TextMeshProUGUI>(),
                portraitObject.transform.Find("Dialogue Display").Find("Name Tag").GetComponent<TextMeshProUGUI>(),
                portraitObject.transform.Find("Portrait").GetComponent<Image>(),
                portraitObject.transform.Find("Dialogue Display").gameObject,
                portraitObject.transform.Find("Dialogue Display").Find("Text Box").gameObject,
                portraitObject.transform.Find("Dialogue Display").Find("Text Box").Find("Next Symbol").gameObject);
            if (leftSide) portraitUIComponentsLeft = currentPortraitUIComponent;
            else portraitUIComponentsRight = currentPortraitUIComponent;
            
            leftSide = false;
        }
        
        /*portraitUIComponentsRight.text.transform.rotation = Quaternion.Euler(0, -180, 0);
        portraitUIComponentsRight.nameTag.transform.rotation = Quaternion.Euler(0, -180, 0);*/
    }

    public void SetCharacterPortrait(DialogueManager.CharacterPortrait characterPortrait)
    {
        var currentPortraitUIComponent = characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        characterPortraits.Add(characterPortrait);
        currentPortraitUIComponent.nameTag.text = characterPortrait.name;
        currentPortraitUIComponent.portraitImage.sprite = characterPortrait.portraitSprites[0];
    }

    public void Speak(string text, char identifier)
    {
        var characterPortrait = GetCharacterPortraitFromIdentifier(identifier);
        var currentPortraitUIComponent = characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        (!characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight).dialogueDisplay.SetActive(false);
        currentPortraitUIComponent.dialogueDisplay.SetActive(true);
        currentPortraitUIComponent.text.text = text;
    }

    public void Speak(string text, DialogueManager.Emotion emotion, char identifier)
    {
        Speak(text, identifier);
        SetEmotion(emotion, identifier);
    }

    public void SetEmotion(DialogueManager.Emotion emotion, char identifier)
    {
        var characterPortrait = GetCharacterPortraitFromIdentifier(identifier);
        var currentPortraitUIComponent = characterPortrait.leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        currentPortraitUIComponent.portraitImage.sprite = characterPortrait.portraitSprites[DialogueManager.GetValueFromEmotion(emotion)];
    }
    
    public void SkipAnimation()
    {
    }

    private DialogueManager.CharacterPortrait GetCharacterPortraitFromIdentifier(char identifier)
    {
        return characterPortraits.Find(characterPortrait => characterPortrait.identifier == identifier);
    }
    
    private class PortraitUIComponents
    {
        public TextMeshProUGUI text;
        public TextMeshProUGUI nameTag;
        public Image portraitImage;
        public GameObject dialogueDisplay;
        public GameObject textBox;
        public GameObject nextArrow;

        public PortraitUIComponents(TextMeshProUGUI text, TextMeshProUGUI nameTag, Image portraitImage, GameObject dialogueDisplay, GameObject textBox, GameObject nextArrow)
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
