using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAi1 : MonoBehaviour
{
    public static CarAi1 manage;
    public GameObject Blow;
    public int coin = 0;
    private void Awake()
    {
        manage = this;
    }
    public int energy = 100;
    private void OnTriggerEnter(Collider other)
    {
        
        int a = 0;
        if (other.CompareTag("Player") && !CarDamage.manage.isDead)
        {
            if (MainMenuManager.manage.isAllvsYou )
            {
                if (GetComponent<RCC_CarControllerV3>().speed > 90)
                {
                    PlayerPrefs.SetInt("damage", 15);
                    energy -= 5;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead1)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        Blow.SetActive(true);
                        energy = 0;
                        // Invoke("latency", 0.01f);
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 160)
                {
                    PlayerPrefs.SetInt("damage", 25);
                    energy -= 15;
    
                    if (energy <= 0 && !CarDamage.manage.AiIsDead1)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        Blow.SetActive(true);
                        energy = 0;
                        // Invoke("latency", 0.01f);
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 250)
                {
                    PlayerPrefs.SetInt("damage", 35);
                    energy -= 25;
                    if (energy <= 0 && !CarDamage.manage.AiIsDead1)
                    {
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        Blow.SetActive(true);
                        energy = 0;
                        // Invoke("latency", 0.01f);
                    }
                }
                else if(!CarDamage.manage.AiIsDead1)
                {
                    PlayerPrefs.SetInt("damage", 0);
                    energy -= 2;

                    if (energy <= 0)
                        {
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            coin += 500;
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            energy = 0;
                        // Invoke("latency", 0.01f);
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
