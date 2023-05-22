using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "Dialog/New Decision")]
public class Decision : ScriptableObject
{
    [SerializeField]
    public string decisionName;

    [SerializeField]
    public LineBlock followingLineBlock;

    
}
