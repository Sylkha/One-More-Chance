using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by the father of the breakable platforms
public class PlatformAudio : MonoBehaviour
{
    /// <summary>
    /// Audio of the platforms breaking
    /// </summary>
    [SerializeField]
    AudioClip BreakPlatformAudio;

    /// <summary>
    /// Audio reference
    /// </summary>
    AudioSource audio;

    private void Start()
    {
        //Get Components
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //The bool platformAudio goes true in the MainCharacter script
        if (GameController.instance.platformAudio == true)
        {
            audio.clip = BreakPlatformAudio;
            audio.Play();
            GameController.instance.platformAudio = false;
        }
    }
}
