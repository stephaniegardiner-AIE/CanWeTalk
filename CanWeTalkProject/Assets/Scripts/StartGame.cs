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

    void Start()
    {

    }

    public void NameCharacters()
    {
        Debug.Log("Y");
        SceneManager.LoadScene("CharacterNamer");
    }

}
