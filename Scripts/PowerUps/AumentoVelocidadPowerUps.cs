using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentoVelocidadPowerUps : MonoBehaviour
{
    /// <summary>
    /// Lo que se le resta
    /// </summary>
    [SerializeField]
    float plus = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            // Disminuye la gravedad del player restandole el plus       
            GameController.instance.set_gravity(GameController.instance.Get_Gravity() - plus);

            GameController.instance.powerUpAudio = true;

            gameObject.SetActive(false);
        }
        
    }
}
