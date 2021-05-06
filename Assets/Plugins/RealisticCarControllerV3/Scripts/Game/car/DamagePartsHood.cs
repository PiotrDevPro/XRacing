using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePartsHood : MonoBehaviour
{
    public static DamagePartsHood manage;
    [SerializeField] Transform carPart;
    public Transform wheels1;
    public Transform wheels2;
    public Transform wheels1col;
    public Transform wheels2col;
    public GameObject blow;

    public int point = 50;

    public RCC_CarControllerV3 carController;

    private void Awake()
    {
        manage = this;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CarAI") || col.CompareTag("Car"))
        {
             point -= 1;

             if (carController.speed > 50)
              {
                 point -= 3;
                  
             }
             if (carController.speed > 150)
              {
                 point -= 8;
             }

            if (point <= 45)
            {
                if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 1 || PlayerPrefs.GetInt("CurrentCar") == 2 || PlayerPrefs.GetInt("CurrentCar") == 7)
                {
                    carPart.gameObject.SetActive(false);
                }
            }
            if (point <= 44)
            {
                wheels1.gameObject.SetActive(false);
                wheels1col.gameObject.SetActive(false);
            }

            if (point <= 43)
            {
                wheels2.gameObject.SetActive(false);
                wheels2col.gameObject.SetActive(false);
                blow.SetActive(true);
            }
        }
    }
}
