using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindStart : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().RailGrind(this.gameObject);
        }
    }
}
