using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    // Start is called before the first frame update
    public void Retry()
    {
        GlobalVars.clearedFloors = 0;
        GlobalVars.floor = 0;
        SceneManager.LoadScene("Level");
    }

    // Quit Game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application QUIT");
    }
}
