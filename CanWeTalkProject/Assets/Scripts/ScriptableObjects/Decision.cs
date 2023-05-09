using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "Dialog/New Decision")]
public class Decision : ScriptableObject
{
    public string decisionName;

    public LineBlock followingLineBlock;

    
}
