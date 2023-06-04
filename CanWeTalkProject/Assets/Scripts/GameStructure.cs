using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStructure : MonoBehaviour
{
    public bool isRunning;
    public SceneStarter sceneStarter;
    public int daysAmount;
    public int currentDay = 0;
    public int dayTime;
    public DayStructure currentDaySO;
    public List<DayStructure> dayStructure;

    public CharacterManager characterManager;
    public SpriteManager spriteManager;
    public AttitudeManager attitudeManager;
    public ActivityManager activityManager;
    public Scenes sceneManager;
    public GameStructure gameStructure;
    public Actions actions;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
       // Debug.Log("???");
    }

    public void AssignSelf()
    {
        SceneStarter thisone = FindObjectOfType<SceneStarter>();
        thisone.gameStructure = this;
       // Debug.Log("assign game structure");
    }
    private void OnAwake()
    {
        if (dayStructure == null)
        {
            dayStructure = new List<DayStructure>(new DayStructure[daysAmount]);
            Debug.Log("Awake");

            for (int i = 0; i < daysAmount; i++)
            {
                //create new data object
                var tmp = new DayStructure();

                //store the Data object in our dataArray
                dayStructure[i] = tmp;
            }
        }

    }

    public void ProgressDay()
    {

    }

    public void StartGame()
    {
        isRunning = false;

        if (currentDaySO == null)
        {
            Debug.Log(currentDaySO);
            currentDaySO = dayStructure[currentDay];
        }

        LoadScene();


    }

    public void LoadScene()
    {
        SceneManager.LoadScene(currentDaySO.scene.name);
        //Debug.Log("load the scene");
       // CallAssignSelfs();
        RunScene();

    }

    public void RunScene()
    {
        // if (isRunning)
        //{
        isRunning = true;

        CallAssignSelfs();

        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // Debug.Log(scene.name + mode);
    }

    public void GoToNextDayTime()
    {
        Debug.Log("GO TO NEXT DAY TIME");



        dayTime++;

        if (dayTime > 2)
        {
            GoToNextDay();
        }
        else
        {
            spriteManager.ClearCharacters();

            if (currentDaySO.dayParts[dayTime].GetType().ToString() == "DialogScene")
            {
                Debug.Log(currentDaySO.dayParts[dayTime].GetType());
                sceneManager.currentScene = (DialogScene)currentDaySO.dayParts[dayTime];

                for (int i = 0; i < sceneManager.scenes.Length; i++)
                {
                    if (sceneManager.scenes[i].name == sceneManager.currentScene.name)
                    {
                        //sceneManager.currentScene = nextScene;
                        sceneManager.currentSceneNo = i;
                    }
                    else
                    {
                        Debug.Log("could not find match for " + sceneManager.currentScene.name);
                    }
                }


            }
            else
            {
                Debug.Log(currentDaySO.dayParts[dayTime].GetType());

            }

            //
            //sceneManager.currentSceneNo++;
            //sceneManager.currentScene = sceneManager.scenes[sceneManager.currentSceneNo];
            if (currentDaySO.dayParts[dayTime].GetType() == typeof(ActivityBlock))
            {
                //sceneManager.currentSceneNo = 0;
                //sceneManager.currentScene = sceneManager.scenes[0];


                Debug.Log("RUN ACTIVITY");
                sceneStarter = FindObjectOfType<SceneStarter>();
                //sceneStarter.AssignSelf();

                //sceneStarter.currentLineBlock = null;
                sceneStarter.lineNumber = 0;

                //sceneStarter.StartSceneStarter();
                sceneStarter.SetSceneInfo();
                sceneStarter.ActivityRunner();
            }
            if (currentDaySO.dayParts[dayTime].GetType() == typeof(DialogScene))
            {
                Debug.Log("RUN LINE BLOCK");
                sceneStarter = FindObjectOfType<SceneStarter>();
                //sceneStarter.AssignSelf();

                sceneStarter.lineNumber = 0;

                //sceneManager.currentScene = currentDaySO.dayParts[dayTime].G;
                sceneStarter.StartSceneStarter();
                sceneStarter.SetSceneInfo();
                sceneStarter.LineRunner();
            }
        }

        
    }

    public void GoToNextDay()
    {

        Debug.Log("GO TO NEXT DAY");

        spriteManager.ClearCharacters();
        dayTime = 0;


        currentDay++;
        currentDaySO = dayStructure[currentDay];


        sceneManager.currentScene = (DialogScene)currentDaySO.dayParts[dayTime];

        for (int i = 0; i < sceneManager.scenes.Length; i++)
        {
            if (sceneManager.scenes[i].name == sceneManager.currentScene.name)
            {
                sceneManager.currentSceneNo = i;
            }
        }

        StartGame();
        

    }

    public void CallAssignSelfs()
    {
        Debug.Log(sceneStarter.gameStructure);
        AssignSelf();
        //sceneStarter = FindObjectOfType<SceneStarter>();
        //sceneStarter.AssignSelf();
        characterManager = FindObjectOfType<CharacterManager>();
        characterManager.AssignSelf();
        spriteManager = FindObjectOfType<SpriteManager>();
        spriteManager.AssignSelf();
        attitudeManager = FindObjectOfType<AttitudeManager>();
        attitudeManager.AssignSelf();
        activityManager = FindObjectOfType<ActivityManager>();
        activityManager.AssignSelf();
        sceneManager = FindObjectOfType<Scenes>();
        sceneManager.AssignSelf();
        actions = FindObjectOfType<Actions>();
        actions.AssignSelf();
    }
}




