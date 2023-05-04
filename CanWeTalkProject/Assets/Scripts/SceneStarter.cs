using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneStarter : MonoBehaviour
{
    public LineBlock startLineBlock;
    public GameObject characterSpeechBubble;
    public GameObject content;
    public float xPositionSpeechBubble;
    public float yPositionSpeechBubble;
    public GameObject dialogText;
    public float lineNumber;
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


        dialogText = GameObject.Find("CharacterDialogText");

        dialogText.GetComponent<TextMeshProUGUI>().text = startLineBlock.sceneComponents[0].dialog;

        lineNumber++;


    }
}
