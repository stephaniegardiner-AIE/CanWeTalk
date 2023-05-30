using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingCanvas : MonoBehaviour
{
    public Canvas cover;
    public Image img;
    public GameObject thisThing;

    // Start is called before the first frame update
    void Start()
    {
        thisThing = gameObject;
        cover = thisThing.GetComponent<Canvas>();
        img = cover.GetComponent<Image>();
    }

    public void FadeOut()
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(true));
    }

    public void FadeIn()
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(false));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
            fadeAway = false;
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                fadeAway = true;
                yield return null;
            }
            
        }
    }
}
