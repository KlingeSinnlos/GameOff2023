using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private float animationSpeed;

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
        
        currentPortraitUIComponent.characterPortrait = characterPortrait;
        currentPortraitUIComponent.nameTag.text = characterPortrait.name;
        currentPortraitUIComponent.portraitImage.sprite = characterPortrait.portraitSprites[0];
        
        print(currentPortraitUIComponent.characterPortrait.name);
    }

    public void Speak(string text, DialogueManager.Emotion emotion, bool leftSide)
    {
        var currentPortraitUIComponent = leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        (!leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight).dialogueDisplay.SetActive(false);
        currentPortraitUIComponent.dialogueDisplay.SetActive(true);
        currentPortraitUIComponent.text.text = text;
        currentPortraitUIComponent.portraitImage.sprite = currentPortraitUIComponent.characterPortrait.portraitSprites[DialogueManager.GetValueFromEmotion(emotion)];
    }

    public void SetEmotion(DialogueManager.Emotion emotion, bool leftSide)
    {
        var currentPortraitUIComponent = leftSide ? portraitUIComponentsLeft : portraitUIComponentsRight;
        
        currentPortraitUIComponent.portraitImage.sprite = currentPortraitUIComponent.characterPortrait.portraitSprites[DialogueManager.GetValueFromEmotion(emotion)];
    }
    
    public void SkipAnimation()
    {
    }

    private class PortraitUIComponents
    {
        public TextMeshProUGUI text;
        public TextMeshProUGUI nameTag;
        public Image portraitImage;
        public GameObject dialogueDisplay;
        public GameObject textBox;
        public GameObject nextArrow;
        public DialogueManager.CharacterPortrait characterPortrait;

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
