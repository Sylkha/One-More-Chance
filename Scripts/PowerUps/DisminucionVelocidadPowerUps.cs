using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisminucionVelocidadPowerUps : MonoBehaviour
{
    /// <summary>
    /// Lo que se le suma
    /// </summary>
    [SerializeField]
    float plus = 1f;

    private void OnTriggerEnter(Collider other)
    {
        // Aumenta la gravedad del player

        GameController.instance.set_gravity(GameController.instance.Get_Gravity() + plus);

        GameController.instance.powerUpAudio = true;

        gameObject.SetActive(false);

    }
}
