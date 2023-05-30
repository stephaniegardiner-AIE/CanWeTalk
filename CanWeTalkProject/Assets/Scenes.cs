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
        if (GameObject.FindGameObjectsWithTag("SceneManager").Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
;        audioSource = GetComponent<AudioSource>();
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
