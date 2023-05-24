using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttitudeManager : MonoBehaviour
{


    public int wifeAttitude;
    public int youAttitude;
    public int childrenAttitude;
    public int dogAttitude;


    public float megaLoss;
    public float highLoss;
    public float midLoss;
    public float lowLoss;
    public float lowGain;
    public float midGain;
    public float highGain;
    public float megaGain;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
