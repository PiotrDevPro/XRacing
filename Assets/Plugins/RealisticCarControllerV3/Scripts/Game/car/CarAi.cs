using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

    public class CarAi : MonoBehaviour
    {
        public static CarAi manage;
        public GameObject Blow;
        public int energy = 100;
        public int coin = 0;
        private GameObject HP1;
        private GameObject lbHP1;

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
                    HP1 = GameObject.Find("LifeCar1");
                    lbHP1 = GameObject.Find("carLbl");
                    HP1.GetComponent<Text>().text = energy.ToString();
                    if (GetComponent<RCC_CarControllerV3>().speed > 90)
                    {
                        PlayerPrefs.SetInt("damage", 10);
                        energy -= 5;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;
                            Amplitude.Instance.logEvent("Bot1KillThePlayer90KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 160)
                    {
                        PlayerPrefs.SetInt("damage", 20);
                        energy -= 15;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;

                            Amplitude.Instance.logEvent("Bot1KillThePlayer140KMH");
                        }
                    }

                    if (GetComponent<RCC_CarControllerV3>().speed > 250)
                    {
                        PlayerPrefs.SetInt("damage", 30);
                        energy -= 25;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;

                            Amplitude.Instance.logEvent("Bot1KillThePlayer250KMH");
                        }
                    }
                    else
                    {
                        PlayerPrefs.SetInt("damage", 0);
                        energy -= 2;
                        if (energy <= 0 && !CarDamage.manage.AiIsDead)
                        {
                            energy = 0;
                            GetComponent<RCC_CarControllerV3>().KillEngine();
                            Blow.SetActive(true);
                            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                            coin += 500;

                            Amplitude.Instance.logEvent("Bot1KillThePlayer");
                        }
                    }
                }
                if (energy < 0)
                {
                    energy = 0;
                }
                if (energy > 50)
                {
                    lbHP1.GetComponent<Text>().color = Color.green;
                }
                if (energy < 50)
                {
                    lbHP1.GetComponent<Text>().color = Color.yellow;
                }

                if (energy < 20)
                {
                    lbHP1.GetComponent<Text>().color = Color.red;
                }
            }
            if (CarDamage.manage.isDead && !MainMenuManager.manage.isTopSpeedActive)
            {
                GetComponent<RCC_CarControllerV3>().KillEngine();
            }
        }   
    }

        public void StartEngine()
        {
            if (!CarDamage.manage.AiIsDead)
            {
                Invoke("latencyStartEngine", 1f);
            }

        }

        void latencyStartEngine()
        {
            GetComponent<RCC_CarControllerV3>().StartEngineNow();
        }

    }