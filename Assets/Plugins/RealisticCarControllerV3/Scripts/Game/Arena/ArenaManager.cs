using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager manage;
    public GameObject lose;
    public GameObject Win;
    public AudioSource loseSnd;
    public AudioSource winSnd;
    public Text erndMoneyWin;
    public Text erndMoneyLose;
    public Text carDisplayWin;
    public Text carDisplayLose;
    public GameObject LapTimer;
    public GameObject Laps;
    public GameObject FinishLine;
    [SerializeField] GameObject NextBtn;
    [Header("Game")]
    [SerializeField] Text wins;
    [Header("Cars")]
    public GameObject[] cars;
    [SerializeField] Transform Parent_;

    public GameObject InstantiatedCar1;
    public GameObject InstantiatedCar2;
    public GameObject InstantiatedCar3;
    public GameObject InstantiatedCar4;
    public GameObject InstantiatedCar5;
    public GameObject InstantiatedCar6;
    public GameObject InstantiatedCar7;
    public GameObject InstantiatedCar8;
    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;
    [SerializeField] Transform spawnPoint3;
    [SerializeField] Transform spawnPoint4;
    [SerializeField] Transform spawnPoint5;
    private int car_counter = 0;

    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        lose.SetActive(false);
        Win.SetActive(false);

        #region Arena

        if (MainMenuManager.manage.isArena1)
        {
            Amplitude.Instance.logEvent("Arena#1");
            InstatiationAiCars1();
        }
        if (MainMenuManager.manage.isArena2)
        {
            Amplitude.Instance.logEvent("Arena#2");
            InstatiationAiCars1();
            InstatiationAiCars2();
        }

        if (MainMenuManager.manage.isArena3)
        {
            Amplitude.Instance.logEvent("Arena#3");
            InstatiationAiCars1();
            InstatiationAiCars2();
            InstatiationAiCars3();
        }

        if (MainMenuManager.manage.isArena4)
        {
            Amplitude.Instance.logEvent("Arena#4");
            InstatiationAiCars1();
            InstatiationAiCars2();
            InstatiationAiCars3();
            InstatiationAiCars4();
        }

        if (MainMenuManager.manage.isArena5)
        {
            Amplitude.Instance.logEvent("Arena#5");
            InstatiationAiCars1();
            InstatiationAiCars2();
            InstatiationAiCars3();
        }

        if (MainMenuManager.manage.isArena6)
        {
            Amplitude.Instance.logEvent("Arena#6");
            InstatiationAiCars1();
            InstatiationAiCars2();
        }

        if (MainMenuManager.manage.isArena7)
        {
            Amplitude.Instance.logEvent("Arena#7");
            InstatiationAiCars5();
        }

        if (MainMenuManager.manage.isArena8)
        {
            Amplitude.Instance.logEvent("Arena#8");
            InstatiationAiCars1();
            InstatiationAiCars2();
            InstatiationAiCars3();
        }

        if (MainMenuManager.manage.isArena9)
        {
            Amplitude.Instance.logEvent("Arena#9");
            InstatiationAiCars1();
            InstatiationAiCars2();
            InstatiationAiCars3();
            InstatiationAiCars4();
        }

        if (MainMenuManager.manage.isArena10)
        {
            Amplitude.Instance.logEvent("Arena#10"); 
            InstatiationAiCars6();

        }

        if (MainMenuManager.manage.isArena11)
        {
            Amplitude.Instance.logEvent("Arena#11");
            InstatiationAiCars5();
            InstatiationAiCars7();
        }

        if (MainMenuManager.manage.isArena12)
        {
            Amplitude.Instance.logEvent("Arena#12");
            InstatiationAiCars7();
        }

        if (MainMenuManager.manage.isArena13)
        {
            Amplitude.Instance.logEvent("Arena#13");
            InstatiationAiCars7();
            InstatiationAiCars5();
            InstatiationAiCars1();
        }

        if (MainMenuManager.manage.isArena14)
        {
            Amplitude.Instance.logEvent("Arena#12");
            InstatiationAiCars8();
        }

        #endregion

        #region Checkpoint

        if (MainMenuManager.manage.isCheckpoint1)
        {
            Amplitude.Instance.logEvent("CheckpointLV#2");
            InstatiationAiCars1();
            InstatiationAiCars2();
            InstatiationAiCars3();
            InstantiatedCar1.GetComponent<RCC_AICarController>()._AIType = RCC_AICarController.AIType.FollowWaypoints;
            InstantiatedCar2.GetComponent<RCC_AICarController>()._AIType = RCC_AICarController.AIType.FollowWaypoints;
            InstantiatedCar3.GetComponent<RCC_AICarController>()._AIType = RCC_AICarController.AIType.FollowWaypoints;
        }
        #endregion
    }

    #region UI Mechanics
    public void Lose()
    {
        Invoke("LatencyLose", 0.5f);
    }

    public void Winner()
    {
        Invoke("LatencyWin", 0.5f);
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 5000f);
    }

    void LatencyWin()
    {
        erndMoneyWin.text = (AICarBehaiovour.manage.coin).ToString();
        carDisplayWin.text = (CarDamage.manage.frag).ToString();
        Win.SetActive(true);
        wins.text = PlayerPrefs.GetInt("ArenaWinsLevel1").ToString();
        winSnd.Play();
        if (MainMenuManager.manage.isArena4 || MainMenuManager.manage.isArena9)
        {
            NextBtn.SetActive(false);
        }
    }

    void LatencyLose()
    {
        erndMoneyLose.text = (AICarBehaiovour.manage.coin).ToString();
        carDisplayLose.text = (CarDamage.manage.frag).ToString();
        lose.SetActive(true);
        loseSnd.Play();
    }

    public void EnergyForAds()
    {
        CarDamage.manage.energy = CarDamage.manage.energy + 25;
        CarDamage.manage.energyBarProgress.GetComponent<Text>().text = CarDamage.manage.energy.ToString();
        CarDamage.manage.StartEngine();
        CarDamage.manage.isDead = false;
        AICarBehaiovour.manage.StartEngine();
        DamagePartsHood.manage.blow.SetActive(false);
        lose.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Amplitude.Instance.logEvent("Restart");
        SceneManager.LoadScene(Application.loadedLevel);
        AudioListener.pause = false;
    }

    public void Next()
    {
        if (PlayerPrefs.GetInt("ArenaWin1")==1 && CarDamage.manage.isWin1)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena2 = true;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            print("ArenaLV2");
        }

        if (PlayerPrefs.GetInt("ArenaWin2") == 1 && CarDamage.manage.isWin2)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = true;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            print("ArenaLV3");
        }

        if (PlayerPrefs.GetInt("ArenaWin3") == 1 && CarDamage.manage.isWin3)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = true;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            print("ArenaLV3");
        }

        if (PlayerPrefs.GetInt("ArenaWin4") == 1 && CarDamage.manage.isWin4)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = true;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            print("ArenaLV4");
        }

        if (PlayerPrefs.GetInt("ArenaWin5") == 1 && CarDamage.manage.isWin5)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = true;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            print("ArenaLV5");
        }

        if (PlayerPrefs.GetInt("ArenaWin6") == 1 && CarDamage.manage.isWin6)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = true;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            print("ArenaLV6");
        }

        if (PlayerPrefs.GetInt("ArenaWin7") == 1 && CarDamage.manage.isWin7)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = true;
            MainMenuManager.manage.isArena9 = false;
            print("ArenaLV7");
        }

        if (PlayerPrefs.GetInt("ArenaWin8") == 1 && CarDamage.manage.isWin8)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = true;
            print("ArenaLV8");
        }

        if (PlayerPrefs.GetInt("ArenaWin9") == 1 && CarDamage.manage.isWin9)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            MainMenuManager.manage.isArena10 = true;
            print("ArenaLV9");
        }

        if (PlayerPrefs.GetInt("ArenaWin10") == 1 && CarDamage.manage.isWin10)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            MainMenuManager.manage.isArena10 = false;
            MainMenuManager.manage.isArena11 = true;
            print("ArenaLV10");
        }

        if (PlayerPrefs.GetInt("ArenaWin11") == 1 && CarDamage.manage.isWin11)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            MainMenuManager.manage.isArena10 = false;
            MainMenuManager.manage.isArena11 = true;
            print("ArenaLV11");
        }

        if (PlayerPrefs.GetInt("ArenaWin12") == 1 && CarDamage.manage.isWin12)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            MainMenuManager.manage.isArena10 = false;
            MainMenuManager.manage.isArena11 = false;
            MainMenuManager.manage.isArena12 = false;
            MainMenuManager.manage.isArena13 = true;
            print("ArenaLV12");
        }

        if (PlayerPrefs.GetInt("ArenaWin13") == 1 && CarDamage.manage.isWin13)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            MainMenuManager.manage.isArena10 = false;
            MainMenuManager.manage.isArena11 = false;
            MainMenuManager.manage.isArena12 = false;
            MainMenuManager.manage.isArena13 = false;
            MainMenuManager.manage.isArena14 = true;
            print("ArenaLV13");
        }

        if (PlayerPrefs.GetInt("ArenaWin14") == 1 && CarDamage.manage.isWin13)
        {
            MainMenuManager.manage.isArena1 = false;
            MainMenuManager.manage.isArena2 = false;
            MainMenuManager.manage.isArena3 = false;
            MainMenuManager.manage.isArena4 = false;
            MainMenuManager.manage.isArena5 = false;
            MainMenuManager.manage.isArena6 = false;
            MainMenuManager.manage.isArena7 = false;
            MainMenuManager.manage.isArena8 = false;
            MainMenuManager.manage.isArena9 = false;
            MainMenuManager.manage.isArena10 = false;
            MainMenuManager.manage.isArena11 = false;
            MainMenuManager.manage.isArena12 = false;
            MainMenuManager.manage.isArena13 = false;
            MainMenuManager.manage.isArena14 = false;
            MainMenuManager.manage.isArena15 = true;
            print("ArenaLV14");
        }

        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        Amplitude.Instance.logEvent("Menu");
        SceneManager.LoadScene("garage");

    }
    #endregion


    #region Game Mechanics

    void InstatiationAiCars1()
    {

        InstantiatedCar1 = Instantiate(cars[Random.Range(0, cars.Length)], spawnPoint1.position, spawnPoint1.rotation);
        InstantiatedCar1.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;
        
    }

    void InstatiationAiCars2()
    {

        InstantiatedCar2 = Instantiate(cars[Random.Range(0, cars.Length)], spawnPoint2.position, spawnPoint2.rotation);
        InstantiatedCar2.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;
        
    }

    void InstatiationAiCars3()
    {

        InstantiatedCar3 = Instantiate(cars[Random.Range(0, cars.Length)], spawnPoint3.position, spawnPoint3.rotation);
        InstantiatedCar3.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;

    }

    void InstatiationAiCars4()
    {

        InstantiatedCar4 = Instantiate(cars[Random.Range(0, cars.Length)], spawnPoint4.position, spawnPoint4.rotation);
        InstantiatedCar4.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;

    }

    void InstatiationAiCars5()
    {

        InstantiatedCar5 = Instantiate(cars[Random.Range(0, cars.Length)], spawnPoint5.position, spawnPoint5.rotation);
        InstantiatedCar5.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;

    }

    void InstatiationAiCars6()
    {

        InstantiatedCar6 = Instantiate(cars[6], spawnPoint5.position, spawnPoint5.rotation);
        InstantiatedCar6.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;

    }

    void InstatiationAiCars7()
    {

        InstantiatedCar7 = Instantiate(cars[Random.Range(0, cars.Length)], spawnPoint2.position, spawnPoint2.rotation);
        InstantiatedCar7.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;

    }

    void InstatiationAiCars8()
    {

        InstantiatedCar8 = Instantiate(cars[7], spawnPoint1.position, spawnPoint1.rotation);
        InstantiatedCar8.GetComponent<Transform>().SetParent(Parent_);
        car_counter += 1;

    }

    private void Update()
    {

        

    }


    #endregion
}
