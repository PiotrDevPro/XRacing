using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationAI : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("OnTriggerEnter");

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("OnTriggerStay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
    }
}
