using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPoints : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, .15f);
    }
}
