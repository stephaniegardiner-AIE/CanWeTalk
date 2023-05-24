using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "Dialog/New Activity")]
public class Activity : ScriptableObject
{
    public string activityName;

    public int attitudeArrayLength;

    public List<Line.AttitudeEffects> attitudeArray;

    private void OnAwake()
    {
        attitudeArray = new List<Line.AttitudeEffects>(new Line.AttitudeEffects[attitudeArrayLength]);


        for (int i = 0; i < attitudeArrayLength; i++)
        {
            //create new data object
            var tmp = new Line.AttitudeEffects();

            //tmp.attitudeChangeAmount = 0;

            //store the Data object in our dataArray
            attitudeArray[i] = tmp;
        }


    }
}
