using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Description")]
    public Color descriptionColor;

    [Header("You")]
    public string youName;
    public Color youColor;

    [Header("Wife")]
    public string wifeName;
    public Color wifeColor;

    [Header("Boy")]
    public string boyName;
    public Color boyColor;

    [Header("Girl")]
    public string girlName;
    public Color girlColor;

    [Header("Dog")]
    public string dogName;
    public Color dogColor;

    [Header("Friend")]
    public string friendName;
    public Color friendColor;

    [Header("Lawyer")]
    public string lawyerName;
    public Color lawyerColor;

    [Header("Principal")]
    public string principalName;
    public Color principalColor;

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
