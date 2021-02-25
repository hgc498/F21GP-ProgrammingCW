//refrenced from: https://www.youtube.com/watch?v=Iv7A8TzreY4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    //this method uses the build index to load the next level
    //so that once a level is complete it continues to the nest level
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
