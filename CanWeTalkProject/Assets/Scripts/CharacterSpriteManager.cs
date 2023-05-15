using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpriteManager : MonoBehaviour
{
    [Header("Character Sprites")]
    public GameObject wifeSpriteGO;
    public GameObject boySpriteGO;
    public GameObject girlSpriteGO;
    public GameObject dogSpriteGO;

    [Header("Sprite Locations")]
    public Image spriteLocation1;
    public Image spriteLocation2;
    public Image spriteLocation3;
    public Image spriteLocation4;

    public Line.Character character;

    public float sizeLerpDuration;
    public float opacityLerpDuration;

    private float _sizeLerpValue;
    private float _opacityLerpValue;

    public List<GameObject> activeCharacterSprites;
    // Start is called before the first frame update

    public void FigureCharacterSprites(Line.Character character)
    {
        Debug.Log("Checkingifitsanewcharacter");
        if (activeCharacterSprites.Count == 0)
        {
            Debug.Log("there are no characters");
            NewCharacterSprite(character);
        }
        else
        {
            Debug.Log("there are some characters so lets go!");
            for (int i = 0; i < activeCharacterSprites.Count; i++)
            {
               /* Debug.Log("aaaaaaaa");
                if (character.ToString() + "GO" == activeCharacterSprites[i].ToString())
                {
                    Debug.Log("we good fam");
                }
                else
                {
                    NewCharacterSprite(character);
                } */
            }
        }
    }

    public void NewCharacterSprite(Line.Character character)
    {
        if (character == Line.Character.Wife)
        {
            GameObject wifeSprite = Instantiate(wifeSpriteGO) as GameObject;
            wifeSprite.transform.SetParent(spriteLocation1.transform, false);
            wifeSprite.name = "wifeSprite";
            activeCharacterSprites.Add(wifeSprite);
            AddCharacterSprite(wifeSprite);


            //spriteLocation1.sprite = wifeSprite;
        }
        if (character == Line.Character.Boy)
        {
            GameObject boySprite = Instantiate(boySpriteGO) as GameObject;
            boySprite.transform.SetParent(spriteLocation1.transform, false);
            boySprite.name = "boySprite";
            activeCharacterSprites.Add(boySprite);
            AddCharacterSprite(boySprite);

            // spriteLocation1.sprite = boySprite;
        }
        if (character == Line.Character.Girl)
        {
            GameObject girlSprite = Instantiate(girlSpriteGO) as GameObject;
            girlSprite.transform.SetParent(spriteLocation1.transform, false);
            girlSprite.name = "girlSprite";
            activeCharacterSprites.Add(girlSprite);
             AddCharacterSprite(girlSprite);

            // spriteLocation1.sprite = girlSprite;
        }
        if (character == Line.Character.Dog)
        {
            GameObject dogSprite = Instantiate(dogSpriteGO) as GameObject;
            dogSprite.transform.SetParent(spriteLocation1.transform, false);
            dogSprite.name = "dogSprite";
            activeCharacterSprites.Add(dogSprite);
            AddCharacterSprite(dogSprite);
            //spriteLocation1.sprite = dogSprite;
        }
    }
    public void AddCharacterSprite(GameObject newCharacterSprite)
    {
       // newCharacterSprite.GetComponent<Image>().color = new Color(1, 1, 1, 0);
       // StartCoroutine(OpacityLerp(0, 1, newCharacterSprite));
        //activeCharacterSprites.Add(newCharacterSprite);
    }

    public void MakeSmaller()
    {
        //StartCoroutine(SizeLerp();
    }

    public void MakeBigger()
    {

    }

    public void ChangePosition()
    {

    }

    IEnumerator SizeLerp(float startValue, float endValue, GameObject characterSprite)
    {

        float timeElapsed = 0;
        while (timeElapsed < sizeLerpDuration)
        {
            _sizeLerpValue = Mathf.Lerp(startValue, endValue, timeElapsed / sizeLerpDuration);
            timeElapsed += Time.deltaTime;
            characterSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(_sizeLerpValue, _sizeLerpValue);
            yield return null;
        }
        _sizeLerpValue = endValue;
    }

    IEnumerator OpacityLerp(float startValue, float endValue, GameObject characterSprite)
    {
        float timeElapsed = 0;
        while (timeElapsed < opacityLerpDuration)
        {
            _opacityLerpValue = Mathf.Lerp(startValue, endValue, timeElapsed / opacityLerpDuration);
            timeElapsed += Time.deltaTime;
            characterSprite.GetComponent<Image>().color = new Color(1, 1, 1, _opacityLerpValue);
            yield return null;
        }
        _opacityLerpValue = endValue;
    }
}
