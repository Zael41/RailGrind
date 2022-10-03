using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMark : MonoBehaviour
{
    public GameObject[] pickupLights;
    bool activated;
    //bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        //waiting = false;
        pickupLights = GameObject.FindGameObjectsWithTag("PickupMark");
        foreach (GameObject light in pickupLights)
        {
            light.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ActivateLights();
        }
    }

    void ActivateLights()
    {
        foreach (GameObject light in pickupLights)
        {
            if (light != null) light.SetActive(!activated);
        }
        activated = !activated;
    }
}
