using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Line", menuName = "Dialog/New Line")]
public class Line : ScriptableObject
{

    public enum Character
    {
        Description, //0
        You, //1
        Wife, //2
        Boy, //3
        Girl, //4
        Dog,  //5
        Friend,
        Lawyer,
        Principal,
    }

    public Character character;

    [TextArea(15, 20)]
    public string dialog;

    public int attitudeArrayLength;

    [System.Serializable]
    public class AttitudeEffects
    {
        public enum AttitudesCharacter
        {
            none,
            youAttitude,
            wifeAttitude,
            kidsAttitude,
            dogAttitude,
            friendAttitude,
            lawyerAttitude,
            principalAttitude,
        }

        public AttitudesCharacter attitudeChangeEffects;

        public enum AttitudeChange
        {
            megaLoss,
            highLoss,
            midLoss,
            lowLoss,
            lowGain,
            midGain,
            highGain,
            megaGain,
        }
        public float attitudeChangeAmount;
    }

    public List<AttitudeEffects> attitudeArray;

    private void OnAwake()
    {
        attitudeArray = new List<AttitudeEffects> (new AttitudeEffects[attitudeArrayLength]);
        

        for (int i = 0; i < attitudeArrayLength; i++)
        {
            //create new data object
            var tmp = new AttitudeEffects();

            //tmp.attitudeChangeAmount = 0;

            //store the Data object in our dataArray
            attitudeArray[i] = tmp;
        } 

        
    }


    private void OnValidate()
    {
        if (action != 0)
        {
            hasAction = true;
        }
        if (action == 0)
        {
            hasAction = false;
        }


            //AssetDatabase.SaveAssets();
        
        
    }
    public bool hasAction;

    public LineBlock.Actions action;

    
}




