using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSizer : MonoBehaviour
{
    public RectTransform characterDialogText;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(characterDialogText.GetComponent<RectTransform>().sizeDelta.x, characterDialogText.GetComponent<RectTransform>().sizeDelta.y);

        //Debug.Log()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
