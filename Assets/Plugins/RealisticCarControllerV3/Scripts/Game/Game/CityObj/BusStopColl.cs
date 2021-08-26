using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStopColl : MonoBehaviour
{

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            print("isPlayerDetect");
            GetComponentInChildren<Rigidbody>().isKinematic = false;
        }
    }
}
