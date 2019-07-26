using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by the camera
public class CameraMC : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Object that the camera will follow
    /// </summary>
    GameObject Player;

    /// <summary>
    /// Transform component
    /// </summary>
    Transform trCamera;

    /// <summary>
    /// Vector of the Camera
    /// </summary>
    Vector3 offset;

    /// <summary>
    /// In order to move the camera so the character is above the center of the camera
    /// </summary>
    public float delay = 2f;

    /// <summary>
    /// Grade of smooth
    /// </summary>
    [SerializeField]
    float smooth = 0.125f;

    /// <summary>
    /// AudioSource reference
    /// </summary>
    AudioSource audio;

    /// <summary>
    /// Clip for the One more time 
    /// </summary>
    [SerializeField]
    AudioClip OneMoreTime;

    /// <summary>
    /// We have an update, so we need to take control of how many times the audio (Ambient) is called
    /// </summary>
    bool audioB = false;

    /// <summary>
    /// We have an update, so we need to take control of how many times the audio (OneMoreTime) is called
    /// </summary>
    bool raudio = false;

    #endregion Variables

    void Start()
    { 
        trCamera = GetComponent<Transform>();
        Player = GameObject.Find("Character");
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = Player.transform.position + offset;
        float smoothPositionY = Mathf.Lerp(transform.position.y, desiredPosition.y - delay, smooth);
        float smoothPositionX = Mathf.Lerp(transform.position.x, desiredPosition.x, smooth);
        //The camera will follow the player and stay still on the Z axis
        trCamera.position = new Vector3(smoothPositionX, smoothPositionY, trCamera.position.z);
        //trCamera.position = new Vector3(trCamera.position.x, desiredPosition.y - delay, trCamera.position.z);

        if(GameController.instance.pyramidGo == true)
        {
            audio.mute = true;           
            audioB = true;
        }

        if(audioB == true)
        {
            audio.mute = false;
            audioB = false;
        }

        if(GameController.instance.isRewinding == true)
        {
            if (raudio == false)
            {
                audio.clip = OneMoreTime;
                audio.Play();
                raudio = true;
            }
        }

    }

}
