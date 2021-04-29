using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPTrunkTwo : MonoBehaviour
{
    public static DPTrunkTwo manage;
    public int point = 50;
    public RCC_CarControllerV3 carController;
    public Transform wheels1;
    public Transform wheels2;
    public Transform wheels1col;
    public Transform wheels2col;


    private void Awake()
    {
        manage = this;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CarAI") || col.CompareTag("Car"))
        {
            point -= 1;
            print(point);

            if (carController.speed > 50)
            {
                point -= 3;

            }
            if (carController.speed > 150)
            {
                point -= 8;
            }

            if (point <= 30)
            {
                wheels1.gameObject.SetActive(false);
                wheels1col.gameObject.SetActive(false);
            }

            if (point <= 15)
            {
                wheels2.gameObject.SetActive(false);
                wheels2col.gameObject.SetActive(false);
            }
        }
    }
}
