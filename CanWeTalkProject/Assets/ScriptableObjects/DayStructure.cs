using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "DayStructure", menuName = "Dialog/New Day Structure")]
public class DayStructure : ScriptableObject
{

    public Object scene;
    public ScriptableObject[] dayParts;

    public DialogScene.DayTime dayTime;

}
