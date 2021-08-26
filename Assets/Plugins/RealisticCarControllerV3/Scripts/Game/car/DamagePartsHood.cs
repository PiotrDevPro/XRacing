using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePartsHood : MonoBehaviour
{
    public static DamagePartsHood manage;
    [SerializeField] Transform carPart;
    [SerializeField] Transform carPart2;
    [SerializeField] Transform carPart3;
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
        if (col.CompareTag("Obstacle"))
        {
            point -= 1;
            print(point);

            if (point <= 30)
            {
                if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 1 || PlayerPrefs.GetInt("CurrentCar") == 2 || PlayerPrefs.GetInt("CurrentCar") == 7
                    || PlayerPrefs.GetInt("CurrentCar") == 11)
                {
                    carPart.gameObject.SetActive(false);
                }

            }

            if (point <= 27)
            {
                if (PlayerPrefs.GetInt("CurrentCar") == 11)
                {
                    carPart2.gameObject.SetActive(false);
                    carPart3.gameObject.SetActive(false);
                    Amplitude.Instance.logEvent("WindowDoorsIkarusDamaged");
                }
            }
            if (point <= 20)
            {
                wheels1.gameObject.SetActive(false);
                wheels1col.gameObject.SetActive(false);
                blow.SetActive(true);
                Amplitude.Instance.logEvent("wheel1damaged");
            }

            if (point <= 5)
            {
                wheels2.gameObject.SetActive(false);
                wheels2col.gameObject.SetActive(false);
                Amplitude.Instance.logEvent("wheel1damaged + wheel1damaged");
            }
        }
        if (col.CompareTag("CarAI") || col.CompareTag("Car"))
        {
            
            if (carController.speed <= 50)
            {
                point -= 1;
            }

             if (carController.speed > 50)
              {
                 point -= 3;
                  
             }
             if (carController.speed > 150)
             {
                 point -= 8;
             }

            if (carController.speed > 300)
            {
                point -= 50;
            }

            if (point <= 30)
            {
                if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 1 || PlayerPrefs.GetInt("CurrentCar") == 2 || PlayerPrefs.GetInt("CurrentCar") == 7
                    || PlayerPrefs.GetInt("CurrentCar") == 11)
                {
                    carPart.gameObject.SetActive(false);
                    Amplitude.Instance.logEvent("FrontPartsDamaged");
                }
                
            }

            if (point <= 27)
            {
                if (PlayerPrefs.GetInt("CurrentCar") == 11)
                {
                    carPart2.gameObject.SetActive(false);
                    carPart3.gameObject.SetActive(false);
                    Amplitude.Instance.logEvent("WindowDoorsIkarusDamaged");
                }
            }
            if (point <= 25)
            {
                wheels1.gameObject.SetActive(false);
                wheels1col.gameObject.SetActive(false);
                blow.SetActive(true);
                Amplitude.Instance.logEvent("wheel1damaged");
            }

            if (point <= 15)
            {
                wheels2.gameObject.SetActive(false);
                wheels2col.gameObject.SetActive(false);
                Amplitude.Instance.logEvent("wheel1damaged + wheel1damaged");
            }

            print(point);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        print(point);
        if (col.gameObject.CompareTag("Obstacle"))
        {
            print(point);
        }
    }
}
