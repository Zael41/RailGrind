using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextButton()
    {
        SceneManager.LoadScene("Level2");
    }
}
