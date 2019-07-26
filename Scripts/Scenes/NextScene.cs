using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is contained by the final trigger in the level/scene
public class NextScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            if (SceneManager.GetActiveScene().name == "Nivel 1")
                SceneManager.LoadScene("Nivel 2");

            else if (SceneManager.GetActiveScene().name == "Nivel 2")
                SceneManager.LoadScene("Nivel 3");

            
            else if (SceneManager.GetActiveScene().name == "Nivel 3")
                SceneManager.LoadScene("Final");

        }
    }


    public void EmpezarJuego()
    {
        SceneManager.LoadScene("Nivel 1");

    }
}
