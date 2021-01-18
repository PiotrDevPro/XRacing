using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CarDamage : MonoBehaviour
{
    public static CarDamage manage;
    public int energy = 100;
    public bool isDead = false;
    private GameObject energyBarProgress;
    private GameObject optimizeCollider;
    private GameObject enemyDestroy;

    int count = 0;
    int frag = 0;

    private void Awake()
    {
        manage = this;
    }

    private void Update()
    {
        count += 1;
        if (count == 1)
        {
            if (SceneManager.GetActiveScene().name == "level_lap6" && MainMenuManager.manage.isAllvsYou)
            {
                energyBarProgress = GameObject.Find("LifeGauge");
                optimizeCollider = GameObject.Find("OptimazeColl");
                enemyDestroy = GameObject.Find("currenemy");
                PlayerPrefs.SetInt("crashed",0);
                print(PlayerPrefs.GetInt("crashed"));
                optimizeCollider.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarAI") && !isDead)
        {
            if (MainMenuManager.manage.isAllvsYou)
            {
                energy -= 3 + PlayerPrefs.GetInt("damage");
                if (energy != 0)
                {
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }
                if (energy <= 0)
                {
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    PlayerPrefs.SetInt("crashed", 1);
                    energyBarProgress.GetComponent<Text>().text = "0";
                    CarManager.manage.Lose();
                    isDead = true;
                }

                if (CarAi.manage.energy <= 0)
                {

                    frag += 1;
                    enemyDestroy.GetComponent<Text>().text = frag.ToString();
                }

                if (CarAi1.manage.energy <= 0)
                {
                    frag += 1;
                    enemyDestroy.GetComponent<Text>().text = frag.ToString();
                }

                if (CarAi2.manage.energy <= 0)
                {
                    frag += 1;
                    enemyDestroy.GetComponent<Text>().text = frag.ToString();
                }

                if (CarAi.manage.energy <=0 && CarAi1.manage.energy <= 0 && CarAi2.manage.energy <= 0 && !isDead)
                {
                    CarManager.manage.Winner();

                }
            }
        }
    }       
}
