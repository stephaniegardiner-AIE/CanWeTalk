using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// SOUND INDEX // ||||||||||||||||||||||||||||||||||||||||||||
//The sounds are randomly selected through an array of arrays that isolates categories and picks a random sound within the category.
// itemID is equal to the category of sound, the available IDs are as follows
// 0. Character Sounds                   > "DogTalking"
// 1. UI Sounds           > 1"Character Appear" 2"CourtSceneEnd" 3"NegativeReaction" 4"PCNegativeReaction" 5"PCPositiveReaction" 6"PositiveReaction" 7"NewDay" 8"NewScene" 9"ActivityChoose" 10"DecisionChoose" 11"NextLine1" 12"NextLine2" 13"NextLine3"



public class AudioClipArray
{
    public AudioClip[] itemCategory; //audioclip array of category
}
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioSource audioBGM;
    public AudioSource audioAmbient;
    public AudioClipArray[] soundLibrary; //an array of audio clip arrays containing all sound effects
    public AudioClip[] audioAmbienceSounds; //array of ambient sounds
    private int soundID; //the variable that will pick one of the variations, allows for randomisation

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioBGM.Play();
        PlayAmbience();
    }

    void PlayAmbience()
    {
        int random = Random.Range(0, audioAmbienceSounds.Length); //pick a random ambience sound
        audioAmbient.clip = audioAmbienceSounds[random]; //assign it as the clip
        audioAmbient.Play(); //play clip
        StartCoroutine(LoopAmbience(audioAmbient.clip.length)); //wait the length of the clip then loop
        IEnumerator LoopAmbience(float length) //loop
        {
            yield return new WaitForSeconds(length);
            PlayAmbience();
        }
    }

    //Call with 
    //PlaySound(itemID); //the itemID is the category of sounds listed above, soundID is the number in that category
    public void PlaySound(int itemID, int soundID)
    {
        audioSource.PlayOneShot(soundLibrary[itemID].itemCategory[soundID]); //play that sound
    }
}