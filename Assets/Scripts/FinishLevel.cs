using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
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

    public Material[] cupMaterials;

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

            string sceneName = SceneManager.GetActiveScene().name;
            int levelNumber = int.Parse(sceneName.Substring(sceneName.Length - 1));
            Debug.Log(GameController.pickupCount + " " + GameController.totalPickups[levelNumber - 1]);
            pickupsCollected.text = "Pickups collected: " + GameController.pickupCount + " / " + GameController.totalPickups[levelNumber - 1];
            Cursor.lockState = CursorLockMode.None;

            if (GameController.pickupCount == GameController.totalPickups[levelNumber - 1])
            {
                GameController.allItemsObtained[levelNumber - 1] = true;
            }

            int cupObtained = 0;

            if (timerUI.getElapsed() < GameController.authorTimes[levelNumber - 1])
            {
                cupObtained = 4;
            }
            else if (timerUI.getElapsed() < GameController.authorTimes[levelNumber - 1] + 5)
            {
                cupObtained = 3;
            }
            else if (timerUI.getElapsed() < GameController.authorTimes[levelNumber - 1] + 10)
            {
                cupObtained = 2;
            }
            else
            {
                cupObtained = 1;
            }

            if (cupObtained > GameController.allCupsObtained[levelNumber - 1]) GameController.allCupsObtained[levelNumber - 1] = cupObtained;
            GameObject levelCup = GameObject.Find("CupHigh");
            levelCup.GetComponent<MeshRenderer>().material = cupMaterials[cupObtained];
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
