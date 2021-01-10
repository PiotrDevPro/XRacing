using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blk2_left : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _block3_2;
    public GameObject bl_2_1;
    public GameObject bl_2_2;
    public GameObject people1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(false);
            _block3_2.SetActive(false);
            bl_2_1.SetActive(true);
            people1.SetActive(true);
            bl_2_2.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            Amplitude.Instance.logEvent("block2_left");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(false);
            _block3_2.SetActive(false);
            bl_2_1.SetActive(true);
            people1.SetActive(true);
            bl_2_2.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(true);
            _block3_2.SetActive(false);
            people1.SetActive(false);
            bl_2_1.SetActive(false);
            bl_2_2.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(true);
        }
    }
}
