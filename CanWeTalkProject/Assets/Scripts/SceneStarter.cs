using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SceneStarter : MonoBehaviour
{
    public LineBlock startLineBlock;
    public GameObject characterSpeechBubble;
    public GameObject content;
    public float xPositionSpeechBubble;
    public float yPositionSpeechBubble;
    public GameObject dialogText;
    public float lineNumber;
    public TextMeshProUGUI currentTextToWrite;

    public List<GameObject> currentVisibleLines;
    //public GameObject scrollContent;
    // Start is called before the first frame update
    void Start()
    {
        LineRunner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LineRunner()
    {
        GameObject speechbubble = Instantiate(characterSpeechBubble) as GameObject;
        speechbubble.transform.SetParent(content.transform, false);
        speechbubble.name = "SpeechBubble" + lineNumber.ToString();
        speechbubble.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
        currentVisibleLines.Add(speechbubble);

        dialogText = GameObject.Find("CharacterDialogText");

        dialogText.GetComponent<TextMeshProUGUI>().text = lineNumber.ToString();
        //dialogText.GetComponent<TextMeshProUGUI>().text = startLineBlock.sceneComponents[0].dialog;

        lineNumber++;

        // EndCheck();

        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, speechbubble.GetComponent<RectTransform>().sizeDelta.y*currentVisibleLines.Count);


    }

    //[SerializeField] TextMeshProUGUI _textMeshPro;
    [Header("Type Writer Effect")]
    public string[] stringArray;

    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;

    int i = 0;

    public void EndCheck()
    {
        Debug.Log("???");
        if (i <= stringArray.Length - 1)
        {
        startLineBlock.sceneComponents[0].dialog = stringArray[i];
            StartCoroutine(TextVisible());

        Debug.Log("Working");
        }
    }

    private IEnumerator TextVisible()
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
    }

    void OnNext()
    {
        Debug.Log("Next!!!");
        LineRunner();

    } 
}
