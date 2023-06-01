using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAssigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteManager thisone = FindObjectOfType<SpriteManager>();
        thisone.canvas = gameObject;
        //gameObject.Find
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
