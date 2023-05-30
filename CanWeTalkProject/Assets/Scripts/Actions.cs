using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actions : MonoBehaviour
{
    //public Dictionary<string, bool> actionList;

    /*  public bool none, ; 
      public bool signedPapers;
      public bool wentToWork;
      public bool pickingKidsUp;
      public bool forgotToPickKidsUp;
      public bool letKidsStayUp;
      public bool sleepingOnTheCouch;
      public bool wantsFullCustody;
      public bool wantsNoCustody;
      public bool wantsSplitCustody;
      public bool gaveKidsPancakes; */

    public List<bool> actionList;

    //public string boolName;

    public void MakeActionList()
    {
        var actionsCount = LineBlock.ActionListElement.Actions.GetNames(typeof(LineBlock.ActionListElement)).Length;

        //actionList = new Dictionary<string, bool>();


        for (int i = 0; i < actionsCount; i++)
        {
            var actionNumber = (LineBlock.ActionListElement.Actions)i;
            var actionDisplayStatus = (LineBlock.ActionListElement.Actions)actionNumber;
            string boolName = actionDisplayStatus.ToString();


            actionList.Add(false);
            //BuildPersonnelXml2(actionList);

            //Debug.Log(actionList);
            //new bool boolName;


        }

        //for (int i = 0; i < LineBlock.Actions.Parse(LineBlock.Actions); i++)

        //actionList.Add(new bool.name = LineBlock.Actions.GetValues(typeof(LineBlock.Actions)).Cast<LineBlock.Actions>().Last());

    }

   /* public string BuildPersonnelXml2(Dictionary<string, bool> actionList)
    {
        Tuple<string, bool> myResult = new Tuple(actionList["bool"], actionList[boolName]);


    } */

        public void Start()
    {

            DontDestroyOnLoad(gameObject);

;
        actionList.Clear();
        //MakeActionList();

    }



    public void SetTrue(LineBlock.ActionListElement.Actions action)
    {
        int actionNumber = (int)action;
        actionList[actionNumber] = true;

    }

    public void AssignSelf()
    {
        ActivityManager thisone = FindObjectOfType<ActivityManager>();

        thisone.actions = this;
    }
}
