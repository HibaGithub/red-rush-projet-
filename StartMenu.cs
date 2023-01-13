using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public class StartMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public int level;
    public int nextLvl;

    void Start()
    {
        nextLvl = SceneManager.GetActiveScene().buildIndex + 1;
    }
    public void Quit()
    {
        //Exite game when the button clicked
        Application.Quit();
    }

    public void StartGame()
    {
        //start a new game
        level = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(level);
    }
}
