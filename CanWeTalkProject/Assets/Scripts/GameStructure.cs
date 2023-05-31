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
            //StartGame();


        Debug.Log("???");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AssignSelf()
    {
        SceneStarter thisone = FindObjectOfType<SceneStarter>();
        thisone.gameStructure = this;
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
        //sceneStarter = GameObject.FindGameObjectWithTag("SceneStarter").GetComponent<SceneStarter>();
        //LoadScene();
    }

    public void StartGame()
    {
        

        Debug.Log("why");

        if (currentDaySO == null)
        {
            currentDaySO = dayStructure[currentDay];
            //sceneStarter = GameObject.FindGameObjectWithTag("SceneStarter").GetComponent<SceneStarter>();
            //Debug.Log(GameObject.FindGameObjectWithTag("SceneStarter"));

            if (SceneManager.GetActiveScene().name == currentDaySO.scene.name)
            {
                
                //LoadScene();
                Debug.Log("yo waht");
                RunScene();
            }
        }

        LoadScene();
        //gameStructure.gameRunning = true;

        


    }

    public void LoadScene()
    {
        SceneManager.LoadScene(currentDaySO.scene.name);
        Debug.Log("load the scene");
        
        RunScene();

    }

    public void RunScene()
    {
       // if (isRunning)
        //{
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
            gameStructure = FindObjectOfType<GameStructure>();
            gameStructure.AssignSelf();
            actions = FindObjectOfType<Actions>();
            actions.AssignSelf();


            if (currentDaySO.dayParts[dayTime].GetType() == typeof(ActivityBlock))
            {
                Debug.Log("Run activity");
                sceneStarter = FindObjectOfType<SceneStarter>();
                sceneStarter.AssignSelf();
                sceneStarter.StartSceneStarter();
            isRunning = true;
           // sceneStarter.ActivityRunner();

            }
        //}


      //  if (currentDaySO.morning.GetType() == typeof()
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // Debug.Log(scene.name + mode);
    }

    public void GoToAfternoon()
    {
        dayTime++;
        if (currentDaySO.dayParts[dayTime].GetType() == typeof(ActivityBlock))
        {
            Debug.Log("Run activity");
            sceneStarter = FindObjectOfType<SceneStarter>();
            sceneStarter.AssignSelf();
            sceneStarter.StartSceneStarter();
            sceneStarter.ActivityRunner();
        }
    }

}




