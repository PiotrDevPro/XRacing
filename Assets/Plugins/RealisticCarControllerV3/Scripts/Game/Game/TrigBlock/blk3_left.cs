﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blk3_left : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _crossroad4;
    public GameObject _crossroad5;
    public GameObject _block3_1;
    public GameObject _block3_2;
    public GameObject people;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(false);
            _block3_1.SetActive(true);
            _block3_2.SetActive(true);
            people.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(true);
            _crossroad5.SetActive(true);
            Amplitude.Instance.logEvent("block3_left");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _main.SetActive(false);
            _block3_1.SetActive(true);
            _block3_2.SetActive(true);
            people.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(true);
            _crossroad5.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("Latency",0.5f);
        }
    }

    void Latency()
    {
        _main.SetActive(true);
        _block3_1.SetActive(false);
        _block3_2.SetActive(false);
        people.SetActive(false);
        _crossroad1.SetActive(true);
        _crossroad2.SetActive(true);
        _crossroad3.SetActive(false);
        _crossroad4.SetActive(false);
        _crossroad5.SetActive(false);
    }
}
