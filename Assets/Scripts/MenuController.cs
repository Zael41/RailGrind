using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject[] allPickupsObtained;
    public GameObject[] allCups;
    public Material[] cupMaterials;

    public void Start()
    {
        int i = 0;
        foreach (GameObject p in allPickupsObtained)
        {
            if (GameController.allItemsObtained[i] == true)
            {
                allPickupsObtained[i].SetActive(true);
            }
            else
            {
                allPickupsObtained[i].SetActive(false);
            }

            allCups[i].GetComponent<MeshRenderer>().material = cupMaterials[GameController.allCupsObtained[i]];
            i++;
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
