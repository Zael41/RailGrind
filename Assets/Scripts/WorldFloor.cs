using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFloor : MonoBehaviour
{
    public Transform playerSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = playerSpawn.position;
        }
    }
}
