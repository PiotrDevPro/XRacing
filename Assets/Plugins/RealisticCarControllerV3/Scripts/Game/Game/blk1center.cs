using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blk1center : MonoBehaviour
{
    public GameObject people1;
    public GameObject people2;
    [Header("Optim")]
    [SerializeField] GameObject from_main_block;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            people1.SetActive(true);
            people2.SetActive(true);
            Amplitude.Instance.logEvent("center1");
            from_main_block.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            people1.SetActive(true);
            people2.SetActive(true);
            from_main_block.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            people1.SetActive(false);
            people2.SetActive(false);
            from_main_block.SetActive(true);
        }
    }
}
