using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneStarter : MonoBehaviour
{
    public SpriteManager spriteManager;
    public CharacterManager characterManager;
    public AttitudeManager attitudeManager;
    public ActivityManager activityManager;
    public AudioManager audioManager;
    public Scenes sceneManager;
    //public DialogScene currentScene;
    public Actions actions;
    public GameStructure gameStructure;

    //public int sceneNumber;
    
    public bool decision;
    public bool activityActive;
    public bool activityChosen;
    public bool changingDay;

    [Header("Scene Info")]
    public int dayNumber;
    public DialogScene.WeekDay weekday;
    public DialogScene.DayTime dayTime;
    public DialogScene.Location location;

    [Header("Scene Objects")]
    public LineBlock currentLineBlock;
    public GameObject linePrefab;
    public GameObject decisionBlockPrefab;
    public GameObject decisionPrefab;
    public GameObject activityBlockPrefab;
    public GameObject activityPrefab;
    public GameObject content;
    public float contentHeight;
    public float tailHeight;
    public float xPositionSpeechBubble;
    public float yPositionSpeechBubble;
    public GameObject dialogText;
    public Transform dialogBackground;
    public float dialogLinePadding;
    public int lineNumber;
    public int decisionNumber;
    public int activityNumber;
    public TextMeshProUGUI currentTextToWrite;

    public int previousLines;

    public List<GameObject> currentVisibleSpeech;

    [Header("Scene Appearance Objects")]
    public Image sceneBackground;
    public TextMeshProUGUI dayNumberText;
    public TextMeshProUGUI weekdayText;
    //public TextMeshProUGUI dayTimeText;



    [Header("Speech Tails")]
    public GameObject speechTailLeft;
    public GameObject speechTailRight;




    [Header("TypeWriter")]
    public float characterTime;
    public bool typeWriting = false;
    public TextMeshProUGUI currentText;



    [Header("SceneBackgrounds")]
    public Sprite houseDay;
    public Sprite houseAfternoon;
    public Sprite houseNight;

    public Sprite courtDay;
    public Sprite courtAfternoon;

    public Sprite townDay;
    public Sprite townAfternoon;
    public Sprite townNight;

    public float skipSpeed;

    public void AssignSelf()
    {
        GameStructure thisone = FindObjectOfType<GameStructure>();

        thisone.sceneStarter = this;

        ActivityManager thisotherone = FindObjectOfType<ActivityManager>();
        thisotherone.scene = this;

    }

    //public GameObject scrollContent;
    // Start is called before the first frame update
    void Start()
    {
        AssignSelf();
        StartSceneStarter();
    }

    public void StartSceneStarter()
    {

        if (gameStructure == null)
        {
            AssignSelf();
        }
        else
        {


            if (gameStructure.isRunning)
            {
                spriteManager.activeCharacterSprites.Clear();
                spriteManager.secondaryCharacterSprites.Clear();

                if (gameStructure.currentDaySO.dayParts[gameStructure.dayTime].GetType() == typeof(ActivityBlock))
                {
                    //Debug.Log("RUN ACTIVITY");
                    SetSceneInfo();

                    ActivityRunner();
                    // sceneStarter.ActivityRunner();

                }
                if (gameStructure.currentDaySO.dayParts[gameStructure.dayTime].GetType() == typeof(DialogScene))
                {
                    // Debug.Log("RUN LINE BLOCK");
                    SetSceneInfo();
                    LineRunner();

                }

                if (sceneManager.currentScene.lineBlocks != null)
                {
                    if (sceneManager.currentScene.lineBlocks.Length != 0)
                    {
                        currentLineBlock = sceneManager.currentScene.lineBlocks[0];
                    }

                    SetSceneInfo();
                }

                if (dayNumberText != null)
                {
                    SetSceneInfo();
                }


                //LineRunner();

                // if(gameStructure.currentDaySO.dayParts[gameStructure.dayTime].GetType() == typeof(ActivityBlock))
                // {
                //currentLineBlock = null;
                // }



                contentHeight = content.GetComponent<RectTransform>().sizeDelta.y;
            }
        }


        


    }

    public void SetSceneInfo()
    {
        dayNumber = sceneManager.currentScene.dayNumber;
        weekday = sceneManager.currentScene.weekday;

        if (gameStructure.dayTime == 0)
        {
            dayTime = DialogScene.DayTime.Morning;
        }
        if (gameStructure.dayTime == 1)
        {
            dayTime = DialogScene.DayTime.Afternoon;
        }
        if (gameStructure.dayTime == 2)
        {
            dayTime = DialogScene.DayTime.Night;
        }

        //dayTime = sceneManager.currentScene.dayTime;
        location = sceneManager.currentScene.location;
        UpdateSceneAppearance();

        attitudeManager.SetAttitude();
    }

    void OnNext()
    {
        if (gameStructure.isRunning && !changingDay)
        {
            //Debug.Log("on next");
            //if the decision maker isn't active! run the next line :))))
            if (!decision && !activityActive && !activityChosen)
            {
                //Debug.Log("line runner");
                LineRunner();
                //Debug.Log("NextLine");
            }
            if (activityChosen)
            {
                //Debug.Log("sup fam");
                activityChosen = false;
                gameStructure.GoToNextDayTime();
                
            }
            if (sceneBackground.sprite.name == "SpeechBubble")
            {
                
                SetSceneInfo();
            }
        }




    }

    private void SkipDialog()
    {
        //if the decision maker isn't active! run the next line :))))
        if (!decision)
        {
            LineRunner();
            //Debug.Log("Skip dialog");
            //Debug.Log("NextLine");
        }
    }

    void OnFinishDialog()
    {

        StartCoroutine(DialogSkipper());
    }

    public void UpdateSceneAppearance()
    {
        dayNumberText.text = "Day " + (dayNumber + 1).ToString();
        weekdayText.text = weekday.ToString() + ", " + dayTime.ToString();
        //dayTimeText.text = dayTime.ToString();
        //UpdateSceneBackground(dayTime, location);

        if (location == DialogScene.Location.House)
        {
            if (dayTime == DialogScene.DayTime.Morning)
            {
                sceneBackground.sprite = houseDay;
            }
            if (dayTime == DialogScene.DayTime.Afternoon)
            {
                sceneBackground.sprite = houseAfternoon;
            }
            if (dayTime == DialogScene.DayTime.Night)
            {
                sceneBackground.sprite = houseNight;
            }
        }
        if (location == DialogScene.Location.Court)
        {
            if (dayTime == DialogScene.DayTime.Morning)
            {
                sceneBackground.sprite = courtDay;
            }
            if (dayTime == DialogScene.DayTime.Afternoon)
            {
                sceneBackground.sprite = courtAfternoon;
            }
        }
        if (location == DialogScene.Location.Town)
        {
            if (dayTime == DialogScene.DayTime.Morning)
            {
                sceneBackground.sprite = townDay;
            }
            if (dayTime == DialogScene.DayTime.Afternoon)
            {
                sceneBackground.sprite = townAfternoon;
            }
            if (dayTime == DialogScene.DayTime.Night)
            {
                sceneBackground.sprite = townNight;
            }
        }

    }

    //runs the next line
    public void LineRunner()
    {
        //if it's run out of lines, figure out the next thing to do
        if (typeWriting)
        {
            StopCoroutine(TextVisible(currentText));
            currentText.maxVisibleCharacters = currentText.textInfo.characterCount;
        }
        else
        {
            //Debug.Log("almost made it");
            if (sceneManager.currentScene.lineBlocks.Length != 0 && sceneManager.currentScene.lineBlocks != null)
            {
                if (lineNumber > currentLineBlock.lines.Length - 1)
                {

                    Debug.Log("line runner " + currentLineBlock.lines.Length + sceneManager.currentScene.name + " " + lineNumber);
                    FigureNext();
                }
                //if the lines aren't run out, run the next line
                else
                {

                    CreateLine();
                }
            }


        }
       
    }

    public void CreateLine()
    {
        GameObject line = Instantiate(linePrefab) as GameObject;
        CheckCharacterFormat(line);

        line.transform.SetParent(content.transform, false);
        line.name = "SpeechBubble" + lineNumber.ToString();
        line.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
        currentVisibleSpeech.Add(line);

        dialogBackground = line.GetComponent<Transform>();
        dialogText = dialogBackground.Find("CharacterDialogText").gameObject;

        //sets the text
        dialogText.GetComponent<TextMeshProUGUI>().text = currentLineBlock.lines[lineNumber].dialog;

        NameCheck(dialogText.GetComponent<TextMeshProUGUI>());

        TypeWriter(dialogText.GetComponent<TextMeshProUGUI>());

        //if the line has an attitude change, now make sure that happens!!!
        if (currentLineBlock.lines[lineNumber].attitudeArrayLength > 0)
        {
            attitudeManager.UpdateAttitudes(currentLineBlock.lines[lineNumber]);
            //Debug.Log("AttitudeChange!");
        }

        if (currentLineBlock.lines[lineNumber].hasAction)
        {
           // Debug.Log("change action");
            ChangeAction(currentLineBlock.lines[lineNumber]);
        }

        lineNumber++;


        ResizeSpeech();

        ResizeContent(line.GetComponent<RectTransform>().sizeDelta.y + tailHeight);
    }

    public void CreateActivityResponse(int activityNumber)
    {
        GameObject line = Instantiate(linePrefab) as GameObject;
        CheckCharacterFormat(line);

        line.transform.SetParent(content.transform, false);
        line.name = "SpeechBubble" + lineNumber.ToString();
        line.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
        currentVisibleSpeech.Add(line);

        dialogBackground = line.GetComponent<Transform>();
        dialogText = dialogBackground.Find("CharacterDialogText").gameObject;

        //sets the text
        dialogText.GetComponent<TextMeshProUGUI>().text = activityManager.currentActivites[activityNumber].activityResponse.dialog;

        NameCheck(dialogText.transform.GetComponent<TextMeshProUGUI>());

        TypeWriter(dialogText.GetComponent<TextMeshProUGUI>());



        //if the line has an attitude change, now make sure that happens!!!
        if (activityManager.currentActivites[activityNumber].activityResponse.attitudeArrayLength > 0)
        {
            //Debug.Log("hello!");
            attitudeManager.UpdateAttitudes(activityManager.currentActivites[activityNumber].activityResponse);
            
        } 

     /*   if (currentLineBlock.lines[lineNumber].hasAction)
        {
        
            ChangeAction(currentLineBlock.lines[lineNumber]);
        } */

        //lineNumber++;


        ResizeSpeech();

        ResizeContent(line.GetComponent<RectTransform>().sizeDelta.y + tailHeight);

      //  gameStructure.
    }

    public void ChangeAction(Line currentLine)
    {
        // Debug.Log("we made it fam");

        //Debug.Log("hi");

        if (currentLine.action > 0)
        {
            //Debug.Log("damn");
            actions.SetTrue(currentLine.action);
        }
        
    }

    public void ResizeSpeech()
    {
        //establishing a bnch of variables
        float dialogTextSizeDeltaWidth = dialogText.GetComponent<RectTransform>().sizeDelta.x;
        float dialogFontSize = dialogText.GetComponent<TextMeshProUGUI>().fontSize;
        float dialogLineCount = dialogText.GetComponent<TextMeshProUGUI>().textInfo.lineCount;


        dialogText.GetComponent<RectTransform>().sizeDelta = new Vector2(dialogTextSizeDeltaWidth + (dialogLinePadding * 2),
            (dialogFontSize * dialogLineCount) + (dialogLinePadding * dialogLineCount) + (dialogLinePadding * 2));

        float dialogTextWidth = dialogTextSizeDeltaWidth + (dialogLinePadding * 2);

        float dialogTextHeight = (dialogFontSize * dialogLineCount) + (dialogLinePadding * dialogLineCount) + (dialogLinePadding * 2);

 
        float dialogBackgroundHeight = dialogBackground.GetComponent<RectTransform>().sizeDelta.y;

        dialogBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(dialogTextWidth + (dialogLinePadding * 4), dialogTextHeight);

        //Debug.Log(dialogBackgroundHeight);
    }

    //the background for the button block
    public void ResizeButtonBlock(GameObject buttonBlock)
    {
        //float buttonBlockHeight = buttonBlock.GetComponent<RectTransform>().sizeDelta.y;

        float buttonBlockWidth = buttonBlock.transform.parent.GetComponent<RectTransform>().sizeDelta.x;

        int thisDialogLinePadding = (int)dialogLinePadding;

            buttonBlock.GetComponent<VerticalLayoutGroup>().padding.left = (thisDialogLinePadding * 3);

            float buttonsHeight = 0;

            for (int i = 0; i < buttonBlock.transform.childCount; i++)
            {
                buttonsHeight += buttonBlock.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
                // Debug.Log(buttonsHeight);
            }

            //float buttonTextLine

            buttonBlock.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonBlockWidth, buttonsHeight + (dialogLinePadding * 2));
            //Debug.Log("Activites");
       

    }

    //the actual speech bubble
    public void ResizeButtonBlockBackground(GameObject buttonBlockBackground)
    {
        float buttonBlockWidth = (buttonBlockBackground.transform.GetComponent<RectTransform>().sizeDelta.x);
        float buttonBlockHeight = 0;
        
        for (int i = 0; i <buttonBlockBackground.transform.childCount; i++)
        {
            buttonBlockHeight += buttonBlockBackground.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y + (dialogLinePadding * 2);
            
        }

        buttonBlockBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonBlockWidth, buttonBlockHeight);
    }
    public void ResizeButton(GameObject button)
    {
        if (button.tag == "Decision")
        {
            TextMeshProUGUI buttonText = button.GetComponent<Transform>().Find("DecisionButtonText").GetComponent<TextMeshProUGUI>();
            float buttonTextFontSize = buttonText.fontSize;
            float buttonTextLineCount = buttonText.textInfo.lineCount;

            float buttonWidth = button.GetComponent<RectTransform>().sizeDelta.x;
            float buttonHeight = button.GetComponent<RectTransform>().sizeDelta.y;

            button.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth,
                (buttonTextFontSize * buttonTextLineCount));
        }
        
        if (button.tag == "Activity")
        {
            //Debug.Log("I'm an activity");
            TextMeshProUGUI buttonText = button.GetComponent<Transform>().Find("ActivityButtonText").GetComponent<TextMeshProUGUI>();
            float buttonTextFontSize = buttonText.fontSize;
            float buttonTextLineCount = buttonText.textInfo.lineCount;

            //Debug.Log(buttonText.textInfo.lineCount);
           // Debug.Log(button.GetComponent<RectTransform>().sizeDelta.y);

            float buttonWidth = button.GetComponent<RectTransform>().sizeDelta.x;
            float buttonHeight = button.GetComponent<RectTransform>().sizeDelta.y;

            button.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonWidth,
                (buttonHeight * buttonTextLineCount));
        }


        //Debug.Log(buttonTextLineCount);
    }

    //figures out what to run next after a line block is finished
    public void FigureNext()
    {


        //if activity is not next runn the decision
        if (currentLineBlock.endDecisionBlock != null)
        {
            DecisionRunner();
            
        }
        if (currentLineBlock.runActivityBlockNext)
        {
            spriteManager.ClearCharacters();
            ActivityRunner();
           // Debug.Log("RUN ACTVIITY BLOCK NEXT YOU HAVEN'T SET IT UP THOUGH STEPH");
        }
        if (currentLineBlock.nextLineBlock != null)
        {
            lineNumber = 0;
            Debug.Log("go to next line block " + currentLineBlock.lines.Length + sceneManager.currentScene.name + " " + lineNumber);
            GoToNextLineBlock(currentLineBlock.nextLineBlock);
        }
        if (currentLineBlock.nextScene != null)
        {
            lineNumber = 0;
            Debug.Log("go to next dialog scene " + currentLineBlock.lines.Length + sceneManager.currentScene.name + " " + lineNumber);
            spriteManager.ClearCharacters();
            GoToNextDialogScene(currentLineBlock.nextScene);
        }
        if (currentLineBlock.goToNextDayTime)
        {
            Debug.Log("go to next day time " + currentLineBlock.lines.Length + sceneManager.currentScene.name + " " + lineNumber);
            spriteManager.ClearCharacters();
            gameStructure.GoToNextDayTime();
        }
        //Debug.Log("LineBlockComplete");
        if (currentLineBlock.goToNextDay)
        {
            Debug.Log("go to next day " + currentLineBlock.lines.Length + sceneManager.currentScene.name + " " + lineNumber);
            //currentLineBlock = sceneManager.currentScene.lineBlocks[0];
            lineNumber = 0;
            changingDay = true;

            gameStructure.GoToNextDay();

            //GoToNextScene();
        }

    }

    public void GoToNextLineBlock(LineBlock nextLineBlock)
    {
        bool attitudeConditionsMet = true;
        bool actionConditionsMet = true;

        Debug.Log(lineNumber + " for start of gotonextlineblock");
        lineNumber = 0;
        Debug.Log(lineNumber + " for after of gotonextlineblock");

        //Attitude Conditions
        if (nextLineBlock.attitudeCondition || nextLineBlock.actionCondition)
        {
            Line.AttitudeEffects.AttitudesCharacter attitudeCharacter;
            float characterAttitudeLevel;
            
            //for wife attitude
            if(nextLineBlock.attitudes == Line.AttitudeEffects.AttitudesCharacter.wifeAttitude)
            {
                attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.wifeAttitude;
                characterAttitudeLevel = attitudeManager.wifeAttitudeLevel;

                //if condition is greater than
                if (nextLineBlock.attitudeLevel == LineBlock.AttitudeLevel.GreaterThan)
                {
                    if (characterAttitudeLevel > nextLineBlock.attitudeAmount)
                    {
                        attitudeConditionsMet = true;
                    }
                    else
                    {
                        attitudeConditionsMet = false;
                    }
                }
                //if condition is less than
                if (nextLineBlock.attitudeLevel == LineBlock.AttitudeLevel.LessThan)
                {
                    if (characterAttitudeLevel <= nextLineBlock.attitudeAmount)
                    {
                        attitudeConditionsMet = true;
                    }
                    else
                    {
                        attitudeConditionsMet = false;
                    }
                }
            }
            //for kids attitude
            if(nextLineBlock.attitudes == Line.AttitudeEffects.AttitudesCharacter.kidsAttitude)
            {
                attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.kidsAttitude;
                characterAttitudeLevel = attitudeManager.kidsAttitudeLevel;

                //if condition is more than
                if (nextLineBlock.attitudeLevel == LineBlock.AttitudeLevel.GreaterThan)
                {
                    if (characterAttitudeLevel > nextLineBlock.attitudeAmount)
                    {
                        attitudeConditionsMet = true;
                    }
                    else
                    {
                        attitudeConditionsMet = false;
                    }
                }
                //if condition is less than
                else
                {
                    if (characterAttitudeLevel <= nextLineBlock.attitudeAmount)
                    {
                        attitudeConditionsMet = true;
                    }
                    else
                    {
                        attitudeConditionsMet = false;
                    }
                }
            }
            //for yout attittude
            if(nextLineBlock.attitudes == Line.AttitudeEffects.AttitudesCharacter.youAttitude)
            {
                attitudeCharacter = Line.AttitudeEffects.AttitudesCharacter.youAttitude;
                characterAttitudeLevel = attitudeManager.youAttitudeLevel;

                //if conhdition is mnore than 
                if (nextLineBlock.attitudeLevel == LineBlock.AttitudeLevel.GreaterThan)
                {
                    if (characterAttitudeLevel > nextLineBlock.attitudeAmount)
                    {
                        attitudeConditionsMet = true;
                    }
                    else
                    {
                        attitudeConditionsMet = false;
                    }
                }
                // if condition is less than
                else
                {
                    if (characterAttitudeLevel <= nextLineBlock.attitudeAmount)
                    {
                        attitudeConditionsMet = true;
                    }
                    else
                    {
                        attitudeConditionsMet = false;
                    }
                }
            }

            for (int i = 0; i < nextLineBlock.actionArrayLength; i++)
            {

                int actionConditionNo = (int)nextLineBlock.actionArray[i].actionsCondition;
                if (actions.actionList[actionConditionNo] && nextLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.True)
                {
                    actionConditionsMet = true;
                }
                if (actions.actionList[actionConditionNo] && nextLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.False)
                {
                    attitudeConditionsMet = false;

                }
                if (!actions.actionList[actionConditionNo] && nextLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.True)
                {
                    attitudeConditionsMet = false;
                }
                if (!actions.actionList[actionConditionNo] && nextLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.False)
                {
                    actionConditionsMet = true;
                }
            }


        }

        if (actionConditionsMet && attitudeConditionsMet)
        {
            currentLineBlock = nextLineBlock;
            lineNumber = 0;

            typeWriting = false;
            //LineRunner();
            LineRunner();
        }
        else
        {
            if (nextLineBlock.ifFailedRunActivity)
            {
                ActivityRunner();
            }
            else
            {
                GoToNextLineBlock(nextLineBlock.ifFailedContinueTo);
            }
        }

    }

    public void GoToNextDialogScene(DialogScene nextScene)
    {
        for (int i = 0; i < sceneManager.scenes.Length; i++)
        {
            if (sceneManager.scenes[i].name == nextScene.name)
            {
                sceneManager.currentScene = nextScene;
                sceneManager.currentSceneNo = i;
                lineNumber = 0;
                GoToNextLineBlock(sceneManager.currentScene.lineBlocks[0]);
                //currentLineBlock = sceneManager.currentScene.lineBlocks[0];
                Debug.Log("panic");
                
            }
        }



        //lineNumber = 0;

        //LineRunner();

        

        //Attitude Conditions
        //if (currentLineBlock.actionCondition)
        //{
        //    Debug.Log("we're here");

        //    bool actionConditionsMet = false;

        //    for (int i = 0; i < currentLineBlock.actionArrayLength; i++)
        //    {
        //        Debug.Log("here we g0");
        //        int actionConditionNo = (int)currentLineBlock.actionArray[i].actionsCondition;
        //        if (actions.actionList[actionConditionNo] && currentLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.True)
        //        {
        //            Debug.Log("if action is true and it wants true");
        //            actionConditionsMet = true;
        //        }
        //        if (actions.actionList[actionConditionNo] && currentLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.False)
        //        {
        //            Debug.Log("if action is true and it want false");
        //            actionConditionsMet = false;

        //        }
        //        if (!actions.actionList[actionConditionNo] && currentLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.True)
        //        {
        //            Debug.Log("if action is false and wants true");
        //            actionConditionsMet = false;
        //        }
        //        if (!actions.actionList[actionConditionNo] && currentLineBlock.actionArray[i].trueOrFalse == LineBlock.ActionListElement.TrueOrFalse.False)
        //        {
        //            Debug.Log("if action is false and wants false");
        //            actionConditionsMet = true;
        //        }
        //        Debug.Log("didn't match anything here");
        //    }

        //    if (actionConditionsMet)
        //    {
        //        //currentLineBlock = nextLineBlock;
        //        Debug.Log("conditions met");
        //        lineNumber = 0;

        //        LineRunner();
        //    }
        //    else
        //    {

        //        //else
        //        //{
        //        Debug.Log("conditions failed so going to next");
        //        GoToNextLineBlock(currentLineBlock.ifFailedContinueTo);
        //        //}
        //    }
        //}
        //else
        //{
        //    lineNumber = 0;

        //    LineRunner();
        //}



        // LineRunner();

        //SetSceneInfo();

        spriteManager.ClearCharacters();
    } 

    //creates a decision block when called
    public void DecisionRunner()
    {
        //instantiates the decision speech block
        GameObject decisionBlock = Instantiate(decisionBlockPrefab) as GameObject;
        decisionBlock.transform.SetParent(content.transform, false);
        decisionBlock.name = "Decision" + decisionNumber.ToString();
        decisionBlock.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
        currentVisibleSpeech.Add(decisionBlock);

        

        

        //makes a decision button appear for each decision in list
        for (int i = 0; i < currentLineBlock.endDecisionBlock.decisions.Length; i++)
        {
            GameObject decision = Instantiate(decisionPrefab) as GameObject;
            decision.tag = "Decision";
            decision.transform.SetParent(decisionBlock.transform.Find("DecisionButtons"), false);
            decision.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLineBlock.endDecisionBlock.decisions[i].decisionName;

            NameCheck(decision.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            //adds type writer effect to button text
            TypeWriter(decision.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            decision.GetComponent<ButtonClicked>().decisionNumber = i;

            //sets the size of teh deicision buttons
            ResizeButton(decision);

            //Debug.Log(decision.transform.GetChild(1).GetComponent<TextMeshProUGUI>().textInfo.lineCount);
        }

        ResizeButtonBlock(decisionBlock.transform.Find("DecisionButtons").gameObject);

        ResizeButtonBlockBackground(decisionBlock);

        //activates the decision state
        decision = true;

        ResizeContent(decisionBlock.GetComponent<RectTransform>().sizeDelta.y);

    }

    public void ActivityRunner()

        
    {
       // Debug.Log("att actvity manager");

        if (gameStructure.isRunning)
        {

           // Debug.Log("made it past isrunning");
            //Debug.Log("Run Activity");



            activityManager.CreateActivityList();

            //instantiates the activity speech block
            GameObject activityBlock = Instantiate(activityBlockPrefab) as GameObject;
            activityBlock.transform.SetParent(content.transform, false);
            activityBlock.name = "Activity" + activityNumber.ToString();
            activityBlock.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionSpeechBubble, yPositionSpeechBubble);
            currentVisibleSpeech.Add(activityBlock);

            //makes a decision button appear for each decision in list
            for (int i = 0; i < activityManager.currentActivites.Count; i++)
            {
               // Debug.Log(activityManager.currentActivites.Count + SceneManager.GetActiveScene().name);

                GameObject activity = Instantiate(activityPrefab) as GameObject;
                activity.tag = "Activity";
                activity.transform.SetParent(activityBlock.transform.Find("ActivityButtons"), false);
                activity.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = activityManager.currentActivites[i].activityName;
                NameCheck(activity.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                //adds type writer effect to button text
                TypeWriter(activity.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                activity.GetComponent<ButtonClicked>().activityNumber = i;

                //sets the size of teh deicision buttons
                ResizeButton(activity);

                //Debug.Log(decision.transform.GetChild(1).GetComponent<TextMeshProUGUI>().textInfo.lineCount);
            }

            ResizeButtonBlock(activityBlock.transform.Find("ActivityButtons").gameObject);

            ResizeButtonBlockBackground(activityBlock);

            //activates the decision state
            //decision = true;

            ResizeContent(activityBlock.GetComponent<RectTransform>().sizeDelta.y);

            activityActive = true;
            activityChosen = false;
        }
 

    }

    //Formats Speech bubbles
    public void CheckCharacterFormat(GameObject speech)
    {
        TextMeshProUGUI speechText = speech.GetComponent<Transform>().Find("CharacterDialogText").GetComponent<TextMeshProUGUI>();
        GameObject speechBackground = speech.GetComponent<Transform>().gameObject;

        if (activityActive)
        {
            speechBackground.GetComponent<Image>().color = characterManager.descriptionColor;
            speechText.alignment = TextAlignmentOptions.Center;
        }
        else
        {
            if (currentLineBlock.lines[lineNumber].character == Line.Character.Description)
            {
                speechBackground.GetComponent<Image>().color = characterManager.descriptionColor;
                speechText.alignment = TextAlignmentOptions.Center;
                //FigureCharacterSprites(Line.Character character);

            }
            if (currentLineBlock.lines[lineNumber].character == Line.Character.You)
            {
                speechBackground.GetComponent<Image>().color = characterManager.youColor;
                GameObject speechTail = Instantiate(speechTailLeft) as GameObject;
                speechTail.transform.SetParent(speechBackground.transform, false);
                speechTail.GetComponent<Image>().color = characterManager.youColor;
                //FigureCharacterSprites(Line.Character character);
            }
            if (currentLineBlock.lines[lineNumber].character == Line.Character.Wife)
            {
                speechBackground.GetComponent<Image>().color = characterManager.wifeColor;
                GameObject speechTail = Instantiate(speechTailRight) as GameObject;
                speechTail.transform.SetParent(speechBackground.transform, false);
                speechTail.GetComponent<Image>().color = characterManager.wifeColor;
                spriteManager.FigureCharacterSprites(Line.Character.Wife);
            }
            if (currentLineBlock.lines[lineNumber].character == Line.Character.Boy)
            {
                speechBackground.GetComponent<Image>().color = characterManager.boyColor;
                GameObject speechTail = Instantiate(speechTailRight) as GameObject;
                speechTail.transform.SetParent(speechBackground.transform, false);
                speechTail.GetComponent<Image>().color = characterManager.boyColor;
                spriteManager.FigureCharacterSprites(Line.Character.Boy);
            }
            if (currentLineBlock.lines[lineNumber].character == Line.Character.Girl)
            {
                speechBackground.GetComponent<Image>().color = characterManager.girlColor;
                GameObject speechTail = Instantiate(speechTailRight) as GameObject;
                speechTail.transform.SetParent(speechBackground.transform, false);
                speechTail.GetComponent<Image>().color = characterManager.girlColor;
                spriteManager.FigureCharacterSprites(Line.Character.Girl);
            }
            if (currentLineBlock.lines[lineNumber].character == Line.Character.Dog)
            {
                speechBackground.GetComponent<Image>().color = characterManager.dogColor;
                GameObject speechTail = Instantiate(speechTailRight) as GameObject;
                speechTail.transform.SetParent(speechBackground.transform, false);
                speechTail.GetComponent<Image>().color = characterManager.dogColor;
                spriteManager.FigureCharacterSprites(Line.Character.Dog);
            }
            if (currentLineBlock.lines[lineNumber].character == Line.Character.Friend)
            {
                speechBackground.GetComponent<Image>().color = characterManager.friendColor;
                GameObject speechTail = Instantiate(speechTailRight) as GameObject;
                speechTail.transform.SetParent(speechBackground.transform, false);
                speechTail.GetComponent<Image>().color = characterManager.friendColor;
                spriteManager.FigureCharacterSprites(Line.Character.Friend);
            }
            if (currentLineBlock.lines[lineNumber].character == Line.Character.Lawyer)
            {
                speechBackground.GetComponent<Image>().color = characterManager.lawyerColor;
                GameObject speechTail = Instantiate(speechTailRight) as GameObject;
                speechTail.transform.SetParent(speechBackground.transform, false);
                speechTail.GetComponent<Image>().color = characterManager.lawyerColor;
                spriteManager.FigureCharacterSprites(Line.Character.Lawyer);
            }
        }
        
        

    }

    //resises the viewscreen content box to properly fit all the speech bubbles
    public void ResizeContent(float heightToAdd)
    {
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, contentHeight + heightToAdd);
        contentHeight = content.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void NameCheck(TextMeshProUGUI textToReplace)
    {
        
        textToReplace.text = textToReplace.text.Replace("WifeName", characterManager.wifeName);
        textToReplace.text = textToReplace.text.Replace("KidBoyName", characterManager.boyName);
        textToReplace.text = textToReplace.text.Replace("KidGirlName", characterManager.girlName);
        textToReplace.text = textToReplace.text.Replace("DogName", characterManager.dogName);
        textToReplace.text = textToReplace.text.Replace("FriendName", characterManager.friendName);
        textToReplace.text = textToReplace.text.Replace("HotDivorceLawyerName", characterManager.lawyerName);
        textToReplace.text = textToReplace.text.Replace("PlayerName", characterManager.youName);



        textToReplace.text = textToReplace.text.Replace("*", ",");
        
    }

    //  THE TYPE WRITER
    public void TypeWriter(TextMeshProUGUI text)
    {
        text.maxVisibleCharacters = 0;
        text.ForceMeshUpdate();
        StartCoroutine(TextVisible(text));
    }

    private IEnumerator TextVisible(TextMeshProUGUI text)
    {
        typeWriting = true;
        currentText = text;
        text.ForceMeshUpdate();

        while (text.textInfo.characterCount >= text.maxVisibleCharacters)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(characterTime);
            audioManager.PlaySound(1, 12);
        }

        text.maxVisibleCharacters = text.textInfo.characterCount;
        typeWriting = false;
    }

    IEnumerator DialogSkipper()
    {
        while (lineNumber < currentLineBlock.lines.Length)
        {
            SkipDialog();
            yield return new WaitForSeconds(skipSpeed);
        }

        FigureNext();
        //yield return null;
    }
}
