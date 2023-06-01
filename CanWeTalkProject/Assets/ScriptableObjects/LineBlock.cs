using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Line;

[CreateAssetMenu(fileName = "LineBlock", menuName = "Dialog/New Line Block")]
public class LineBlock : ScriptableObject
{
    //public
   // [Tooltip($"{lines[}")]
    public Line[] lines;

    public DecisionBlock endDecisionBlock;

    public bool runActivityBlockNext;

    //public ActivityBlock endActivityBlock;

    public LineBlock nextLineBlock;

    public DialogScene nextScene;

    public bool goToNextDayTime;

    public bool goToNextDay;
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

        if (actionArrayLength != 0)
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
    public int actionArrayLength;

    [System.Serializable]
    public class ActionListElement
    {
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
            GaveKidsPancakes,
            LawyerAtCourt,
            WifeAndKidsHome,
            FriendAtTown,
            KidsAtSchool,
            KidsHome,
            DogHome,
            DidntDisciplineKids,
            NeitherParentUnfit,
            WantsTheDog,
            GotLawyerNumber,
            WentOutPartying,
            DeadDog,
            LeftChildrenHomeAlone
        }

        public Actions actionsCondition;

        public enum TrueOrFalse
        {
            True,
            False,
        }
        public TrueOrFalse trueOrFalse;
    }

    
    public List<ActionListElement> actionArray;

    private void OnEnable()
    {
        if (actionArray == null)
        {
            actionArray = new List<ActionListElement>(new ActionListElement[actionArrayLength]);
            //Debug.Log("Awake");

            for (int i = 0; i < actionArrayLength; i++)
            {
                //create new data object
                var tmp = new ActionListElement();

               // tmp.actionsCondition = 0;

                //store the Data object in our dataArray
                actionArray[i] = tmp;
            }
        }
    }


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
