using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStructure : MonoBehaviour
{
    public SceneStarter sceneStarter;
    public int daysAmount;
    public int currentDay = 0;
    public DayStructure currentDaySO;
    public List<DayStructure> dayStructure;
    // Start is called before the first frame update
    void Start()
    {

        if (GameObject.FindGameObjectsWithTag("GameStructure").Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            StartGame();
            Debug.Log("lets start the game");
        }
        else
        {
            Destroy(gameObject);

            Debug.Log("DESTROY");
        }

        
        


        Debug.Log("???");
    }

    // Update is called once per frame
    void Update()
    {

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
        LoadScene();
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
                
                LoadScene();
                Debug.Log("yo waht");
                RunScene();
            }
        }


    }

    public void LoadScene()
    {
        SceneManager.LoadScene(currentDaySO.scene.name);
        Debug.Log("load the scene");

        
        
    }

    public void RunScene()
    {
       // if (currentDaySO.morning.GetType() == typeof(ActivityBlock))
       // {
            Debug.Log("Run activity");
            sceneStarter.ActivityRunner();
       // }
    }

}




