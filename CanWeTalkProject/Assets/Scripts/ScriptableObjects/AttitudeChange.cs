using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttitudeChange", menuName = "Dialog/New Attitude Change")]
[System.Serializable]
public class AttitudeChange : ScriptableObject

{
    public enum Attitudes
    {
        youAttitude,
        wifeAttitude,
        kidsAttitude,
        dogAttitude,
    }

    public Attitudes attitudeChangeEffects;
    public float attitudeChangeAmount;
}


