using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttitudeManager : MonoBehaviour
{


    [Header("Character Attitudes")]
    public float youAttitudeLevel;
    public float wifeAttitudeLevel;
    public float kidsAttitudeLevel;
    public float dogAttitudeLevel;
    public float friendAttitudeLevel;
    public float lawyerAttitudeLevel;
    public float principalAttitudeLevel;

    [Header("AttitudeChangeAmount")]
    public float megaLoss;
    public float highLoss;
    public float midLoss;
    public float lowLoss;
    public float lowGain;
    public float midGain;
    public float highGain;
    public float megaGain;

    [Header("Attitude Bar")]
    public Image youAttitudeBar;
    public Image wifeAttitudeBar;
    public Image kidsAttitudeBar;
    public Image dogAttitudeBar;
    public Image friendAttitudeBar;
    public Image lawyerAttitudeBar;

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
