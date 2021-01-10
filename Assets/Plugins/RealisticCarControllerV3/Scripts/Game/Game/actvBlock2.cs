using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actvBlock2 : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _crossroad4;
    public GameObject _crossroad5;
    public GameObject _block3_1;
    public GameObject _block3_2;
    public GameObject _block3_3;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(false);
            _crossroad5.SetActive(false);
            _block3_1.SetActive(false);
            _block3_2.SetActive(false);
            _block3_3.SetActive(false);
            Amplitude.Instance.logEvent("fromStreets");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(false);
            _crossroad5.SetActive(false);
            _block3_1.SetActive(false);
            _block3_2.SetActive(false);
            _block3_3.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(false);
            _crossroad5.SetActive(false);
            _block3_1.SetActive(false);
            _block3_2.SetActive(false);
            _block3_3.SetActive(false);
        }
    }
}
