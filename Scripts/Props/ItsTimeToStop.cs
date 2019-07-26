using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by the Moving Platforms
public class ItsTimeToStop : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Renderer reference
    /// </summary>
    Renderer rd;

    /// <summary>
    /// Original color of the moving platform
    /// </summary>
    [SerializeField]
    Color MovingPlatformC;

    /// <summary>
    /// Color of the moving platform changed cus of the Pyramids ability
    /// </summary>
    [SerializeField]
    Color MPCTime;

    /// <summary>
    /// When the rewind is over
    /// </summary>
    bool rewind = false;

    #endregion Variables

    void Start()
    {
        //Get Components
        rd = GetComponent<Renderer>();

        //Set values
        rd.material.color = MovingPlatformC;

        //Start Coroutines
        StartCoroutine(UpdateC());
    }

    void Update()
    {
        //Stop animations and go trigger
        if (GameController.instance.isRewinding == true)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<Collider>().isTrigger = true;
            rewind = true;
        }
        if(GameController.instance.isRewinding == false && rewind == true)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider>().isTrigger = false;
            rewind = false;
        }

    }

    IEnumerator UpdateC()
    {
        while (true)
        {
            //Sphere anim time
            if (GameController.instance.circleGo == true)
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Sphere());
            //Square anim + cd time
            if (GameController.instance.squareGo == true)
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Square() + GameController.instance.Get_timeCd_Square());

            //Stop animations and change colors --> Pyramids ability
            if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.W))
            {
                GetComponent<Animator>().enabled = false;
                rd.material.color = MPCTime;
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Pyramid());
                GetComponent<Animator>().enabled = true;
                rd.material.color = MovingPlatformC;
                yield return new WaitForSeconds(GameController.instance.Get_timeCd_Pyramid());
            }
            yield return null;
        }
    }
}
