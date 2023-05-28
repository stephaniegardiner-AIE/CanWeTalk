using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Scene", menuName = "Dialog/New Scene")]
public class Scene : ScriptableObject
{
    public LineBlock[] lineBlocks;
    public int dayNumber;

    [SerializeField]
    public enum WeekDay
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
    }

    public WeekDay weekday;

    public enum DayTime
    {
        None,
        Morning,
        Afternoon,
        Night,
    }

    public DayTime dayTime;

    public enum DayType
    {
        None,
        Weekday,
        Weekend,
    }

    public DayType dayType;

    public enum Location
    {
        House,
        Court,
        Town,
    }

    public Location location;


    private void OnEnable()
    {
        if (dayType == DayType.None)
        {
            if (weekday == WeekDay.Sunday || weekday == WeekDay.Saturday)
            {
                dayType = DayType.Weekend;
            }
            else
            {
                dayType = DayType.Weekday;
            }
        }
    }
}
