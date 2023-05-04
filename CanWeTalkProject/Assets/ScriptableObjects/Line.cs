using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Line", menuName = "Dialog/New Line")]
public class Line : ScriptableObject
{
    public enum Character
    {
        Description, //0
        You, //1
        Wife, //2
        Boy, //3
        Girl, //4
        Dog,  //5
    }

    public Character character;

    [TextArea(15, 20)]
    public string dialog;

}
