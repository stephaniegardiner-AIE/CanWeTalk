using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public InputField inputFieldWife;
    public InputField inputFieldSon;
    public InputField inputFieldDaughter;
    public InputField inputFieldDog;
    public InputField inputFieldFriend;
    public InputField inputFieldLawyer;
    public InputField inputFieldYou;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void AssignSelf()
    {
        SceneStarter thisone = FindObjectOfType<SceneStarter>();
        thisone.characterManager = this;
    }

    public void ChangeNameWife()
    {
        wifeName = inputFieldWife.text;
    }

    public void ChangeNameBoy()
    {
        boyName = inputFieldSon.text;
    }

    public void ChangeNameGirl()
    {
        girlName = inputFieldDaughter.text;
    }

    public void ChangeNameDog()
    {
        dogName = inputFieldDog.text;
    }

    public void ChangeNameFriend()
    {
        friendName = inputFieldFriend.text;
    }

    public void ChangeLawyerName()
    {
        lawyerName = inputFieldLawyer.text;
    }

    public void ChangeYouName()
    {
        youName = inputFieldYou.text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
