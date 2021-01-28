using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace CarAI2
//{
    public class CarAi2 : MonoBehaviour
    {
        public static CarAi2 manage;
        public GameObject Blow;
        public int energy = 100;
        int a = 0;
        public int coin = 0;
        private void Awake()
        {
            manage = this;
        }

        private void Start()
        {
            energy = Random.Range(100, 130);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !CarDamage.manage.isDead)
            {
                if (MainMenuManager.manage.isAllvsYou || MainMenuManager.manage.isFreerideActive)
                {
                    if (GetComponent<RCC_CarControllerV3>().speed > 90)
                    {
                        PlayerPrefs.SetInt("damage", 10);
                        energy -= 5;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Blow.SetActive(true);
                            
                            Amplitude.Instance.logEvent("Bot3KillThePlayer90KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 140)
                    {
                        PlayerPrefs.SetInt("damage", 20);
                        energy -= 15;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Blow.SetActive(true);
                            
                            Amplitude.Instance.logEvent("Bot3KillThePlayer140KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 250)
                    {
                        PlayerPrefs.SetInt("damage", 30);
                        energy -= 25;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                            coin += 500;
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            Blow.SetActive(true);
                            
                            Amplitude.Instance.logEvent("Bot3KillThePlayer250KMH");
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("damage", 0);
                        energy -= 2;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead2)
                        {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                            coin += 500;
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            Blow.SetActive(true);
                            
                        }
                    }
                }
            }
            if (CarDamage.manage.isDead)
            {
                GetComponent<RCC_CarControllerV3>().KillEngine();
            }
        }

        public void StartEngine()
        {
            if (!CarDamage.manage.AiIsDead2)
            {
                Invoke("latencyStartEngine", 1f);
            }

        }

        void latencyStartEngine()
        {
            GetComponent<RCC_CarControllerV3>().StartEngineNow();
        }
    }

//}

