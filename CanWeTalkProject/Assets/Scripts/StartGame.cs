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

    void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        characterManager.AssignSelf();
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
