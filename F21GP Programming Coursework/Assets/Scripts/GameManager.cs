//refrenced from: https://www.youtube.com/watch?v=Iv7A8TzreY4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //bool gameHasEnded = false;

    //public float restartDelay = 1f;

    public GameObject levelUI;

    //if the level is completed then it will set the level complete screen to active
    //completing the panles animation and moving to the next level
    public void CompleteLevel()
    {
        //TODO: make it so that can only continue if the player has killed all enemies 
        levelUI.SetActive(true);
        Debug.Log("Level Complete");
    }

    
}
