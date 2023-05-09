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

    //public GameObject scrollContent;
    // Start is called before the first frame update
    void Start()
    {
        LineRunner();

        contentHeight = content.GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LineRunner()
    {
        if (lineNumber > currentLineBlock.lines.Length - 1)
        {
            FigureNext();
        }
        else
        {
            GameObject line = Instantiate(linePrefab) as GameObject;
            CheckCharacterFormat(line);

            line.transform.SetParent(content.transform, false);
            line.name = "SpeechBubble" + lineNumber.ToString();
            line.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
            currentVisibleSpeech.Add(line);

            

            dialogText = line.GetComponent<Transform>().Find("CharacterDialogText");

            //sets the text
            dialogText.GetComponent<TextMeshProUGUI>().text = currentLineBlock.lines[lineNumber].dialog;
            //dialogText.GetComponent<TextMeshProUGUI>().text = startLineBlock.sceneComponents[0].dialog;

            lineNumber++;

            // EndCheck();

            ResizeContent(line.GetComponent<RectTransform>().sizeDelta.y);

            

           
        }       
    }

    public void FigureNext()
    {
        Debug.Log("LineBlockComplete");

        //if activity is not next runn the decision
        if (currentLineBlock.endActivityBlock == null)
        {
            DecisionRunner();
            
        }
    }

    public void DecisionRunner()
    {
        GameObject decisionBlock = Instantiate(decisionBlockPrefab) as GameObject;
        decisionBlock.transform.SetParent(content.transform, false);
        decisionBlock.name = "Decision" + decisionNumber.ToString();
        decisionBlock.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
        currentVisibleSpeech.Add(decisionBlock);

        ResizeContent(decisionBlock.GetComponent<RectTransform>().sizeDelta.y);

        for (int i = 0; i < currentLineBlock.endDecisionBlock.decisions.Length; i++)
        {
            GameObject decision = Instantiate(decisionPrefab) as GameObject;
            decision.transform.SetParent(decisionBlock.transform.Find("DecisionButtons"), false);
            //decision.name = "Decision"
            decision.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLineBlock.endDecisionBlock.decisions[i].decisionName;
            decision.GetComponent<ButtonClicked>().decisionNumber = i;

            //decision.GetComponent<Button>().OnPointerClick(ButtonClicked);

        }

        decision = true; 
        //for (int i = 0; i < )
        //GameObject decision = Instan.Length;

       
    }

    //Formats Speech bubbles
    public void CheckCharacterFormat(GameObject speech)
    {
        TextMeshProUGUI speechText = speech.GetComponent<Transform>().Find("CharacterDialogText").GetComponent<TextMeshProUGUI>();
        GameObject speechBackground = speech.GetComponent<Transform>().Find("CharacterSpeechBubbleBackground").gameObject;
        
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Description)
        {
            speechBackground.GetComponent<Image>().color = descriptionColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.You)
        {
            speechBackground.GetComponent<Image>().color = youColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Wife)
        {
            speechBackground.GetComponent<Image>().color = wifeColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Boy)
        {
            speechBackground.GetComponent<Image>().color = boyColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Girl)
        {
            speechBackground.GetComponent<Image>().color = girlColor;
        }
        if (currentLineBlock.lines[lineNumber].character == Line.Character.Dog)
        {
            speechBackground.GetComponent<Image>().color = dogColor;
        } 

    }

   /*  //[SerializeField] TextMeshProUGUI _textMeshPro;
    [Header("Type Writer Effect")]
    public string[] stringArray;

    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords; */

  /*  int i = 0; */

  /*  public void EndCheck()
    {
        Debug.Log("???");
        if (i <= stringArray.Length - 1)
        {
        startLineBlock.sceneComponents[0].dialog = stringArray[i];
            StartCoroutine(TextVisible());

        Debug.Log("Working");
        }
    } */

    /* private IEnumerator TextVisible()
    {
        dialogText.GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
        int totalVisibleCharacters = dialogText.GetComponent<TextMeshProUGUI>().textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = totalVisibleCharacters + 1;
            dialogText.GetComponent<TextMeshProUGUI>().maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                Invoke("EndCheck", timeBtwnWords);
                Debug.Log("Ahhhh");
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);


        }
    } */

    void OnNext()
    {
        Debug.Log("NextLine");
        if (!decision)
        {
            LineRunner();
        }
        

    } 

    public void ResizeContent(float heightToAdd)
    {
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, contentHeight + heightToAdd);
        contentHeight = content.GetComponent<RectTransform>().sizeDelta.y;
    }


}
