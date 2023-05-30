using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttitudeManager : MonoBehaviour
{
    public SpriteManager spriteManager;

    [Header("Character Attitudes")]
    public float youAttitudeLevel;
    public float wifeAttitudeLevel;
    public float kidsAttitudeLevel;
    public float dogAttitudeLevel;
    public float friendAttitudeLevel;
    public float lawyerAttitudeLevel;
    public float principalAttitudeLevel;

    [Header("AttitudeChangeAmount")]
    public float megaLoss;
    public float highLoss;
    public float midLoss;
    public float lowLoss;
    public float lowGain;
    public float midGain;
    public float highGain;
    public float megaGain;

    [Header("Attitude Bar")]
    public Image youAttitudeBar;
    public Image wifeAttitudeBar;
    public Image kidsAttitudeBar;
    public Image dogAttitudeBar;
    public Image friendAttitudeBar;
    public Image lawyerAttitudeBar;

    [Header("Attitude Bar Lerping")]
    public float attitudeLerpDuration;
    private float _valueToLerp;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("AttitudeManager").Length == 1)
        {
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }

       
        SetAttitude();
    }


    public void UpdateAttitudes(Line line)
    {

        for (int i = 0; i < line.attitudeArray.Count; i++)

        {
            float attitudeToChange = 0;
            float whoseAttitude = 0;
            Image whoseAttitudeBar = null;

            FigureAttitudeChangeAmount(line, i, attitudeToChange, whoseAttitude, whoseAttitudeBar);
                      
        }
    }

    //figures out the amount of atttitude to change SUPER efficiently
    public void FigureAttitudeChangeAmount(Line line, int i, float attitudeToChange, float whoseAttitude, Image whoseAttitudeBar)
    {
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.megaLoss)
        {
            attitudeToChange = megaLoss;
        }
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.highLoss)
        {
            attitudeToChange = highLoss;
        }
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.midLoss)
        {
            attitudeToChange = midLoss;
        }
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.lowLoss)
        {
            attitudeToChange = lowLoss;
        }
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.lowGain)
        {
            attitudeToChange = lowGain;
        }
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.midGain)
        {
            attitudeToChange = midGain;
        }
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.highGain)
        {
            attitudeToChange = highGain;
        }
        if (line.attitudeArray[i].attitudeChangeAmount == Line.AttitudeEffects.AttitudeChange.megaGain)
        {
            attitudeToChange = megaGain;
        }

        FigureWhoseAttitudeIsChanging(line, i, attitudeToChange, whoseAttitude, whoseAttitudeBar);
    }

    //its self explanatory, it also alters the attitude level of the character on this actual script
    public void FigureWhoseAttitudeIsChanging(Line line, int i, float attitudeToChange, float whoseAttitude, Image whoseAttitudeBar)
    {
        if (line.attitudeArray[i].attitudeCharacter == Line.AttitudeEffects.AttitudesCharacter.youAttitude)
        {

            whoseAttitude = youAttitudeLevel;
            whoseAttitudeBar = youAttitudeBar;
            youAttitudeLevel += attitudeToChange;

        }

        //change wife attitude
        if (line.attitudeArray[i].attitudeCharacter == Line.AttitudeEffects.AttitudesCharacter.wifeAttitude)
        {
            whoseAttitude = wifeAttitudeLevel;
            whoseAttitudeBar = wifeAttitudeBar;
            wifeAttitudeLevel += attitudeToChange;
        }

        //change kids attitude
        if (line.attitudeArray[i].attitudeCharacter == Line.AttitudeEffects.AttitudesCharacter.kidsAttitude)
        {
            whoseAttitude = kidsAttitudeLevel;
            whoseAttitudeBar = kidsAttitudeBar;
            kidsAttitudeLevel += attitudeToChange;
        }

        //change dog attitude
        if (line.attitudeArray[i].attitudeCharacter == Line.AttitudeEffects.AttitudesCharacter.dogAttitude)
        {
            whoseAttitude = dogAttitudeLevel;
            whoseAttitudeBar = dogAttitudeBar;
            dogAttitudeLevel += attitudeToChange;
        }

        if (line.attitudeArray[i].attitudeCharacter == Line.AttitudeEffects.AttitudesCharacter.friendAttitude)
        {
            whoseAttitude = friendAttitudeLevel;
            whoseAttitudeBar = friendAttitudeBar;
            friendAttitudeLevel += attitudeToChange;
        }

        if (line.attitudeArray[i].attitudeCharacter == Line.AttitudeEffects.AttitudesCharacter.lawyerAttitude)
        {
            whoseAttitude = lawyerAttitudeLevel;
            whoseAttitudeBar = lawyerAttitudeBar;
            lawyerAttitudeLevel += attitudeToChange;
        }

        ChangeAttitude(attitudeToChange, whoseAttitude, whoseAttitudeBar, line.attitudeArray[i].attitudeCharacter);

    }

    public void ChangeAttitude(float attitudeToChange, float whoseAttitude, Image whoseAttitudeBar, Line.AttitudeEffects.AttitudesCharacter character)
    {

        StartCoroutine(AttitudeLerp(whoseAttitude, whoseAttitude + attitudeToChange, whoseAttitudeBar));
        whoseAttitude += attitudeToChange;
        AttitudeEffect(character, attitudeToChange);
    }

    public void SetAttitude()
    {
        youAttitudeBar.fillAmount = youAttitudeLevel / 100;
        wifeAttitudeBar.fillAmount = wifeAttitudeLevel / 100;
        kidsAttitudeBar.fillAmount = kidsAttitudeLevel / 100;
        dogAttitudeBar.fillAmount = dogAttitudeLevel / 100;
    }


    public void AttitudeEffect(Line.AttitudeEffects.AttitudesCharacter character, float attitudeChangeAmount)
    {
        if ((int)character >= 1)
        {
            spriteManager.CreateAttitudeReaction(character, attitudeChangeAmount);
        }
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
}
