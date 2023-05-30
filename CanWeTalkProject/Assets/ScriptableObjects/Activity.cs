using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Activity", menuName = "Dialog/New Activity")]
public class Activity : ScriptableObject
{
    public string activityName;
    public Line activityResponse;

   /* [Header("Attitude Arrays")]
    public int attitudeArrayLength;
    public List<Line.AttitudeEffects> attitudeArray; */

    [Header("Location Conditions")]
    public int locationArrayLength;
    public List<DialogScene.Location> locationArray;

    [Header("Action Conditions")]
    public int actionArrayLength;
    public List<LineBlock.Actions> actionArray;

    [Header("Day Type Conditions")]
    public int dayTypeArrayLength;
    public List<DialogScene.DayType> dayTypeArray;

    [Header("Day Time Conditions")]
    public int dayTimeArrayLength;
    public List<DialogScene.DayTime> dayTimeArray;

    

    private void OnEnable()
    {

      /*  if(attitudeArray == null)
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
        } */

        if(locationArray == null)
        {
            locationArray = new List<DialogScene.Location>(new DialogScene.Location[locationArrayLength]);

            for (int i = 0; i < locationArrayLength; i++)
            {
                var tmp = new DialogScene.Location();

                locationArray[i] = tmp;
            }
        }

        if (actionArray == null)
        {
            actionArray = new List<LineBlock.Actions>(new LineBlock.Actions[actionArrayLength]);

            for (int i = 0; i < actionArrayLength; i++)
            {
                var tmp = new LineBlock.Actions();

                actionArray[i] = tmp;
            }
        }

        if (dayTypeArray == null)
        {
            dayTypeArray = new List<DialogScene.DayType>(new DialogScene.DayType[dayTypeArrayLength]);

            for (int i = 0; i < dayTypeArrayLength; i++)
            {
                var tmp = new DialogScene.DayType();

                dayTypeArray[i] = tmp;
            }
        }

        if (dayTimeArray == null)
        {
            dayTimeArray = new List<DialogScene.DayTime>(new DialogScene.DayTime[dayTimeArrayLength]);

            for (int i = 0; i < dayTimeArrayLength; i++)
            {
                var tmp = new DialogScene.DayTime();

                dayTimeArray[i] = tmp;
            }
        }

    }


}
