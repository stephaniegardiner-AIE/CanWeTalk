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

    public LineBlock nextLineBlock;

    public int nextSceneNumber;
    public enum Conditions
    {
       None,
       Attitude,
       Action,
       Condition,

    }

    [Header("Conditions")]
    public bool hasConditions;
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

        if (actions != 0)
        {
            actionCondition = true;
        }
        else
        {
            actionCondition = false;
        }

        if (conditions != 0)
        {
            hasConditions = true;
        }
        else
        {
            hasConditions = false;  
        }

        if(attitudes != 0)
        {
            attitudeCondition = true;
        }
        else
        {
            attitudeCondition = false;
        }
    }

    

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

    public Line.AttitudeEffects.AttitudesCharacter attitudes;
    
    public enum AttitudeLevel
    {
        None,
        GreaterThan,
        LessThan,
    }

    public AttitudeLevel attitudeLevel;

    public float attitudeAmount;

    public LineBlock ifFailedContinueTo;

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