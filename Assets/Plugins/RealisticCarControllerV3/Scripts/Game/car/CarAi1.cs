using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

    public class CarAi1 : MonoBehaviour
    {
        public static CarAi1 manage;
        public GameObject Blow;
        public int coin = 0;
        public int energy = 100;
        private GameObject HP2;
        private GameObject lbHP2;
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
        if (!CarDamage.manage.isDead)
        {
            if (other.CompareTag("Car") || other.CompareTag("Player"))
            {
                if (MainMenuManager.manage.isAllvsYou || MainMenuManager.manage.isFreerideActive || MainMenuManager.manage.isTopSpeedActive)
                {
                    HP2 = GameObject.Find("LifeCar2");
                    lbHP2 = GameObject.Find("carLb2");
                    HP2.GetComponent<Text>().text = energy.ToString();
                    if (GetComponent<RCC_CarControllerV3>().speed > 90)
                    {
                        PlayerPrefs.SetInt("damage", 10);
                        energy -= 5;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead1)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Blow.SetActive(true);

                            Amplitude.Instance.logEvent("Bot2Crashed90KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 140)
                    {
                        PlayerPrefs.SetInt("damage", 20);
                        energy -= 15;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead1)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Blow.SetActive(true);
                            Amplitude.Instance.logEvent("Bot2Crashed140KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 250)
                    {
                        PlayerPrefs.SetInt("damage", 30);
                        energy -= 25;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead1)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Blow.SetActive(true);
                            Amplitude.Instance.logEvent("Bot2Crashed250KMH");
                        }
                    }
                    else if (!CarDamage.manage.AiIsDead1)
                    {
                        PlayerPrefs.SetInt("damage", 0);
                        energy -= 2;
                        if (energy <= 0)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            coin += 500;
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            Amplitude.Instance.logEvent("Bot2Crashed");
                        }
                    }
                }

                if (energy < 0)
                {
                    energy = 0;
                }
                if (energy > 50)
                {
                    lbHP2.GetComponent<Text>().color = Color.green;
                }
                if (energy < 50)
                {
                    lbHP2.GetComponent<Text>().color = Color.yellow;
                }

                if (energy < 20)
                {
                    lbHP2.GetComponent<Text>().color = Color.red;
                }
            }
            if (CarDamage.manage.isDead && !MainMenuManager.manage.isTopSpeedActive)
            {
                GetComponent<RCC_CarControllerV3>().KillEngine();
            }
        }
    }
            
        void latency()
        {
            //  GetComponentInChildren<BoxCollider>().tag = "Car";
        }

        public void StartEngine()
        {
            if (!CarDamage.manage.AiIsDead1)
            {
                Invoke("latencyStartEngine", 1f);
            }

        }

        void latencyStartEngine()
        {
            GetComponent<RCC_CarControllerV3>().StartEngineNow();
        }

    }

