//refrenced from: https://www.youtube.com/watch?v=Iv7A8TzreY4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager gameManager;

    //This is linked to an end object that when the player enters it
    //the code will complete the method in the gamemanager code that completes the level
    void OnTriggerEnter()
    {
        gameManager.CompleteLevel();
    }
}
