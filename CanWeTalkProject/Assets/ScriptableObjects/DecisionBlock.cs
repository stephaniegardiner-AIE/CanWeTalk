using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecisionBlock", menuName = "Dialog/New Decision Block")]
public class DecisionBlock : ScriptableObject
{
    public Decision[] decisions;
}
