using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static int pickupCount;
    public static bool[] allItemsObtained = new bool[8];
    public static int[] allCupsObtained = new int[8] {0, 4, 3, 2, 4, 0, 1, 2};
    public static int[] totalPickups = new int[] { 19, 19, 25, 24, 30, 16, 22, 26 };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*if (scene.name != "MainMenu")
        {

        }*/
        pickupCount = 0;
        //Debug.Log(allItemsObtained[0]);
    }
}
