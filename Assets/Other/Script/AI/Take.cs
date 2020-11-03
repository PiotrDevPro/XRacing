using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take : MonoBehaviour
{
    public GameObject halfPoint;
    public GameObject finishPoint;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "CarAI")
        {
            finishPoint.SetActive(true);
            halfPoint.SetActive(false);
        }
    }
}
