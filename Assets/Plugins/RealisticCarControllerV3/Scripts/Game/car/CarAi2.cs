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
                if (GetComponent<RCC_CarControllerV3>().speed > 90)
                {
                    PlayerPrefs.SetInt("damage", 15);
                    energy -= 5;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        Blow.SetActive(true);
                        energy = 0;
                        Amplitude.Instance.logEvent("Bot3KillThePlayer90KMH");
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 140)
                {
                    PlayerPrefs.SetInt("damage", 25);
                    energy -= 15;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        Blow.SetActive(true);
                        energy = 0;
                        Amplitude.Instance.logEvent("Bot3KillThePlayer140KMH");
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 250)
                {
                    PlayerPrefs.SetInt("damage", 35);
                    energy -= 25;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        coin += 500;
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        Blow.SetActive(true);
                        energy = 0;
                        Amplitude.Instance.logEvent("Bot3KillThePlayer250KMH");
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("damage", 0);
                    energy -= 2;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        coin += 500;
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        Blow.SetActive(true);
                        energy = 0;
                    }
                }
            }
        }
        if (CarDamage.manage.isDead)
        {
            GetComponent<RCC_CarControllerV3>().KillEngine();
        }
    }

    public static Vector3 SetPosZero()
    {
        return Vector3.zero;
    }

    public void StartEngine()
    {
        if (!CarDamage.manage.AiIsDead2)
        {
            Invoke("latencyStartEngine",1f);
        }

    }

    void latencyStartEngine()
    {
        GetComponent<RCC_CarControllerV3>().StartEngineNow();
    }
}
