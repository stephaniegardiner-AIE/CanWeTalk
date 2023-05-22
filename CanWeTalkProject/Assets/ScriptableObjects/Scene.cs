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
    }

    public WeekDay weekday;

    public enum DayTime
    {
        Morning,
        Afternoon,
        Night,
    }

    public DayTime dayTime;

    public enum Location
    {
        House,
        Court,
        Town,
    }

    public Location location;
      
}
