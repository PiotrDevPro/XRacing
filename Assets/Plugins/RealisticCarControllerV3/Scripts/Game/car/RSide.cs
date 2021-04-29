using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSide : MonoBehaviour
{
    public static LSide manage;
    [SerializeField] Transform carPart;
    public int point = 50;
    public RCC_CarControllerV3 carController;

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

            if (point <= 40)
            {
                carPart.gameObject.SetActive(false);
            }
        }
    }
}
