using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AICarBehaiovour : MonoBehaviour
{
    public static AICarBehaiovour manage;
    public GameObject Blow;
    public int energy = 100;
    public int coin = 0;
    public bool isDead = false;
    [SerializeField] private Material hp;

    int c = 0;

    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {

        //if (SceneManager.GetActiveScene().name == "_arena_1" || SceneManager.GetActiveScene().name == "_arena_2"
        //    || SceneManager.GetActiveScene().name == "_arena_3" || )
        if (MainMenuManager.manage.isArena || MainMenuManager.manage.isCheckpoint)
        {

            hp.color = new Color(0.2120086f, 0.7490196f,0, 0.5254902f);

            #region Arena
            if (MainMenuManager.manage.isArena1) 
            {
                energy = Random.Range(50, 70);
                //energy = Random.Range(10, 15);
            } 
            if (MainMenuManager.manage.isArena2) 
            {
                energy = Random.Range(50, 60);
                //energy = Random.Range(5, 10);
            }

            if (MainMenuManager.manage.isArena3)
            {
                //energy = Random.Range(40, 50);
                energy = Random.Range(27, 60);
            }

            if (MainMenuManager.manage.isArena4)
            {
                //energy = Random.Range(30, 40);
                energy = Random.Range(27, 50);
            }

            if (MainMenuManager.manage.isArena5)
            {
                energy = Random.Range(40, 50);
            }

            if (MainMenuManager.manage.isArena6)
            {
                energy = Random.Range(55, 65);
            }
            if (MainMenuManager.manage.isArena7)
            {
                energy = Random.Range(80, 100);
            }

            if (MainMenuManager.manage.isArena8)
            {
                energy = Random.Range(30, 70);
            }

            if (MainMenuManager.manage.isArena9)
            {
                energy = Random.Range(30, 45);
            }

            if (MainMenuManager.manage.isArena10)
            {
                energy = Random.Range(100, 130);
            }

            if (MainMenuManager.manage.isArena11)
            {
                energy = Random.Range(40, 55);
            }

            if (MainMenuManager.manage.isArena12)
            {
                energy = Random.Range(55, 70);
            }
            if (MainMenuManager.manage.isArena13)
            {
                energy = Random.Range(35, 45);
            }

            if (MainMenuManager.manage.isArena14)
            {
                energy = Random.Range(108, 135);
            }

            #endregion

            #region Checkpoints

            if (MainMenuManager.manage.isCheckpoint1)
            {
                energy = Random.Range(10, 30);
            }

            #endregion

            //print(energy);
        }

    }

    private void Update()
    {
       // c += 1;
        //if (c == 1)
       // {
       //    HP1 = GameObject.Find("LifeCar1");
        //    lbHP1 = GameObject.Find("carLbl");
        //    HP1.GetComponent<Text>().text = energy.ToString();
      //  }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!CarDamage.manage.isDead && !isDead)
        {
            if (other.CompareTag("Car") || other.CompareTag("Player")) //|| other.CompareTag("baseball_bat"))
            {
                if (GetComponent<RCC_CarControllerV3>().speed > 90)
                {
                    PlayerPrefs.SetInt("damage", 10);
                    energy -= 5;
                    if (energy <= 0) //&& !CarDamage.manage.AiIsDead)
                    {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        Blow.SetActive(true);
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        isDead = true; 
                        Amplitude.Instance.logEvent("Bot1Crashed90KMH");
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 160)
                {
                    PlayerPrefs.SetInt("damage", 20);
                    energy -= 15;
                    if (energy <= 0) //&& !CarDamage.manage.AiIsDead)
                    {
                        isDead = true;
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        Blow.SetActive(true);
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        Amplitude.Instance.logEvent("Bot1Crashed140KMH");
                    }
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 250)
                {
                    PlayerPrefs.SetInt("damage", 30);
                    energy -= 25;
                    if (energy <= 0) //&& !CarDamage.manage.AiIsDead)
                    {
                        isDead = true;
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        Blow.SetActive(true);
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;

                        Amplitude.Instance.logEvent("Bot1Crashed250KMH");
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("damage", 0);
                    energy -= 2;
                    if (energy <= 0) //&& !CarDamage.manage.AiIsDead)
                    {
                        isDead = true;
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        Blow.SetActive(true);
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 500f);
                        coin += 500;
                        Amplitude.Instance.logEvent("Bot1Crashed");
                    }
                }

                #region Arena Mode

                if (MainMenuManager.manage.isArena1) //&& !MainMenuManager.manage.isArena2)
                {
                    if (energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                    if (energy >= 100)
                        print("green");
                    hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);



                    print("lv1");
                    if (energy < 80 && energy > 60)

                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if (energy < 60 && energy > 50)

                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if (energy < 50 && energy > 30)

                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if (energy < 30 && energy > 15)

                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if (energy < 15 && energy >= 0)

                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena2)
                {
                    print("lv2");


                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100)
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }


                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                }

                if (MainMenuManager.manage.isArena3)
                {
                    print("lv3");

                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 ||
                        ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {
                        energy = 0;
                        hp.color = Color.black;
                        //black

                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100 ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 100))
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena4)
                {
                    print("lv4");

                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 ||
                        ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {
                        
                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100 ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy >= 100))
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                         (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy >= 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena5)
                {
                    print("lv5");

                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 ||
                        ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100 ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 100))
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena6)
                {
                    print("lv6");


                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100)
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }


                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                }

                if (MainMenuManager.manage.isArena7) 
                {
                    if (energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                    if (energy >= 100)
                        print("green");
                    hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);



                    print("lv7");
                    if (energy < 80 && energy > 60)

                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if (energy < 60 && energy > 50)

                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if (energy < 50 && energy > 30)

                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if (energy < 30 && energy > 15)

                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if (energy < 15 && energy >= 0)

                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena8)
                {
                    print("lv8");

                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 ||
                        ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100 ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 100))
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena9)
                {
                    print("lv9");

                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 ||
                        ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100 ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy >= 100))
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                         (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 0) ||
                        (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy < 15 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy >= 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena10)
                {
                    if (energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                    if (energy >= 100)
                        print("green");
                    hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);



                    print("lv7");
                    if (energy < 80 && energy > 60)

                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if (energy < 60 && energy > 50)

                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if (energy < 50 && energy > 30)

                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if (energy < 30 && energy > 15)

                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if (energy < 15 && energy >= 0)

                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena11)
                {
                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy >= 100)
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 0) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }


                    if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                }

                if (MainMenuManager.manage.isArena12)
                {
                    if (energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                    if (energy >= 100)
                        print("green");
                    hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);



                    print("lv7");
                    if (energy < 80 && energy > 60)

                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if (energy < 60 && energy > 50)

                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if (energy < 50 && energy > 30)

                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if (energy < 30 && energy > 15)

                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if (energy < 15 && energy >= 0)

                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                if (MainMenuManager.manage.isArena13)
                {
                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy >= 100 ||
                        (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100))
                    {
                        print("green");
                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy > 0) ||
                        (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy > 0) ||
                        (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }


                    if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0 ||
                        ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                }

                if (MainMenuManager.manage.isArena14)
                {
                    if (energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                    if (energy >= 100)

                    hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);



                    if (energy < 80 && energy > 60)

                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if (energy < 60 && energy > 50)

                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if (energy < 50 && energy > 30)

                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if (energy < 30 && energy > 15)

                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if (energy < 15 && energy >= 0)

                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }
                }

                #endregion

                #region Checkpoint Mode

                if (MainMenuManager.manage.isCheckpoint1)
                {

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy >= 100) || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy >= 100 ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy >= 100))
                    {

                        hp.color = new Color(0.7490196f, 0.6947644f, 0, 0.572549f);


                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 60) ||
                    (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 80 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 60))
                    {
                        print("<80 && >60");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.572549f);

                    }
                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 50) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 60 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 50))
                    {
                        print("<60 && >50");
                        hp.color = new Color(1, 0.9291219f, 0.03301889f, 0.8f);

                        //yellow
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 30) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 50 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 30))
                    {
                        print("<50 && >30");
                        hp.color = new Color(1, 0.3924071f, 0.03137255f, 0.8f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 15) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 30 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 15))
                    {
                        print("<30 && >15");
                        hp.color = new Color(1, 0.3137255f, 0.1269539f, 0.6588235f);

                        //red
                    }

                    if ((ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy > 0) ||
                        (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy > 0) ||
                        (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy < 10 && ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy > 0))
                    {
                        print("<15");
                        hp.color = Color.red;

                        //red
                    }


                    if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 || ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 ||
                        ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                    {

                        hp.color = Color.black;
                        energy = 0;
                        //black

                    }
                }

                    #endregion

                }
            if (CarDamage.manage.isDead)
            {
                GetComponent<RCC_CarControllerV3>().KillEngine();
            }
        }
    }

    public void Enginedestroy()
    {
        GetComponent<RCC_CarControllerV3>().KillEngine();
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
