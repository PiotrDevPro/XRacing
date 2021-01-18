using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAi2 : MonoBehaviour
{
    public static CarAi2 manage;
    public GameObject Blow;
    private void Awake()
    {
        manage = this;
    }
    public int energy = 100;
    int a = 0;
    public int coin = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !CarDamage.manage.isDead)
        {
            if (MainMenuManager.manage.isAllvsYou )
            {
                if (GetComponent<RCC_CarControllerV3>().speed > 100)
                {
                    PlayerPrefs.SetInt("damage", 15);
                    energy -= 5;
                    if (energy <= 0)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        energy = 0;
                        coin += 500;
                        Blow.SetActive(true);
                        Invoke("latency", 0.01f);
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 160)
                {
                    PlayerPrefs.SetInt("damage", 25);
                    energy -= 15;
                    if (energy <= 0)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        energy = 0;
                        coin += 500;
                        Blow.SetActive(true);
                        Invoke("latency", 0.01f);
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 250)
                {
                    PlayerPrefs.SetInt("damage", 35);
                    energy -= 25;
                    if (energy <= 0)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        energy = 0;
                        coin += 500;
                        Blow.SetActive(true);
                        Invoke("latency", 0.01f);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("damage", 0);
                    energy -= 100;
                    if (energy <= 0)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        energy = 0;
                        coin += 500;
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        Blow.SetActive(true);
                        Invoke("latency", 0.01f);
                        
                    }
                }
            }
        }
        if (CarDamage.manage.isDead)
        {
            GetComponent<RCC_CarControllerV3>().KillEngine();
        }
    }
    void latency()
    {
        GetComponentInChildren<BoxCollider>().tag = "Car";
    }
}
