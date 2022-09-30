using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Palmmedia.ReportGenerator.Core.Common;
using System;

public class FinishLevel : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject finishCam;

    public GameObject gameplayUI;
    public GameObject finishUI;
    public TMP_Text finalTime;
    public TMP_Text pickupsCollected;

    public TimerUI timerUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("End of Level");
            finishCam.SetActive(true);
            playerCam.SetActive(false);
            gameplayUI.SetActive(false);
            finishUI.SetActive(true);

            string endTimer = timerUI.EndTimer();
            finalTime.text = "Final time: " + endTimer;

            pickupsCollected.text = "Pickups collected: " + GameController.pickupCount + " / 19";
            Cursor.lockState = CursorLockMode.None;

            string sceneName = SceneManager.GetActiveScene().name;
            int levelNumber = int.Parse(sceneName.Substring(sceneName.Length - 1));
            Debug.Log(GameController.pickupCount + " " + GameController.totalPickups[levelNumber - 1]);
            if (GameController.pickupCount == GameController.totalPickups[levelNumber - 1])
            {
                GameController.allItemsObtained[levelNumber - 1] = true;
            }
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextButton()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        currentLevel = currentLevel.Substring(currentLevel.Length - 1, 1);
        int x = Int32.Parse(currentLevel);
        string nextLevel = "Level" + (x + 1).ToString();
        SceneManager.LoadScene(nextLevel);
    }
}
