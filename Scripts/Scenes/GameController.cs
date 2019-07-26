using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script is contained by the GameController
public class GameController : MonoBehaviour
{
    #region singleton
    public static GameController instance = null;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion singleton

    #region Variables
    GameObject player;

    [SerializeField]
    float gravity = -8;

    [SerializeField]
    float timeAnim_Sphere = 0.3f;

    [SerializeField]
    float timeAnim_Square = 3;

    [SerializeField]
    float timeAnim_Pyramid = 5.15f;

    [SerializeField]
    float timeCd_Sphere = 0;

    [SerializeField]
    float timeCd_Square = 0.75f;

    [SerializeField]
    float timeCd_Pyramid = 2;

    /// <summary>
    /// Just to know if we are in the sphere form
    /// </summary>
    public bool circleGo = false;

    /// <summary>
    /// This bool is for the cooldown sound of the sphere
    /// </summary>
    public bool circleGo2 = false;

    /// <summary>
    /// Just to know if we are in the square form
    /// </summary>
    public bool squareGo = false;

    /// <summary>
    /// Just to know if we are in the pyramid form
    /// </summary>
    public bool pyramidGo = false;

    /// <summary>
    /// Just to know if we are rewinding
    /// </summary>
    public bool isRewinding = false;

    /// <summary>
    /// To control when we get a powerUp.
    /// </summary>
    public bool powerUpAudio = false;

    /// <summary>
    /// Control breakable platform audio
    /// </summary>
    public bool platformAudio = false;

    /// <summary>
    /// Condition --> 1 way to finish the game
    /// </summary>
    float contScene3 = 0;
    #endregion Variables

    private void Start()
    {
        player = GameObject.Find("Character");
    }

    private void Update()
    {
        player = GameObject.Find("Character");

        //We'll go to another scene after 3 tries --> final scene
        if(SceneManager.GetActiveScene().name == "Nivel 3" && contScene3 == 3)
        {
            SceneManager.LoadScene("Final");
        }

    }

    #region getters
    public GameObject Get_Player()
    {
        return player;
    }

    public float Get_Gravity()
    {
        return gravity;
    }

    public float Get_timeAnim_Sphere()
    {
        return timeAnim_Sphere;
    }

    public float Get_timeAnim_Square()
    {
        return timeAnim_Square;
    }

    public float Get_timeAnim_Pyramid()
    {
        return timeAnim_Pyramid;
    }

    public float Get_timeCd_Sphere()
    {
        return timeCd_Sphere;
    }

    public float Get_timeCd_Square()
    {
        return timeCd_Square;
    }

    public float Get_timeCd_Pyramid()
    {
        return timeCd_Pyramid;
    }

    public float Get_ContScene3()
    {
        return contScene3;
    }
    #endregion getters

    #region setters
    public void set_gravity(float _gravity)
    {
        gravity = _gravity;
        Physics.gravity = new Vector3(0, gravity, 0);
    }

    public void set_CDSphere(float _time)
    {
        timeCd_Sphere = _time;
    }
    public void set_CDSquare(float _time)
    {
        timeCd_Square = _time;
    }
    public void set_CDPyramid(float _time)
    {
        timeCd_Pyramid = _time;
    }
    public void set_ContScene3(float _cont)
    {
        contScene3 = _cont;
    }
    #endregion setters

}
