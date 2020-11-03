using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameobjectTagTest : MonoBehaviour
{
    private GameObject car;

    void Start()
    {
        car = GameObject.FindGameObjectWithTag("PlayerCar");

        if (car == null)
        {
            print("Null");
        }
        else
        {
            print("PlayerCar");

        }
    }

    void Update()
    {
        
    }
}
