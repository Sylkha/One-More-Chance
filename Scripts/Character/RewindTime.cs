using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is contained by the one who rewinds --> MainCharacter
public class RewindTime : MonoBehaviour
{
    /// <summary>
    /// Positions stored
    /// </summary>
    List<Vector3> positions;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
    }

    private void FixedUpdate()
    {
        //If its rewinding it will call the function --> We call it turning the bool true in TiempoVida
        if (GameController.instance.isRewinding)
        {
            Rewind();            
        }
        //If its not rewinding, it will record the positions in the list
        else
            Record();
    }

    void Rewind()
    {
        //If there are enough points to rewind (more than 0 points)
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            //Removes the item at the given index
            if(positions.Count % 2 == 0 && positions.Count != 2)
            {
                for (byte i = 0; i < 4; i++)
                {
                    positions.RemoveAt(0);
                }
            }
            else if(positions.Count % 2 == 0 && positions.Count == 2)
            {
                for (byte i = 0; i < 2; i++)
                {
                    positions.RemoveAt(0);
                }
            }
            else if(positions.Count % 2 != 0 && (positions.Count != 3 || positions.Count != 1))
            {
                for (byte i = 0; i < 5; i ++)
                {
                    positions.RemoveAt(0);
                }
            }
            else if (positions.Count % 2 != 0 && positions.Count == 3)
            {
                for(byte i = 0; i < 3; i++)
                {
                    positions.RemoveAt(0);
                }
            }
            else if (positions.Count % 2 != 0 && positions.Count == 1)
            {
                positions.RemoveAt(0);
            }
        }
        //If there are 0 or less points, it'll stop rewinding
        else
            StopRewind();
    }

    void Record()
    {
        //It adds transform.position at the given index: 0
        positions.Insert(0, transform.position);
    }

    //When there are 0 points, (we are back at the start) the scene will be reload
    void StopRewind()
    {
        GameController.instance.isRewinding = false;
        ReloadLevel();
    }

    public void ReloadLevel()
    {
        //Part of the condition to finish the game. We control it in the Game Controller script
        if(SceneManager.GetActiveScene().name == "Nivel 3")
        {
            GameController.instance.set_ContScene3(GameController.instance.Get_ContScene3() + 1);
        }

        GameController.instance.set_gravity(-800);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
