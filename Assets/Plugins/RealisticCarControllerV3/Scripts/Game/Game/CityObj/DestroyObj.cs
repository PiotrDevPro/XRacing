﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
    
        if (col.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name != "city_online" && RCC_EnterExitCar.manage.isPlayerIn)
            {
                GetComponentInChildren<Rigidbody>().isKinematic = false;
                print("isPlayerCarDetectedTrue");
                Destroy(gameObject, 60f);
            } else if (SceneManager.GetActiveScene().name == "city_online") 
            {
                print("isPlayerCarDetectedTrue");
                Destroy(gameObject, 60f);
            }
                
        }

        if (col.gameObject.CompareTag("Player") && !RCC_EnterExitCar.manage.isPlayerIn)
        {
            if (SceneManager.GetActiveScene().name != "city_online" && !RCC_EnterExitCar.manage.isPlayerIn)
            {
                GetComponentInChildren<Rigidbody>().isKinematic = true;
                print("isPlayerFPSDetectedTrue");
            } else if (SceneManager.GetActiveScene().name == "city_online")
            {
                print("isPlayerCarDetectedTrue");
                Destroy(gameObject, 60f);
            }
        }
    }

}
