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
        currentDay++;
        currentDaySO = dayStructure[currentDay];
    }

    public void StartGame()
    {


        if (currentDaySO == null)
        {
            currentDaySO = dayStructure[currentDay];
        }

        LoadScene();


    }

    public void LoadScene()
    {
        SceneManager.LoadScene(currentDaySO.scene.name);
        //Debug.Log("load the scene");

        RunScene();

    }

    public void RunScene()
    {
        // if (isRunning)
        //{
        CallAssignSelfs();

        isRunning = true;


        //CallAssignSelfs();
        sceneStarter.StartSceneStarter();

        



      /*  if (currentDaySO.dayParts[dayTime].GetType() == typeof(ActivityBlock))
            {
                Debug.Log("RUN ACTIVITY");

            sceneStarter.ActivityRunner();
            // sceneStarter.ActivityRunner();

        }
        if (currentDaySO.dayParts[dayTime].GetType() == typeof(LineBlock))
        {
            Debug.Log("RUN LINE BLOCK");

            sceneStarter.LineRunner();

        } */
        //}


        //  if (currentDaySO.morning.GetType() == typeof()
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // Debug.Log(scene.name + mode);
    }

    public void GoToNextDayTime()
    {
        spriteManager.ClearCharacters();

        dayTime++;
        sceneManager.currentSceneNo++;
        sceneManager.currentScene = sceneManager.scenes[sceneManager.currentSceneNo];
        if (currentDaySO.dayParts[dayTime].GetType() == typeof(ActivityBlock))
        {
            Debug.Log("RUN ACTIVITY");
            sceneStarter = FindObjectOfType<SceneStarter>();
            sceneStarter.AssignSelf();

            sceneStarter.StartSceneStarter();
            sceneStarter.ActivityRunner();
        }
        if (currentDaySO.dayParts[dayTime].GetType() == typeof(DialogScene))
        {
            Debug.Log("RUN LINE BLOCK");
            sceneStarter = FindObjectOfType<SceneStarter>();
            sceneStarter.AssignSelf();

            //sceneManager.currentScene = currentDaySO.dayParts[dayTime].G;
            sceneStarter.StartSceneStarter();
            sceneStarter.LineRunner();
        }
    }

    public void GoToNextDay()
    {
        spriteManager.ClearCharacters();
        dayTime = 0;
        ProgressDay();
        StartGame();
        CallAssignSelfs();

    }

    public void CallAssignSelfs()
    {
        AssignSelf();
        sceneStarter = FindObjectOfType<SceneStarter>();
        sceneStarter.AssignSelf();
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




