using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "Dialog/New Scene")]
public class Scene : ScriptableObject
{
    public LineBlock[] lineBlocks;
    public int dayNumber;
    public enum WeekDay
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
    }

    public WeekDay weekday;

    public enum DayTime
    {
        Morning,
        Afternoon,
        Night,
    }

    public DayTime dayTime;
      
}
