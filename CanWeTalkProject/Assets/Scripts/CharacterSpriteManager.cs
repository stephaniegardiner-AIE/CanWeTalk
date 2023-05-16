using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpriteManager : MonoBehaviour
{
    public GameObject canvas;
    
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
    public float positionLerpDuration;

    private float _sizeLerpValue;
    private float _opacityLerpValue;
    private float _xPositionLerpValue;
    private float _yPositionLerpValue;

    public GameObject primaryCharacter;

    public List<GameObject> secondaryCharacterSprites;

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
        if (character == Line.Character.Wife)
        {
            if (wifeActive)
            {

                SetPrimaryCharacter("wifeSprite", wifeActive);
                
            }
            if(!wifeActive)
            {
                
                GameObject wifeSprite = Instantiate(wifeSpriteGO) as GameObject;
                wifeSprite.transform.SetParent(canvas.transform, false);
                wifeSprite.name = "wifeSprite";

                
                AddCharacterSprite(wifeSprite);
                SetPrimaryCharacter("wifeSprite", wifeActive);
                wifeActive = true;
                
            }
           
        }

        if (character == Line.Character.Boy)
        {
            if (boyActive)
            {
                SetPrimaryCharacter("boySprite", boyActive);
            }
            if (!boyActive)
            {
                
                GameObject boySprite = Instantiate(boySpriteGO) as GameObject;
                boySprite.transform.SetParent(canvas.transform, false);
                boySprite.name = "boySprite";

                
                AddCharacterSprite(boySprite);
                SetPrimaryCharacter("boySprite", boyActive);
                boyActive = true;
                
            }           
        }

        if (character == Line.Character.Girl)
        {
            if (girlActive)
            {
                SetPrimaryCharacter("girlSprite", girlActive);
            }
            if (!girlActive)
            {
                GameObject girlSprite = Instantiate(girlSpriteGO) as GameObject;
                girlSprite.transform.SetParent(canvas.transform, false);
                girlSprite.name = "girlSprite";

                
                AddCharacterSprite(girlSprite);
                SetPrimaryCharacter("girlSprite", girlActive);
                girlActive = true;
            }           
        }

        if (character == Line.Character.Dog)
        {
            if (dogActive)
            {
                SetPrimaryCharacter("dogSprite", dogActive);
            }
            if (!dogActive)
            {
                GameObject dogSprite = Instantiate(dogSpriteGO) as GameObject;
                dogSprite.transform.SetParent(canvas.transform, false);
                dogSprite.name = "dogSprite";

                
                AddCharacterSprite(dogSprite);
                SetPrimaryCharacter("dogSprite", dogActive);
                dogActive = true;

            }         
        }
    }

    public void CalculateSecondaryCharacterSize()
    {
        spriteLocation2.GetComponent<RectTransform>().sizeDelta = new Vector2(spriteLocation3.gameObject.GetComponent<RectTransform>().sizeDelta.x 
            * secondaryCharacterSprites.Count, 
            spriteLocation2.gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void SetPrimaryCharacter(string gameObjectName, bool active)
    {
        GameObject characterSprite;

        foreach (var character in activeCharacterSprites)
        {
            if (character.name == gameObjectName)
            {
                characterSprite = character;

                if (primaryCharacter != null)
                {
                    MoveToSecondary(primaryCharacter);
                    secondaryCharacterSprites.Add(primaryCharacter);
                }
                           
                primaryCharacter = characterSprite;
                secondaryCharacterSprites.Remove(characterSprite);              
                MoveToPrimary(characterSprite);               
                break;
            }
        }        
    }
    public void AddCharacterSprite(GameObject newCharacterSprite)
    {
        newCharacterSprite.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        StartCoroutine(OpacityLerp(0, 1, newCharacterSprite));
        activeCharacterSprites.Add(newCharacterSprite);
    }

    public void MoveToPrimary(GameObject characterSprite)
    {        
        StartCoroutine(SizeLerp(characterSprite.GetComponent<RectTransform>().sizeDelta.y,
            spriteLocation1.GetComponent<RectTransform>().sizeDelta.y,
            characterSprite));

        StartCoroutine(PositionLerp(characterSprite.GetComponent<RectTransform>().position.x, 
            characterSprite.GetComponent<RectTransform>().position.y, 
            spriteLocation1.rectTransform.position.x, 
            spriteLocation1.rectTransform.position.y, 
            characterSprite,
            spriteLocation1.gameObject));

        CalculateSecondaryCharacterSize();
    }

    public void MoveToSecondary(GameObject characterSprite)
    {
        CalculateSecondaryCharacterSize();

        StartCoroutine(SizeLerp(characterSprite.GetComponent<RectTransform>().sizeDelta.y, 
            spriteLocation2.GetComponent<RectTransform>().sizeDelta.y, 
            characterSprite));

        StartCoroutine(PositionLerp(characterSprite.GetComponent<RectTransform>().position.x,
            characterSprite.GetComponent<RectTransform>().position.y,
            spriteLocation2.rectTransform.position.x,
            spriteLocation2.rectTransform.position.y,
            characterSprite,
            spriteLocation2.gameObject));
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

    IEnumerator PositionLerp(float xStartValue, float yStartValue, float xEndValue, float yEndValue, GameObject characterSprite, GameObject endLocation)
    {

        float timeElapsed = 0;

        characterSprite.transform.SetParent(endLocation.transform, true);

        while (timeElapsed < positionLerpDuration)
        {
            _xPositionLerpValue = Mathf.Lerp(xStartValue, xEndValue, timeElapsed / positionLerpDuration);
            _yPositionLerpValue = Mathf.Lerp(yStartValue, yEndValue, timeElapsed / positionLerpDuration);
            timeElapsed += Time.deltaTime;
            characterSprite.GetComponent<RectTransform>().position = new Vector2(_xPositionLerpValue, _yPositionLerpValue);
            yield return null;
        }
        _xPositionLerpValue = xEndValue;
        _yPositionLerpValue = yEndValue;                
    }
}
