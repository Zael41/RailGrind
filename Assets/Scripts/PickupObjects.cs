using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    public TMP_Text pickupsText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameController.pickupCount++;
            pickupsText.text = GameController.pickupCount + " / " + "18";
            Destroy(this.gameObject);
        }
    }
}
