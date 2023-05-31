using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAssigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneStarter thisone = FindObjectOfType<SceneStarter>();
        thisone.content = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
