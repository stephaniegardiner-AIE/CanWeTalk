using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actions : MonoBehaviour
{

    public List<bool> actionList;

    public void MakeActionList()
    {
        
        var actionsCount = LineBlock.ActionListElement.Actions.GetNames(typeof(LineBlock.ActionListElement.Actions)).Length;

        for (int i = 0; i < actionsCount; i++)
        {
            var actionNumber = (LineBlock.ActionListElement.Actions)i;
            var actionDisplayStatus = (LineBlock.ActionListElement.Actions)actionNumber;
            string boolName = actionDisplayStatus.ToString();
            actionList.Add(false);

        }
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        actionList.Clear();
        MakeActionList();
    }

    public void SetTrue(LineBlock.ActionListElement.Actions action)
    {       
        
        int actionNumber = (int)action;
       // Debug.Log("lets go" + actionNumber);
        actionList[actionNumber] = true;
      //  Debug.Log(actionNumber);

    }

    public void AssignSelf()
    {
        ActivityManager thisone = FindObjectOfType<ActivityManager>();
        thisone.actions = this;
        SceneStarter thisotherone = FindObjectOfType<SceneStarter>();
        thisotherone.actions = this;
       // Debug.Log("assign actions");
    }
}
