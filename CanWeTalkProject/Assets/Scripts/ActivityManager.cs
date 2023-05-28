using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public SceneStarter scene;
    public Actions actions;
    public Activity[] activites;

    public List<Activity> currentActivites;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        CreateActivityList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateActivityList()
    {
        Debug.Log(scene.currentScene.location);
        //currentActivites.Clear();

        for (int i = 0; i < activites.Length; i++)
        {
            bool location = false;
            bool action = false;
            bool dayType = false;
            bool dayTime = false;

            if (activites[i].locationArray != null)
            {
            

                for (int e = 0; e < activites[i].locationArrayLength; e++)
                {
                    if (activites[i].locationArray[e] == scene.currentScene.location)
                    {
                        location = true;
                        Debug.Log(activites[i].locationArray[e]);
                    }
                }
            }
            if (activites[i].locationArray == null)
            {
                location = true;
                Debug.Log(activites[i].locationArray);
            }

         /*   if (activites[i].actionArray != null)
            {

                //REVISE THIS ONE
                /*  for (int e = 0; e < activites[i].actionArrayLength; e++)
                  {
                      if (actions.actionList[(int)activites[i].actionArray[e]])
                      {
                          action = true;
                      } 
                  } 

                action = true;
                Debug.Log("action true");
            }
            if (activites[i].actionArray == null)
            {
                action = true;
                Debug.Log("action true");
            }

            if (activites[i].dayTypeArray != null)
            {
                
                for (int e = 0; e < activites[i].dayTypeArrayLength; e++)
                {
                    if (activites[i].dayTypeArray[e] == scene.currentScene.dayType)
                    {
                        dayType = true;
                        Debug.Log("daytype true");
                    }
                }
            }
            if (activites[i].dayTypeArray == null)
            {
                dayType = true;
                Debug.Log("daytype true");
            }

            if (activites[i].dayTimeArray != null)
            {
                
                for (int e = 0; e < activites[i].dayTimeArrayLength; e++)
                {
                    if (activites[i].dayTimeArray[e] == scene.currentScene.dayTime)
                    {
                        dayTime = true;
                        Debug.Log("daytime true");
                    }
                }
            }
            if (activites[i].dayTimeArray == null)
            {
                dayTime = true;
                Debug.Log("daytime true");
            }  */

            if (location && action && dayType && dayTime)

            {
                currentActivites.Add(activites[i]);


            }
        }
    }
}
