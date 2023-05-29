using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStructure : MonoBehaviour
{
    public int daysAmount;
    public List<DayStructure> dayStructure;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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

}




