using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPTrunk : MonoBehaviour
{
    public static DPTrunk manage;
    [SerializeField] Transform carPart;
    public int point = 50;
    public RCC_CarControllerV3 carController;
    public Transform wheels1;
    public Transform wheels2;
    public Transform wheels1col;
    public Transform wheels2col;
    public GameObject Blow;

    private void Awake()
    {
        manage = this;
    }

    private void OnTriggerEnter(Collider col)
    {
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

            if (point <= 35)
            {
                if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 1 || PlayerPrefs.GetInt("CurrentCar") == 7 || PlayerPrefs.GetInt("CurrentCar") == 11)
                {
                    carPart.gameObject.SetActive(false);
                    Amplitude.Instance.logEvent("TrunkPartsDamaged");
                }
                
            }
            if (point <= 25)
            {
                wheels1.gameObject.SetActive(false);
                wheels1col.gameObject.SetActive(false);
                Blow.SetActive(true);
                Amplitude.Instance.logEvent("rearWheel1Damaged");
            }

            if (point <= 15)
            {
                wheels2.gameObject.SetActive(false);
                wheels2col.gameObject.SetActive(false);
                Amplitude.Instance.logEvent("rearWheel1Damaged + rearWheel2Damaged");

            }

            print(point);
        }
    }
}
