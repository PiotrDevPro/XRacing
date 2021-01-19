using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CarAi : MonoBehaviour
{
    public static CarAi manage;
    public GameObject Blow;
    private void Awake()
    {
        manage = this;
    }
    public int energy = 100;
    public int coin = 0;
    int a = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !CarDamage.manage.isDead)
        {
            if (MainMenuManager.manage.isAllvsYou)
            {
                if (GetComponent<RCC_CarControllerV3>().speed > 90)
                {
                    PlayerPrefs.SetInt("damage", 15);
                    energy -= 5;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        Blow.SetActive(true);
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        energy = 0;
                      //  Invoke("latency", 0.01f);
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 160)
                {
                    PlayerPrefs.SetInt("damage", 25);
                    energy -= 15;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        Blow.SetActive(true);
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        energy = 0;
                        //  Invoke("latency", 0.01f);
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 250)
                {
                    PlayerPrefs.SetInt("damage", 35);
                    energy -= 25;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        Blow.SetActive(true);
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        energy = 0;
                        // Invoke("latency", 0.01f);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("damage", 0);
                    energy -= 2;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead)
                    {
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            energy = 0;
                        // Invoke("latency",0.01f);

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
      //  GetComponentInChildren<BoxCollider>().tag = "Car";
    }

}
