using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blk2 : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _block3_2;
    public GameObject people1;
    public GameObject people2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(true);
            _block3_2.SetActive(false);
            people1.SetActive(true);
            people2.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(true);
            Amplitude.Instance.logEvent("center2");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(true);
            _block3_2.SetActive(false);
            people1.SetActive(true);
            people2.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(true);
         //   print("OnTriggerStay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(true);
            _block3_2.SetActive(false);
            people1.SetActive(false);
            people2.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
          //  print("OnTriggerExit");
        }
    }
}

