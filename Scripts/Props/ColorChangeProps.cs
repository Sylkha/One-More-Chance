using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by the ColorManager
public class ColorChangeProps : MonoBehaviour
{
#region Variables

    #region Background
    /// <summary>
    /// Renderer reference --> Background
    /// </summary>
    [SerializeField]
    Renderer[] rdBackg;

    /// <summary>
    /// Original color of the Background
    /// </summary>
    [SerializeField]
    Color BackgroundC;

    /// <summary>
    /// Color of the Background changed cus of the Pyramids ability
    /// </summary>
    [SerializeField]
    Color BCTime;
    #endregion Background

    #region Walls
    /// <summary>
    /// Renderer reference --> Walls
    /// </summary>
    [SerializeField]
    Renderer[] rdWalls;

    /// <summary>
    /// Original color of the Walls
    /// </summary>
    [SerializeField]
    Color WallsC;

    /// <summary>
    /// Color of the Walls changed cus of the Pyramids ability
    /// </summary>
    [SerializeField]
    Color WCTime;
    #endregion Walls

    #region Platforms
    /// <summary>
    /// Renderer reference --> Platforms
    /// </summary>
    [SerializeField]
    Renderer[] rdPlatforms;

    /// <summary>
    /// Original color of the Platforms
    /// </summary>
    [SerializeField]
    Color PlatformsC;

    /// <summary>
    /// Color of the Platforms changed cus of the Pyramids ability
    /// </summary>
    [SerializeField]
    Color PCTime;
    #endregion Platforms

    #region PowerUps
    /// <summary>
    /// Renderer reference --> PowerUps
    /// </summary>
    [SerializeField]
    Renderer[] rdPowerUps;

    /// <summary>
    /// Original color of the PowerUps
    /// </summary>
    [SerializeField]
    Color PowerUpsC;

    /// <summary>
    /// Color of the PowerUps changed cus of the Pyramids ability
    /// </summary>
    [SerializeField]
    Color PUCTime;

    /// <summary>
    /// Renderer reference -->  (Transparent parts)
    /// </summary>
    [SerializeField]
    Renderer[] rdPUTransparents;

    //Original color of the PowerUps
    [SerializeField]
    Color PUTransparentsC;

    //Color changes cus of the Pyramids ability
    [SerializeField]
    Color PUTCTime;
    #endregion PowerUps

    #endregion Variables

    void Start()
    {
        //Set values
        for(byte i = 0; i < rdBackg.Length; i++)
            rdBackg[i].material.color = BackgroundC;

        for (byte i = 0; i < rdWalls.Length; i++)
            rdWalls[i].material.color = WallsC;

        for (byte i = 0; i < rdPlatforms.Length; i++)
            rdPlatforms[i].material.color = PlatformsC;

        for (byte i = 0; i < rdPowerUps.Length; i++)        
            rdPowerUps[i].material.color = PowerUpsC;

        for (byte i = 0; i < rdPUTransparents.Length; i++)
            rdPUTransparents[i].material.color = PUTransparentsC;

        //Start Coroutines
        StartCoroutine(UpdateC());
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

            if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.W))
            {
                //Colors changed --> Pyramids ability
                for (byte i = 0; i < rdBackg.Length; i++)
                    rdBackg[i].material.color = BCTime;

                for (byte i = 0; i < rdWalls.Length; i++)               
                    rdWalls[i].material.color = WCTime;
                
                for (byte i = 0; i < rdPlatforms.Length; i++)               
                    rdPlatforms[i].material.color = PCTime;
                
                for (byte i = 0; i < rdPowerUps.Length; i++)                
                    rdPowerUps[i].material.color = PUCTime;

                for (byte i = 0; i < rdPUTransparents.Length; i++)
                    rdPUTransparents[i].material.color = PUTCTime;

                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Pyramid());
                //Original colors
                for (byte i = 0; i < rdBackg.Length; i++)
                    rdBackg[i].material.color = BackgroundC;

                for (byte i = 0; i < rdWalls.Length; i++)
                    rdWalls[i].material.color = WallsC;
                
                for (byte i = 0; i < rdPlatforms.Length; i++)                
                    rdPlatforms[i].material.color = PlatformsC;
                
                for (byte i = 0; i < rdPowerUps.Length; i++)               
                    rdPowerUps[i].material.color = PowerUpsC;

                for (byte i = 0; i < rdPUTransparents.Length; i++)
                    rdPUTransparents[i].material.color = PUTransparentsC;
                yield return new WaitForSeconds(GameController.instance.Get_timeCd_Pyramid());
            }
            yield return null;
        }
    }
}
