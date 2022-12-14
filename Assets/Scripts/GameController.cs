using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static int pickupCount;
    public static bool[] allItemsObtained = new bool[8];
    public static int[] allCupsObtained = new int[8] {0, 0, 0, 0, 0, 0, 0, 0};
    public static int[] totalPickups = new int[8] { 19, 19, 25, 24, 30, 16, 22, 26 };
    public static int[] authorTimes = new int[8] { 24, 22, 16, 13, 35, 18, 43, 63 };
    public static float playerSensitivity = 10;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pickupCount = 0;
        if (scene.name != "MainMenu")
        {
            string sceneName = SceneManager.GetActiveScene().name;
            int levelNumber = int.Parse(sceneName.Substring(sceneName.Length - 1));
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (levelNumber > 1)
            {
                player.GetComponent<PlayerMovement>().dashAbility = true;
            }
            if (levelNumber > 4)
            {
                player.GetComponent<PlayerMovement>().doubleJumpAbility = true;
            }
        }
        //Debug.Log(allItemsObtained[0]);
    }

    public void OnSliderChanged(float value)
    {
        playerSensitivity = value;
    }
}
