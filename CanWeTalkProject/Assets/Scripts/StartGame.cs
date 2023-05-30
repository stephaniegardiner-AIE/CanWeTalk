using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq;

public class StartGame : MonoBehaviour
{
    public CharacterManager characterManager;
    public SpriteManager spriteManager;
    public AttitudeManager attitudeManager;
    public ActivityManager activityManager;
    public Scenes sceneManager;
    public GameStructure gameStructure;
    public Actions actions;

    void Start()
    {
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
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("Day 1");
    }

    public void Test()
    {
    }

    public void NameCharacters()
    {
        SceneManager.LoadScene("CharacterNamer");
    }

}
