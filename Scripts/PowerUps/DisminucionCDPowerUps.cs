using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisminucionCDPowerUps : MonoBehaviour
{
    /// <summary>
    /// Lo que se le suma
    /// </summary>
    [SerializeField]
    float plus = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Character")
        {
        // Disminuye el cooldown del player restandole plus

        GameController.instance.set_CDSphere(GameController.instance.Get_timeCd_Sphere() - plus);
        GameController.instance.set_CDSquare(GameController.instance.Get_timeCd_Square() - plus);
        GameController.instance.set_CDPyramid(GameController.instance.Get_timeCd_Pyramid() - plus);

        GameController.instance.powerUpAudio = true;

        gameObject.SetActive(false);

        }
    }
}
