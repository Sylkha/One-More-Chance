using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by the TriggerCurtain
public class Curtain : MonoBehaviour
{
    /// <summary>
    /// The curtain that will be activated.
    /// </summary>
    [SerializeField]
    GameObject curtain;

    /// <summary>
    /// When we cross the trigger, the curtain will be activated
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Character")
        {
            curtain.SetActive(true);
        }
    }
}
