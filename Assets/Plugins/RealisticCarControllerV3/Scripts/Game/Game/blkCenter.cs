﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blkCenter : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _block3_2;
    public GameObject bl_2_1;
    public GameObject bl_2_2;
    public GameObject people1;
    public GameObject people2;
    [Header("Optimizations")]
    [SerializeField] GameObject Small_city1;
    [SerializeField] GameObject Small_city2;
    [SerializeField] GameObject Small_city3;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            people1.SetActive(true);
         //   people2.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            Amplitude.Instance.logEvent("block2_right");
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                _main.SetActive(false);
                _block3_2.SetActive(false);
                bl_2_2.SetActive(true);
                bl_2_1.SetActive(false);
                Small_city1.SetActive(false);
                Small_city2.SetActive(false);
                Small_city3.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            people1.SetActive(true);
         //   people2.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                Small_city1.SetActive(false);
                Small_city2.SetActive(false);
                Small_city3.SetActive(false);
                _main.SetActive(false);
                _block3_2.SetActive(false);
                bl_2_2.SetActive(true);
                bl_2_1.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            people1.SetActive(false);
           // people2.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(true);
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                _main.SetActive(true);
                _block3_2.SetActive(false);
                bl_2_2.SetActive(false);
                bl_2_1.SetActive(false);
                Small_city1.SetActive(true);
                Small_city2.SetActive(true);
                Small_city3.SetActive(true);
            }
        }
    }
}
