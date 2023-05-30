using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public SceneStarter scene;
    public Actions actions;
    public Activity[] activities;

    public List<Activity> currentActivites;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("ActivityManager").Length == 1)
        {
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
           // Destroy(gameObject);
        }

        //CreateActivityList();
    }
    public void CreateActivityList()
    {

        currentActivites.Clear();

       // Debug.Log("the current scene is at the " + scene.currentScene.location);
       // Debug.Log("the current scene is on the " + scene.currentScene.dayType);
        

        for (int i = 0; i < activities.Length; i++)
        {
            bool location = false;
            bool action = false;
            bool dayType = false;
            bool dayTime = false;

            if (activities[i].locationArray != null)
            {
                for (int e = 0; e < activities[i].locationArrayLength; e++)
                {
                    if (activities[i].locationArray[e] == scene.currentScene.location)
                    {
                        location = true;
                       // Debug.Log(activities[i].locationArray[e] + "this is a house");
                    }
                }
            }
            if (activities[i].locationArray.Count == 0)
            {
                location = true;
              //  Debug.Log(activities[i].locationArray + "this one doesn't care");
            }

            if (activities[i].dayTypeArray != null)
            {
                for (int e = 0; e < activities[i].dayTypeArrayLength; e++)
                {
                    if(activities[i].dayTypeArray[e] == scene.currentScene.dayType)
                    {
                        dayType = true;
                       // Debug.Log(activities[i].dayTypeArray[e] + "this is on the weekend");
                    }
                }
            }
            if (activities[i].dayTypeArray.Count == 0)
            {
                dayType = true;
              //  Debug.Log(activities[i].dayTypeArray + "this one doesn't care what week type it is");
            }


            if (activities[i].dayTimeArray != null)
            {
                for (int e = 0; e < activities[i].dayTimeArrayLength; e++)
                {
                    if (activities[i].dayTimeArray[e] == scene.currentScene.dayTime)
                    {
                        dayTime = true;
                      //  Debug.Log(activities[i].dayTimeArray[e] + "is at night");
                    }
                }
            }
            if (activities[i].dayTimeArray.Count == 0)
            {
                dayTime = true;
               // Debug.Log(activities[i].dayTimeArray + "doens't care what time of day it is");
            }


            if (activities[i].actionArray != null)
            {

                   //REVISE THIS ONE
                for (int e = 0; e < activities[i].actionArrayLength; e++)
                {
                    if (actions.actionList[(int)activities[i].actionArray[e]])
                    {
                        action = true;
                    } 
                } 

                   action = true;
                  // Debug.Log("action true");
               }
               if (activities[i].actionArrayLength == 0)
               {
                   action = true;
                 //  Debug.Log("action true");
               }

              /* if (activites[i].dayTypeArray != null)
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
              //  Debug.Log("we made it");
                currentActivites.Add(activities[i]);


            }
        }
    }
}
