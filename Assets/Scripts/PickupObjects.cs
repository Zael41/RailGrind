using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupObjects : MonoBehaviour
{
    public TMP_Text pickupsText;

    private string sceneName;
    private int levelNumber;
    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        levelNumber = int.Parse(sceneName.Substring(sceneName.Length - 1));
        pickupsText.text = GameController.pickupCount + " / " + GameController.totalPickups[levelNumber - 1];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.pickupCount++;
            pickupsText.text = GameController.pickupCount + " / " + GameController.totalPickups[levelNumber - 1];
            Destroy(this.gameObject);
        }
    }
}
