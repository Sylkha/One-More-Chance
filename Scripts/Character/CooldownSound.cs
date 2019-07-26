using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by CooldownSound (empty object)
public class CooldownSound : MonoBehaviour
{
    /// <summary>
    /// Audio --> cooldown of the skills
    /// </summary>
    [SerializeField]
    AudioClip CooldownAudio;

    /// <summary>
    /// AudioSource component
    /// </summary>
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        //Get components
        audio = GetComponent<AudioSource>();

        //Start Coroutines
        StartCoroutine(UpdateC());
    }
    private void Update()
    {
        //We mute the audio cus StopCoroutines doesnt seem to work
        if (GameController.instance.isRewinding)
        {
            audio.mute = true;
            StopCoroutine(UpdateC());
        }
    }

    public IEnumerator UpdateC()
    {
        while (true)
        {
            //Cooldown audio --> all the abilities
            if (GameController.instance.circleGo == true || GameController.instance.pyramidGo == true || GameController.instance.squareGo == true)
            {
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q)) || (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E)) || (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.W)))
                {
                    audio.clip = CooldownAudio;
                    audio.Play();
                    Debug.Log("Hay algo en cd");
                }
            }
            //Cooldown audio --> only for the sphere
            if (GameController.instance.circleGo2 == true)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q))
                {
                    audio.clip = CooldownAudio;
                    audio.Play();
                }
            }
            yield return null;
        }
    }
}
