using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is contained by the UI element Tiempo
public class TiempoVida : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Tiempo total del nivel
    /// </summary>
    public float tiempo_total = 27f;
    /// <summary>
    /// Texto que mostrara la cuenta atras del tiempo
    /// </summary>
    public Text texto;

    GameObject player;
    #endregion Variables

    void Start()
    {
        player = GameObject.Find("Character");
        texto.text = tiempo_total.ToString();
        StartCoroutine(CuentaAtras());
    }

    IEnumerator CuentaAtras()
    {
        for(float i= tiempo_total; i>=0; --i)
        {
            texto.text = i.ToString();
            if (i != 0)
                yield return new WaitForSeconds(1f);
            else if (i==0)
            {
                GameController.instance.isRewinding = true;
                yield return null;

            }           
        }
    }

}
