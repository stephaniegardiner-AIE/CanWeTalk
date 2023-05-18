using System.Collections;
using System.Collections.Generic;
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
    }

    public Character character;

    [TextArea(15, 20)]
    public string dialog;

    public int attitudeArrayLength;

    [System.Serializable]
    public class AttitudeArray
    {
        public enum Attitudes
        {
            youAttitude,
            wifeAttitude,
            kidsAttitude,
            dogAttitude,
        }

        public Attitudes attitudeChangeEffects;
        public float attitudeChangeAmount;
    }

    public AttitudeArray[] attitudeArray;

    private void Awake()
    {
        attitudeArray = new AttitudeArray[attitudeArrayLength];
        

        for (int i = 0; i < attitudeArrayLength; i++)
        {
            //create new data object
            var tmp = new AttitudeArray();

           // tmp.attitudeChangeEffects =
           //tmp.attitudeChangeEffects = 
            tmp.attitudeChangeAmount = 0;

            //store the Data object in our dataArray

            attitudeArray[i] = tmp;
        }

        
    }

}




