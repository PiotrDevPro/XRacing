using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CarControllerActive : MonoBehaviour
{
    private GameObject PlayerCarControl;
    public GameObject _carAiControl;
    public GameObject _carAiControl2;
    public GameObject _carAiControl3;

    void Start()
    {
        PlayerCarControl = GameObject.FindGameObjectWithTag("Player");
        PlayerCarControl.GetComponentInChildren<RCC_CarControllerV3>().enabled = true;
        if (SceneManager.GetActiveScene().name != "battle_online")
        {
            _carAiControl.GetComponent<RCC_CarControllerV3>().enabled = true;
            _carAiControl.GetComponent<RCC_AICarController>().enabled = true;
            _carAiControl2.GetComponent<RCC_CarControllerV3>().enabled = true;
            _carAiControl2.GetComponent<RCC_AICarController>().enabled = true;
            _carAiControl3.GetComponent<RCC_CarControllerV3>().enabled = true;
            _carAiControl3.GetComponent<RCC_AICarController>().enabled = true;
        }
    }
}
