using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "DayStructure", menuName = "Dialog/New Day Structure")]
public class DayStructure : ScriptableObject
{

    public Object scene;
    public ScriptableObject morning;
        public ScriptableObject afternoon;
        public ScriptableObject night;

}
