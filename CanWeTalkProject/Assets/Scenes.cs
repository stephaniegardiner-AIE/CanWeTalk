using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public Scene[] scenes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnReloadScene()
    {
        Debug.Log("reloading the scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
