using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LineBlock", menuName = "Dialog/New Line Block")]
public class LineBlock : ScriptableObject
{
    //public

    public Line[] lines;

    public DecisionBlock endDecisionBlock;

    public ActivityBlock endActivityBlock;

    public enum Conditions
    {
       Attitude,
       Action,
       Condition,

    }

    public Conditions conditions;

    private void Awake()
    {

    }

    private void OnValidate()
    {
        if (conditions == Conditions.Attitude)
        {
            //Debug.Log("attitude constraint");
        }
        if (conditions == Conditions.Action)
        {
            //Debug.Log("action restraint");

        }

        if (hasConditions == true)
        {
            //Debug.Log("hasConditions");


        }
    }

    public bool hasConditions;

    [Header("Action Conditions")]
    public bool actionCondition;

    public enum Actions
    {
        None,
        SignedPapers,
        WentToWork,
        PickingKidsUp,
        ForgotToPickKidsUp,
        LetKidsStayUp,
        SleepingOnCouch,
        WantsFullCustody,
        WantsNoCustody,
        WantsSplitCustody,
        GaveKidsPancakes
    }

    public Actions actions;

    [Header("Attitude Conditions")]

    public bool attitudeCondition;

    public Line.AttitudeArray.Attitudes attitudes;
    
    public enum AttitudeLevel
    {
        GreaterThan,
        LessThan,
    }

    public AttitudeLevel attitudeLevel;

    public float attitudeAmount;
   

    /*[Header("Actions")]
    bool signedPapers;
    bool wentToWork;
    bool pickingKidsUp;
    bool forgotToPickKidsUp;
    public bool letKidsStayUp;
    public bool sleepingOnTheCouch;
    public bool wantsFullCustody;
    public bool wantsNoCustody;
    public bool wantsSplitCustody;
    public bool gaveKidsPancakes;  */


    //public List<bool> ( true, false );
    /* public List<bool> items = new List<bool> { 
         bool tooeat = false, 
         signedPapers = false, 
         didnSign = false }; */
}
