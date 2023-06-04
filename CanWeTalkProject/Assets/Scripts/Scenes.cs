using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public DialogScene[] scenes;
    // Start is called before the first frame update
    public AudioSource audioSource;
    public int currentSceneNo;
    public DialogScene currentScene;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
;        audioSource = GetComponent<AudioSource>();

        currentScene = scenes[currentSceneNo];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignSelf()
    {
        SceneStarter thisone = FindObjectOfType<SceneStarter>();
        thisone.sceneManager = this;
        ActivityManager thisotherone = FindObjectOfType<ActivityManager>();
        thisotherone.sceneManager = this;
       // Debug.Log("assign scenes");
    }

    void OnReloadScene()
    {
      //  Debug.Log("reloading the scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
