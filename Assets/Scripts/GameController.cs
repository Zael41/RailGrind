using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static int pickupCount;
    public static bool[] itemsObtained;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu")
        {

        }
    }

    public void StartGame(GameObject levelSelect)
    {
        levelSelect.SetActive(true);
        GameObject mainMenu = GameObject.Find("MainMenu");
        mainMenu.SetActive(false);
        //SceneManager.LoadScene("Level1");
    }

    public void Settings(GameObject settingsMenu)
    {
        settingsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu(GameObject mainMenu)
    {
        mainMenu.SetActive(true);
        GameObject levelSelect = GameObject.Find("SelectMenu");
        levelSelect.SetActive(false);
    }

    public void LoadLevel(string levelNumber)
    {
        string nextLevel = "Level" + levelNumber;
        SceneManager.LoadScene(nextLevel);
    }
}
