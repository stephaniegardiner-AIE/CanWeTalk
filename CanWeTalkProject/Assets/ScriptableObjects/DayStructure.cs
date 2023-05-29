using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DayStructure", menuName = "Dialog/New Day Structure")]
public class DayStructure : ScriptableObject
{

        public ScriptableObject morning;
        public ScriptableObject afternoon;
        public ScriptableObject night;

}
