using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blk1_left : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _block3_2;
    public GameObject _bl_1_1;
    public GameObject _bl_1_2;
    public GameObject people;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(false);
            _block3_2.SetActive(false);
            _bl_1_2.SetActive(false);
            _bl_1_1.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            people.SetActive(true);
            Amplitude.Instance.logEvent("block1_left");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(false);
            _bl_1_2.SetActive(false);
            _bl_1_1.SetActive(true);
            _block3_2.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            people.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(true);
            _block3_2.SetActive(false);
            _bl_1_2.SetActive(false);
            _bl_1_1.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            people.SetActive(false);
        }
    }
}
