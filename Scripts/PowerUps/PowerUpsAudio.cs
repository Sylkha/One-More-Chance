using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by Power Ups (father)
public class PowerUpsAudio : MonoBehaviour
{
    AudioSource audio;

    [SerializeField]
    AudioClip powerupAudio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //The bool powerUpAudio goes true in the PowerUps scripts
        if(GameController.instance.powerUpAudio == true)
        {
            audio.clip = powerupAudio;
            audio.Play();
            GameController.instance.powerUpAudio = false;
        }
    }
}
