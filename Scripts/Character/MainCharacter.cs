using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is contained by the container of the character
public class MainCharacter : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Vector with the position of the mouse 
    /// </summary>
    Vector3 mousePosition;

    /// <summary>
    /// Rigidbody component
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// Grade of smooth: camera in axes X and Y
    /// </summary>
    [SerializeField]
    float smooth = 0.65f;

    /// <summary>
    /// Grade os smooth: movement in axis X
    /// </summary>
    [SerializeField]
    float smoothX = 0.125f;

    /// <summary>
    /// Force on X axis
    /// </summary>
    [SerializeField]
    float forceX = 1.2f;

    /// <summary>
    /// Jump height
    /// </summary>
    [SerializeField]
    float height = 50f;

    /// <summary>
    /// Gravity the Pyramids ability takes away
    /// </summary>
    [SerializeField]
    float grvPyramid = 50f;

    #region Sphere Color
    /// <summary>
    /// Sphere Renderer
    /// </summary>
    [SerializeField]
    Renderer rdSphere;  

    /// <summary>
    /// Sphere color
    /// </summary>
    [SerializeField]
    Color sphereC;

    /// <summary>
    /// Sphere color change --> pyramid ability
    /// </summary>
    [SerializeField]
    Color sphereCTime;
    #endregion Sphere Color

    #region Square Color
    /// <summary>
    /// Sphere Renderer
    /// </summary>
    [SerializeField]
    Renderer rdSquare;

    /// <summary>
    /// Sphere color
    /// </summary>
    [SerializeField]
    Color squareC;

    /// <summary>
    /// Sphere color change --> pyramid ability
    /// </summary>
    [SerializeField]
    Color squareCTime;
    #endregion Square Color

    #region Pyramid Color
    /// <summary>
    /// Sphere Renderer
    /// </summary>
    [SerializeField]
    Renderer rdPyramid;

    /// <summary>
    /// Sphere color
    /// </summary>
    [SerializeField]
    Color pyramidC;

    /// <summary>
    /// Sphere color change --> pyramid ability
    /// </summary>
    [SerializeField]
    Color pyramidCTime;
    #endregion Pyramid Color

    #region Audio
    /// <summary>
    /// Audiosource Component
    /// </summary>
    AudioSource audio;

    /// <summary>
    /// Audio of the Sphere animation (recolocation)
    /// </summary>
    [SerializeField]
    AudioClip SphereAudio;

    /// <summary>
    /// Sphere jump audio
    /// </summary>
    [SerializeField]
    AudioClip SJumpAudio;

    /// <summary>
    /// Audio of the Cube animation
    /// </summary>
    [SerializeField]
    AudioClip CubeAudio;

    /// <summary>
    /// Audio of the Pyramid animation
    /// </summary>
    [SerializeField]
    AudioClip PyramidAudio;
    #endregion Audio

    #region Anim
    /// <summary>
    /// Animator Component
    /// </summary>
    Animator anim;

    /// <summary>
    /// Canvas animator reference
    /// </summary>
    [SerializeField]
    Animator animCanvas;

    #endregion Anim
    #endregion Variables

    void Start()
    {
        //Get Components
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        //Set values
        Physics.gravity = new Vector3(0, GameController.instance.Get_Gravity(), 0);
            // R G B A --> we need 0 to 1 instead of 0 to 255
        rdSphere.material.color = sphereC;
        rdSquare.material.color = squareC;
        rdPyramid.material.color = pyramidC;

        //Start Coroutines
        StartCoroutine(Movement());
        StartCoroutine(UpdateSphere());
        StartCoroutine(UpdateSquare());
        StartCoroutine(UpdatePyramid());
    }

    #region Movement
    /// <summary>
    /// Basic Movement of the character (Free fall without gravity). We control the X axis
    /// </summary>
    /// <returns></returns>
    public IEnumerator Movement()
    {
        while (true)
        {
            //Movement axis: X
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float smoothPositionX = Mathf.Lerp(transform.position.x, mousePosition.x, smoothX);

            //Smooth movement axis X
            rb.AddForce(new Vector3((smoothPositionX) - transform.position.x, 0, 0).normalized * forceX, ForceMode.Force);
            yield return null;
        }
    }
    #endregion Movement

    #region Update
    private void Update()
    {
        //We try to stop all coroutines so the player cant use the skills or movement while its rewinding, and in turn, that the audio doesnt sound
        if (GameController.instance.isRewinding)
        {
            audio.mute = true;
            StopCoroutine(Movement());
            StopCoroutine(UpdateSphere());
            StopCoroutine(UpdateSquare());
            StopCoroutine(UpdatePyramid());
        }
    }

    public IEnumerator UpdateSphere()
    {
        while (true)
        {
            #region Conditions CD from other skills
            //Square anim + cd time
            if (GameController.instance.squareGo == true)
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Square() + GameController.instance.Get_timeCd_Square());
            //Pyramid anim + cd time
            else if (GameController.instance.pyramidGo == true)
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Pyramid() + GameController.instance.Get_timeCd_Pyramid());
            #endregion Conditions CD from other skills

            //Circle skill (0 --> left click)
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q))
            {
                Circle();
                //Sphere Animation
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Sphere());
                Sphere_Sphere();

                //Sphere cd time
                yield return new WaitForSeconds(GameController.instance.Get_timeCd_Sphere());
                GameController.instance.circleGo2 = false;               
            }
            yield return null;
        }
    }

    public IEnumerator UpdateSquare()
    {
        while (true)
        {
            #region Conditions CD from other skills
            //Sphere anim time
            if (GameController.instance.circleGo == true) 
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Sphere()); 
            //Pyramid anim + cd time
            if (GameController.instance.pyramidGo == true) //Este es el tiempo de cd de la habilidad + duración de la anim
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Pyramid() + GameController.instance.Get_timeCd_Pyramid());
            #endregion Conditions CD from other skills

            //Square skill (1 --> right click)
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E))
            {
                Square();
                //Square anim 
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Square());
                Cube_Sphere();

                //Square cd time
                yield return new WaitForSeconds(GameController.instance.Get_timeCd_Square());
                GameController.instance.squareGo = false;
                animCanvas.SetInteger("Cube", 0);
            }
            yield return null;
        }
    }

    public IEnumerator UpdatePyramid()
    {
        while (true)
        {
            #region Conditions CD from other skills
            //Sphere anim time
            if (GameController.instance.circleGo == true) 
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Sphere());
            //Square anim + cd time
            if (GameController.instance.squareGo == true)
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Square() + GameController.instance.Get_timeCd_Square());
            #endregion Conditions CD from other skills

            //Pyramid skill (2 --> central click)
            else if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.W))
            {
                Pyramid();
                //Pyramid Animation
                yield return new WaitForSeconds(GameController.instance.Get_timeAnim_Pyramid());
                Pyramid_Sphere();

                //Pyramid cd time
                yield return new WaitForSeconds(GameController.instance.Get_timeCd_Pyramid());
                GameController.instance.pyramidGo = false;
                animCanvas.SetInteger("Pyramid", 0);
            }
            yield return null;
        }
    }
    #endregion Update

    #region skills
    void Circle()
    {
        GameController.instance.circleGo = true;
        GameController.instance.circleGo2 = true;
        audio.clip = SJumpAudio;
        audio.Play();
        animCanvas.SetInteger("Sphere", 1);
        rb.AddForce(Vector3.up * height, ForceMode.Impulse);
    }

    void Square()
    {
        GameController.instance.squareGo = true;
        audio.clip = CubeAudio;
        audio.Play();
        anim.SetInteger("Cube", 1);
        animCanvas.SetInteger("Cube", 2);
        //animCanvas.SetInteger("")
    }

    void Pyramid()
    {
        GameController.instance.pyramidGo = true;

        audio.clip = PyramidAudio;
        audio.Play();

        anim.SetInteger("Pyramid", 2);
        animCanvas.SetInteger("Pyramid", 3);
        //Color change
        rdSphere.material.color = sphereCTime;
        rdSquare.material.color = squareCTime;
        rdPyramid.material.color = pyramidCTime;

        GameController.instance.set_gravity(GameController.instance.Get_Gravity() + grvPyramid);
        Physics.gravity = new Vector3(0, GameController.instance.Get_Gravity(), 0);
    }
    #endregion skills

    #region return from skills
    void Sphere_Sphere()
    {
        GameController.instance.circleGo = false;
        animCanvas.SetInteger("Sphere", 0);
    }

    void Cube_Sphere()
    {
        audio.clip = SphereAudio;
        audio.Play();
        anim.SetInteger("Cube", 0);
    }

    void Pyramid_Sphere()
    {
        audio.clip = SphereAudio;
        audio.Play();
        anim.SetInteger("Pyramid", 0);
        //Original colors
        rdSphere.material.color = sphereC;
        rdSquare.material.color = squareC;
        rdPyramid.material.color = pyramidC;

        GameController.instance.set_gravity(GameController.instance.Get_Gravity() - 5f);
        Physics.gravity = new Vector3(0, GameController.instance.Get_Gravity(), 0);
    }

    #endregion return from skills

    #region destroy platform --> square skill
    //We need the platform tagged
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "BreakablePlatform" && GameController.instance.squareGo == true)
        {
            GameController.instance.squareGo = false;
            other.gameObject.SetActive(false);
            GameController.instance.platformAudio = true;
        }
    }

    #endregion destroy platform

}
