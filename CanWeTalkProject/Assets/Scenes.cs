using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public DialogScene[] scenes;
    // Start is called before the first frame update
    public AudioSource audioSource;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
;        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignSelf()
    {
        SceneStarter thisone = FindObjectOfType<SceneStarter>();
        thisone.sceneManager = this;
    }

    void OnReloadScene()
    {
        Debug.Log("reloading the scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
