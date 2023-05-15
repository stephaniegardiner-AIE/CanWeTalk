using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class SceneStarter : MonoBehaviour
{
    public LineBlock currentLineBlock;
    public Scene currentScene;
    public bool decision = false;

    [Header("Scene Objects")]
    public GameObject linePrefab;
    public GameObject decisionBlockPrefab;
    public GameObject decisionPrefab;
    public GameObject content;
    public float contentHeight;
    public float xPositionSpeechBubble;
    public float yPositionSpeechBubble;
    public Transform dialogText;
    public Transform dialogBackground;
    public float dialogLinePadding;
    public int lineNumber;
    public int decisionNumber;
    public TextMeshProUGUI currentTextToWrite;

    public int previousLines;

    public List<GameObject> currentVisibleSpeech;

    [Header("Character Colors")]
    public Color descriptionColor;
    public Color youColor;
    public Color wifeColor;
    public Color boyColor;
    public Color girlColor;
    public Color dogColor;

    [Header("Speech Tails")]
    public GameObject speechTailLeft;
    public GameObject speechTailRight;

    [Header("Character Attitudes")]
    public float youAttitudeLevel;
    public float wifeAttitudeLevel;
    public float kidsAttitudeLevel;
    public float dogAttitudeLevel;

    [Header("Attitude Bar")]
    public Image youAttitudeBar;
    public Image wifeAttitudeBar;
    public Image kidsAttitudeBar;
    public Image dogAttitudeBar;

    [Header("Attitude Bar Lerping")]
    public float attitudeLerpDuration;
    private float _valueToLerp;

    [Header("TypeWriter")]
    public float characterTime;

    [Header("CharacterNames")]
    public string wifeName;
    public string youName;
    public string girlName;
    public string boyName;

    //public GameObject scrollContent;
    // Start is called before the first frame update
    void Start()
    {
        LineRunner();
        SetAttitude();

        contentHeight = content.GetComponent<RectTransform>().sizeDelta.y;
    }


    //runs the next line
    public void LineRunner()
    {
        //if it's run out of lines, figure out the next thing to do
        if (lineNumber > currentLineBlock.lines.Length - 1)
        {
            FigureNext();
        }
        //if the lines aren't run out, run the next line
        else
        {
            GameObject line = Instantiate(linePrefab) as GameObject;
            CheckCharacterFormat(line);

            line.transform.SetParent(content.transform, false);
            line.name = "SpeechBubble" + lineNumber.ToString();
            line.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
            currentVisibleSpeech.Add(line);
            
            dialogBackground = line.GetComponent<Transform>();
            dialogText = dialogBackground.Find("CharacterDialogText");

            //sets the text
            dialogText.GetComponent<TextMeshProUGUI>().text = currentLineBlock.lines[lineNumber].dialog;

            NameCheck();

            TypeWriter(dialogText.GetComponent<TextMeshProUGUI>());

            //if the line has an attitude change, now make sure that happens!!!
            if (currentLineBlock.lines[lineNumber].attitudeArrayLength > 0)
            {
                UpdateAttitudes(currentLineBlock.lines[lineNumber]);
            }

                lineNumber++;

            // EndCheck();

            float tailHeight = 0;

            if (line.transform.GetChild(1) != null)
            {
                tailHeight = line.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.y;
            }
            

            ResizeSpeech();

            ResizeContent(line.GetComponent<RectTransform>().sizeDelta.y + tailHeight);

        }       
    }

    public void ResizeSpeech()
    {
        //establishing a bnch of variables
        float dialogTextSizeDeltaWidth = dialogText.GetComponent<RectTransform>().sizeDelta.x;
        float dialogFontSize = dialogText.GetComponent<TextMeshProUGUI>().fontSize;
        float dialogLineCount = dialogText.GetComponent<TextMeshProUGUI>().textInfo.lineCount;


        dialogText.GetComponent<RectTransform>().sizeDelta = new Vector2(dialogTextSizeDeltaWidth + (dialogLinePadding * 2),
            (dialogFontSize * dialogLineCount) + (dialogLinePadding * dialogLineCount) + (dialogLinePadding * 2));

        float dialogTextWidth = dialogTextSizeDeltaWidth + (dialogLinePadding * 2);

        float dialogTextHeight = (dialogFontSize * dialogLineCount) + (dialogLinePadding * dialogLineCount) + (dialogLinePadding * 2);

 
        float dialogBackgroundHeight = dialogBackground.GetComponent<RectTransform>().sizeDelta.y;

        dialogBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(dialogTextWidth + (dialogLinePadding * 4), dialogTextHeight);

        //Debug.Log(dialogBackgroundHeight);
    }

    //the background for the button block
    public void ResizeButtonBlock(GameObject buttonBlock)
    {
        //float buttonBlockHeight = buttonBlock.GetComponent<RectTransform>().sizeDelta.y;

        float buttonBlockWidth = buttonBlock.transform.parent.GetComponent<RectTransform>().sizeDelta.x;

        int thisDialogLinePadding = (int)dialogLinePadding;

        buttonBlock.GetComponent<VerticalLayoutGroup>().padding.left = (thisDialogLinePadding * 3);

        float buttonsHeight = 0;

        for (int i = 0; i < buttonBlock.transform.childCount; i++)
        {
            buttonsHeight += buttonBlock.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
            Debug.Log(buttonsHeight);
        }

        //float buttonTextLine

        buttonBlock.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonBlockWidth, buttonsHeight + (dialogLinePadding * 2));

    }

    //the actual speech bubble
    public void ResizeButtonBlockBackground(GameObject buttonBlockBackground)
    {
        float buttonBlockWidth = (buttonBlockBackground.transform.GetComponent<RectTransform>().sizeDelta.x);
        float buttonBlockHeight = 0;
        
        for (int i = 0; i <buttonBlockBackground.transform.childCount; i++)
        {
            buttonBlockHeight += buttonBlockBackground.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y + (dialogLinePadding * 2);
            
        }

        buttonBlockBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonBlockWidth, buttonBlockHeight);
    }
    public void ResizeButton(GameObject button)
    {
        TextMeshProUGUI buttonText = button.GetComponent<Transform>().Find("DecisionButtonText").GetComponent<TextMeshProUGUI>();
        float buttonTextFontSize = buttonText.fontSize;
        float buttonTextLineCount = buttonText.textInfo.lineCount;

        float buttonWidth = button.GetComponent<RectTransform>().sizeDelta.x;
        float buttonHeight = button.GetComponent<RectTransform>().sizeDelta.y;

        button.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth, 
            (buttonTextFontSize * buttonTextLineCount));

        Debug.Log(buttonTextLineCount);
    }

    public void NameCheck()
    {
        dialogText.GetComponent<TextMeshProUGUI>().text = dialogText.GetComponent<TextMeshProUGUI>().text.Replace("WifeName", wifeName);
        dialogText.GetComponent<TextMeshProUGUI>().text = dialogText.GetComponent<TextMeshProUGUI>().text.Replace("KidBoyName", boyName);
        dialogText.GetComponent<TextMeshProUGUI>().text = dialogText.GetComponent<TextMeshProUGUI>().text.Replace("KidBoyName", boyName);
    }

    //figures out what to run next after a line block is finished
    public void FigureNext()
    {
        Debug.Log("LineBlockComplete");

        //if activity is not next runn the decision
        if (currentLineBlock.endActivityBlock == null)
        {
            DecisionRunner();
            
        }
    }

    //creates a decision block when called
    public void DecisionRunner()
    {
        //instantiates the decision speech block
        GameObject decisionBlock = Instantiate(decisionBlockPrefab) as GameObject;
        decisionBlock.transform.SetParent(content.transform, false);
        decisionBlock.name = "Decision" + decisionNumber.ToString();
        decisionBlock.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
        currentVisibleSpeech.Add(decisionBlock);

        

        

        //makes a decision button appear for each decision in list
        for (int i = 0; i < currentLineBlock.endDecisionBlock.decisions.Length; i++)
        {
            GameObject decision = Instantiate(decisionPrefab) as GameObject;
            decision.transform.SetParent(decisionBlock.transform.Find("DecisionButtons"), false);
            decision.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLineBlock.endDecisionBlock.decisions[i].decisionName;
            //adds type writer effect to button text
            TypeWriter(decision.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            decision.GetComponent<ButtonClicked>().decisionNumber = i;

            //sets the size of teh deicision buttons
            ResizeButton(decision);

            //Debug.Log(decision.transform.GetChild(1).GetComponent<TextMeshProUGUI>().textInfo.lineCount);
        }

        ResizeButtonBlock(decisionBlock.transform.Find("DecisionButtons").gameObject);

        ResizeButtonBlockBackground(decisionBlock);

        //activates the decision state
        decision = true;

        ResizeContent(decisionBlock.GetComponent<RectTransform>().sizeDelta.y);

    }

    //Formats Speech bubbles
    public void CheckCharacterFormat(GameObject speech)
    {
        TextMeshProUGUI speechText = speech.GetComponent<Transform>().Find("CharacterDialogText").GetComponent<TextMeshProUGUI>();
        GameObject speechBackground = speech.GetComponent<Transform>().gameObject;
        
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Description)
        {
            speechBackground.GetComponent<Image>().color = descriptionColor;
            

        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.You)
        {
            speechBackground.GetComponent<Image>().color = youColor;
            GameObject speechTail = Instantiate(speechTailLeft) as GameObject;
            speechTail.transform.SetParent(speechBackground.transform, false);
            speechTail.GetComponent<Image>().color = youColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Wife)
        {
            speechBackground.GetComponent<Image>().color = wifeColor;
            GameObject speechTail = Instantiate(speechTailRight) as GameObject;
            speechTail.transform.SetParent(speechBackground.transform, false);
            speechTail.GetComponent<Image>().color = wifeColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Boy)
        {
            speechBackground.GetComponent<Image>().color = boyColor;
            GameObject speechTail = Instantiate(speechTailRight) as GameObject;
            speechTail.transform.SetParent(speechBackground.transform, false);
            speechTail.GetComponent<Image>().color = boyColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Girl)
        {
            speechBackground.GetComponent<Image>().color = girlColor;
            GameObject speechTail = Instantiate(speechTailRight) as GameObject;
            speechTail.transform.SetParent(speechBackground.transform, false);
            speechTail.GetComponent<Image>().color = girlColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Dog)
        {
            speechBackground.GetComponent<Image>().color = dogColor;
            GameObject speechTail = Instantiate(speechTailRight) as GameObject;
            speechTail.transform.SetParent(speechBackground.transform, false);
            speechTail.GetComponent<Image>().color = dogColor;
        } 

    }

    //public void SpeechTailMaker(GameObject speechBackground,)

    //when the player presses space!
    void OnNext()
    {
        
        //if the decision maker isn't active! run the next line :))))
        if (!decision)
        {
            LineRunner();
            Debug.Log("NextLine");
        }
        

    } 

    //resises the viewscreen content box to properly fit all the speech bubbles
    public void ResizeContent(float heightToAdd)
    {
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, contentHeight + heightToAdd);
        contentHeight = content.GetComponent<RectTransform>().sizeDelta.y;
    }

    //figures out what the attitude array is trying to tell us and then causes fill amount change for teh appropriate attidue bar before changing the the final attitude level
    public void UpdateAttitudes(Line line)
    {

        for (int i = 0; i < line.attitudeArray.Length; i++)

        {
           Debug.Log(line.attitudeArray[i].attitudeChangeEffects);

            //change you attitude
            if (line.attitudeArray[i].attitudeChangeEffects.ToString() == "youAttitude")
            {

                StartCoroutine(AttitudeLerp(youAttitudeLevel, youAttitudeLevel + line.attitudeArray[i].attitudeChangeAmount, youAttitudeBar));
                youAttitudeLevel += line.attitudeArray[i].attitudeChangeAmount;

            }

            //change wife attitude
            if (line.attitudeArray[i].attitudeChangeEffects.ToString() == "wifeAttitude")
            {
                StartCoroutine(AttitudeLerp(wifeAttitudeLevel, wifeAttitudeLevel + line.attitudeArray[i].attitudeChangeAmount, wifeAttitudeBar));
                wifeAttitudeLevel += line.attitudeArray[i].attitudeChangeAmount;

            }

            //change kids attitude
            if (line.attitudeArray[i].attitudeChangeEffects.ToString() == "kidsAttitude")
            {
                StartCoroutine(AttitudeLerp(kidsAttitudeLevel, kidsAttitudeLevel + line.attitudeArray[i].attitudeChangeAmount, kidsAttitudeBar));
                kidsAttitudeLevel += line.attitudeArray[i].attitudeChangeAmount;

            }

            //change dog attitude
            if (line.attitudeArray[i].attitudeChangeEffects.ToString() == "dogAttitude")
            {
                StartCoroutine(AttitudeLerp(dogAttitudeLevel, dogAttitudeLevel + line.attitudeArray[i].attitudeChangeAmount, dogAttitudeBar));
                dogAttitudeLevel += line.attitudeArray[i].attitudeChangeAmount;

            }
        }
    }

    public void ChangeAttitude(float character, float changeAmount, Image attitudeBar)
    {
        character += changeAmount;

        attitudeBar.fillAmount = character / 100;
    }

    public void SetAttitude()
    {
        youAttitudeBar.fillAmount = youAttitudeLevel / 100;
        wifeAttitudeBar.fillAmount = wifeAttitudeLevel / 100;
        kidsAttitudeBar.fillAmount = kidsAttitudeLevel / 100;
        dogAttitudeBar.fillAmount = dogAttitudeLevel / 100;
    }

    IEnumerator AttitudeLerp(float startValue, float endValue, Image attitudeBar)
    {
        float timeElapsed = 0;
        while (timeElapsed < attitudeLerpDuration)
        {
            _valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / attitudeLerpDuration);
            timeElapsed += Time.deltaTime;
            attitudeBar.fillAmount = _valueToLerp / 100;
            yield return null;
        }
        _valueToLerp = endValue;
    }

    public void TypeWriter(TextMeshProUGUI text)
    {
        text.maxVisibleCharacters = 0;
        text.ForceMeshUpdate();
        StartCoroutine(TextVisible(text));
    }

    private IEnumerator TextVisible(TextMeshProUGUI text)
    {
        text.ForceMeshUpdate();

        while (text.textInfo.characterCount >= text.maxVisibleCharacters)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(characterTime);
        }

        text.maxVisibleCharacters = text.textInfo.characterCount;
    }

}
