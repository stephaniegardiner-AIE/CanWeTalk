using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpriteLocation1Assigner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteManager thisone = FindObjectOfType<SpriteManager>();
        thisone.spriteLocation1 = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
