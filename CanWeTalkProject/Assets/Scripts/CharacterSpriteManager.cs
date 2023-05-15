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


    public bool wifeActive = false;
    public bool boyActive = false;
    public bool girlActive = false;
    public bool dogActive = false;
    // Start is called before the first frame update

    public void FigureCharacterSprites(Line.Character character)
    {
            NewCharacterSprite(character);
    }

    public void NewCharacterSprite(Line.Character character)
    {
        if (character == Line.Character.Wife && !wifeActive)
        {
            GameObject wifeSprite = Instantiate(wifeSpriteGO) as GameObject;
            wifeSprite.transform.SetParent(spriteLocation1.transform, false);
            wifeSprite.name = "wifeSprite";
            activeCharacterSprites.Add(wifeSprite);
            AddCharacterSprite(wifeSprite);
            wifeActive = true;

            //spriteLocation1.sprite = wifeSprite;
        }
        if (character == Line.Character.Boy && !boyActive)
        {
            GameObject boySprite = Instantiate(boySpriteGO) as GameObject;
            boySprite.transform.SetParent(spriteLocation1.transform, false);
            boySprite.name = "boySprite";
            activeCharacterSprites.Add(boySprite);
            AddCharacterSprite(boySprite);
            boyActive = true;

            // spriteLocation1.sprite = boySprite;
        }
        if (character == Line.Character.Girl && !girlActive)
        {
            GameObject girlSprite = Instantiate(girlSpriteGO) as GameObject;
            girlSprite.transform.SetParent(spriteLocation1.transform, false);
            girlSprite.name = "girlSprite";
            activeCharacterSprites.Add(girlSprite);
             AddCharacterSprite(girlSprite);
            girlActive = true;

            // spriteLocation1.sprite = girlSprite;
        }
        if (character == Line.Character.Dog && !dogActive)
        {
            GameObject dogSprite = Instantiate(dogSpriteGO) as GameObject;
            dogSprite.transform.SetParent(spriteLocation1.transform, false);
            dogSprite.name = "dogSprite";
            activeCharacterSprites.Add(dogSprite);
            AddCharacterSprite(dogSprite);
            dogActive = true;
            //spriteLocation1.sprite = dogSprite;
        }
    }
    public void AddCharacterSprite(GameObject newCharacterSprite)
    {
        newCharacterSprite.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        StartCoroutine(OpacityLerp(0, 1, newCharacterSprite));
        activeCharacterSprites.Add(newCharacterSprite);
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
