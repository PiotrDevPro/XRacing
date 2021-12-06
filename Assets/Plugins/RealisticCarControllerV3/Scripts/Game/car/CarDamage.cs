using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CarDamage : MonoBehaviourPunCallbacks, IDamageble
{
    public static CarDamage manage;
    public int energy = 100;
    public int counter = 0;
    public bool isDead = false;
    public bool isWin = false;
    public bool isAdsShowed = false;
    public bool AiIsDead = false;
    public bool AiIsDead1 = false;
    public bool AiIsDead2 = false;
    public bool AiIsDead3 = false;
    public bool AiIsDead4 = false;
    public bool AiIsDead5 = false;

    public GameObject energyBarProgress;
    private GameObject enemyDestroy;


    private GameObject HP1;
    private GameObject HP2;
    private GameObject HP3;
    private GameObject lbHP1;
    private GameObject lbHP2;
    private GameObject lbHP3;

    private GameObject point1Active;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListPrefab;

    PhotonView PV;

    Rigidbody rb;

    [Header("ArenaWin")]
    public bool isWin1 = false;
    public bool isWin2 = false;
    public bool isWin3 = false;
    public bool isWin4 = false;
    public bool isWin5 = false;
    public bool isWin6 = false;
    public bool isWin7 = false;
    public bool isWin8 = false;
    public bool isWin9 = false;
    public bool isWin10 = false;
    public bool isWin11 = false;
    public bool isWin12 = false;
    public bool isWin13 = false;
    public bool isWin14 = false;
    public Player Player { get; private set; }

    int count = 0;
    public int frag;
    private int fragRec;


    [Header("CheckpointWin")]
    public bool isChkWin1 = false;

    private void Update()
    {
        count += 1;
        if (SceneManager.GetActiveScene().name == "battle_online" && count == 1)
        {
            energyBarProgress = GameObject.Find("EnergyNum");
            point1Active = GameObject.Find("point1");
            point1Active.SetActive(false);
        }


    }

    private void Awake()
    {
        manage = this;
        PV = GetComponent<PhotonView>();
    }


    private void Start()
    {

        if (PlayerPrefs.GetInt("Energy") == 25)
        {
            energy += 25;
        }
    }

    #region Photon Methods
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("OnPlayerEnteredRoom");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("OnPlayerLeftRoom");
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        print("OnRoomPropertiesUpdate");
    }

    public override void OnJoinedRoom()
    {
        //print(PhotonNetwork.NickName);
        //print(PhotonNetwork.CurrentRoom.CustomProperties["Energy"]);
        //print(PhotonNetwork.LocalPlayer.CustomProperties["Role"]);
        //PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Role", counter++} });
        //print(PhotonNetwork.CurrentRoom.CustomProperties["Energy"].ToString());
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "level_lap6")
        {
            if (other.CompareTag("CarAI") && !isWin)

                if (MainMenuManager.manage.isFreerideActive && !isDead || MainMenuManager.manage.isAllvsYou && !isDead)
                {

                    energyBarProgress = GameObject.Find("LifeGauge");
                    enemyDestroy = GameObject.Find("currenemy");
                    PlayerPrefs.SetInt("crashed", 0);
                    HP1 = GameObject.Find("LifeCar1");
                    HP2 = GameObject.Find("LifeCar2");
                    HP3 = GameObject.Find("LifeCar3");
                    lbHP1 = GameObject.Find("carLbl");
                    lbHP2 = GameObject.Find("carLbl1");
                    lbHP3 = GameObject.Find("carLbl2");
                    HP1.GetComponent<Text>().text = (CarAi.manage.energy).ToString();
                    HP2.GetComponent<Text>().text = (CarAi1.manage.energy).ToString();
                    HP3.GetComponent<Text>().text = (CarAi2.manage.energy).ToString();
                    energy -= 1 + PlayerPrefs.GetInt("damage");

                    if (energy <= 0)
                    {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetInt("crashed", 1);
                        energyBarProgress.GetComponent<Text>().text = "0";
                        CarManager.manage.Lose();
                        isDead = true;
                        Pause.manage.tracks[0].Stop();
                        Pause.manage.tracks[1].Stop();
                        Pause.manage.tracks[2].Stop();
                        Pause.manage.tracks[3].Stop();
                        Pause.manage.tracks[4].Stop();
                        Pause.manage.tracks[5].Stop();
                        Pause.manage.tracks[6].Stop();
                        Amplitude.Instance.logEvent("PlayerIsDead");
                    }

                    if (CarAi.manage.energy <= 0 && !AiIsDead)
                    {

                        frag += 1;
                        PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead = true;
                        PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 15);
                        Amplitude.Instance.logEvent("CarIsDead1");

                    }

                    if (CarAi1.manage.energy <= 0 && !AiIsDead1)
                    {
                        frag += 1;
                        PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead1 = true;
                        PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 15);
                        Amplitude.Instance.logEvent("CarIsDead2");
                    }

                    if (CarAi2.manage.energy <= 0 && !AiIsDead2)
                    {
                        frag += 1;
                        PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead2 = true;
                        PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 15);
                        Amplitude.Instance.logEvent("CarIsDead3");
                    }

                    if (CarAi.manage.energy <= 0 && CarAi1.manage.energy <= 0 && CarAi2.manage.energy <= 0)
                    {
                        CarManager.manage.Winner();
                        isWin = true;
                        Amplitude.Instance.logEvent("PlayerIsWinBattle");
                    }

                    #region Car Display
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    HP1.GetComponent<Text>().text = (CarAi.manage.energy).ToString();
                    if (CarAi.manage.energy < 0)
                    {
                        CarAi.manage.energy = 0;
                    }
                    if (CarAi.manage.energy > 50)
                    {
                        lbHP1.GetComponent<Text>().color = Color.green;
                    }
                    if (CarAi.manage.energy < 50)
                    {
                        lbHP1.GetComponent<Text>().color = Color.yellow;
                    }

                    if (CarAi.manage.energy < 20)
                    {
                        lbHP1.GetComponent<Text>().color = Color.red;
                    }

                    HP2.GetComponent<Text>().text = (CarAi1.manage.energy).ToString();
                    if (CarAi1.manage.energy < 0)
                    {
                        CarAi1.manage.energy = 0;
                    }
                    if (CarAi1.manage.energy > 50)
                    {
                        lbHP2.GetComponent<Text>().color = Color.green;
                    }
                    if (CarAi1.manage.energy < 50)
                    {
                        lbHP2.GetComponent<Text>().color = Color.yellow;
                    }

                    if (CarAi1.manage.energy < 20)
                    {
                        lbHP2.GetComponent<Text>().color = Color.red;
                    }
                    HP3.GetComponent<Text>().text = (CarAi2.manage.energy).ToString();
                    if (CarAi2.manage.energy < 0)
                    {
                        CarAi2.manage.energy = 0;
                    }
                    if (CarAi2.manage.energy > 50)
                    {
                        lbHP3.GetComponent<Text>().color = Color.green;
                    }
                    if (CarAi2.manage.energy < 50)
                    {
                        lbHP3.GetComponent<Text>().color = Color.yellow;
                    }

                    if (CarAi2.manage.energy < 20)
                    {
                        lbHP3.GetComponent<Text>().color = Color.red;
                    }
                    #endregion
                }
        }

        if (SceneManager.GetActiveScene().name == "_arena_1" || SceneManager.GetActiveScene().name == "_arena_2"
            || SceneManager.GetActiveScene().name == "_arena_3" || SceneManager.GetActiveScene().name == "_arena_4")
        {
            if (other.CompareTag("CarAI"))
            {
               if (!isDead)
                {
                    energyBarProgress = GameObject.Find("LifeGauge");
                    enemyDestroy = GameObject.Find("currenemy");
                    PlayerPrefs.SetInt("crashed", 0);
                    energy -= 1 + PlayerPrefs.GetInt("damage");
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    if (energy <= 0)
                    {
                        energy = 0;
                        GetComponent<RCC_CarControllerV3>().KillEngine();
                        PlayerPrefs.SetInt("crashed", 1);
                        energyBarProgress.GetComponent<Text>().text = "0";
                        ArenaManager.manage.Lose();
                        isDead = true;
                        Pause.manage.tracks[0].Stop();
                        Pause.manage.tracks[1].Stop();
                        Pause.manage.tracks[2].Stop();
                        Pause.manage.tracks[3].Stop();
                        Pause.manage.tracks[4].Stop();
                        Pause.manage.tracks[5].Stop();
                        Pause.manage.tracks[6].Stop();
                        Pause.manage.tracks[7].Stop();
                        Pause.manage.tracks[8].Stop();
                        Pause.manage.tracks[9].Stop();
                        Pause.manage.tracks[10].Stop();
                        Pause.manage.tracks[11].Stop();
                        Pause.manage.tracks[12].Stop();
                        Pause.manage.tracks[13].Stop();
                        Pause.manage.tracks[14].Stop();

                        #region Arena Mode
                        if (MainMenuManager.manage.isArena1)
                        {
                            print("Arena1");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }
                        if (MainMenuManager.manage.isArena2)
                        {
                            print("Arena2");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena3)
                        {
                            print("Arena3");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena4)
                        {
                            print("Arena4");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena5)
                        {
                            print("Arena5");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena6)
                        {
                            print("Arena6");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena7)
                        {
                            print("Arena7");
                            ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena8)
                        {
                            print("Arena5");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena9)
                        {
                            print("Arena9");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }
                        if (MainMenuManager.manage.isArena10)
                        {
                            print("Arena10");
                            ArenaManager.manage.InstantiatedCar6.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
;
                        }

                        if (MainMenuManager.manage.isArena11)
                        {
                            print("Arena11");
                            ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena12)
                        {
                            print("Arena12");
                            ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena13)
                        {
                            print("Arena13");
                            ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        if (MainMenuManager.manage.isArena14)
                        {
                            print("Arena14");
                            ArenaManager.manage.InstantiatedCar8.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }

                        #endregion

                        #region Checkpoint Mode
                        if (MainMenuManager.manage.isCheckpoint1)
                        {
                            print("CheckpointLV2");
                            ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().Enginedestroy();
                        }
                        #endregion


                        DamagePartsHood.manage.blow.SetActive(true);
                        Amplitude.Instance.logEvent("PlayerIsDead");
                    }

                    #region WIN Arena

                    if (MainMenuManager.manage.isArena1 && !isWin1)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#1");
                            isWin1 = true;
                            ArenaManager.manage.Winner();
                            
                            Amplitude.Instance.logEvent("PlayerIsWinArena#1");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin1", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena2 && !isWin2)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#2");
                            isWin2 = true;
                            ArenaManager.manage.Winner();
                            
                            Amplitude.Instance.logEvent("PlayerIsWinArena#2");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin2", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena3 && !isWin3)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 &&
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#3");
                            isWin3 = true;
                            ArenaManager.manage.Winner();
                            
                            Amplitude.Instance.logEvent("PlayerIsWinArena#3");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin3", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena4 && !isWin4)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 &&
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#4");
                            isWin4 = true;
                            ArenaManager.manage.Winner();
                            Amplitude.Instance.logEvent("PlayerIsWinArena#4");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin4", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena5 && !isWin5)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 &&
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#5");
                            isWin5 = true;
                            ArenaManager.manage.Winner();
                            Amplitude.Instance.logEvent("PlayerIsWinArena#5");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin5", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena6 && !isWin6)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#6");
                            isWin6 = true;
                            ArenaManager.manage.Winner();

                            Amplitude.Instance.logEvent("PlayerIsWinArena#6");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin6", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena7 && !isWin7)
                    {
                        if (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#7");
                            isWin7 = true;
                            ArenaManager.manage.Winner();

                            Amplitude.Instance.logEvent("PlayerIsWinArena#1");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin7", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena8 && !isWin8)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 &&
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#5");
                            isWin8 = true;
                            ArenaManager.manage.Winner();
                            Amplitude.Instance.logEvent("PlayerIsWinArena#8");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin8", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena9 && !isWin9)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 &&
                            ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#9");
                            isWin9 = true;
                            ArenaManager.manage.Winner();
                            Amplitude.Instance.logEvent("PlayerIsWinArena#9");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin9", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena10 && !isWin10)
                    {
                        if (ArenaManager.manage.InstantiatedCar6.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#10");
                            isWin10 = true;
                            ArenaManager.manage.Winner();

                            Amplitude.Instance.logEvent("PlayerIsWinArena#1");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin10", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena11 && !isWin11)
                    {
                        if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#11");
                            isWin11 = true;
                            ArenaManager.manage.Winner();

                            Amplitude.Instance.logEvent("PlayerIsWinArena#11");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin11", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena12 && !isWin12)
                    {
                        if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#12");
                            isWin12 = true;
                            ArenaManager.manage.Winner();

                            Amplitude.Instance.logEvent("PlayerIsWinArena#1");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin12", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena13 && !isWin13)
                    {
                        if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0
                            && ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#13");
                            isWin13 = true;
                            ArenaManager.manage.Winner();

                            Amplitude.Instance.logEvent("PlayerIsWinArena#11");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin13", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    if (MainMenuManager.manage.isArena14 && !isWin14)
                    {
                        if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        {
                            print("LV#14");
                            isWin14 = true;
                            ArenaManager.manage.Winner();

                            Amplitude.Instance.logEvent("PlayerIsWinArena#1");
                            PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                            PlayerPrefs.SetInt("ArenaWin14", 1);
                            Pause.manage.tracks[0].Stop();
                            Pause.manage.tracks[1].Stop();
                            Pause.manage.tracks[2].Stop();
                            Pause.manage.tracks[3].Stop();
                            Pause.manage.tracks[4].Stop();
                            Pause.manage.tracks[5].Stop();
                            Pause.manage.tracks[6].Stop();
                            Pause.manage.tracks[7].Stop();
                            Pause.manage.tracks[8].Stop();
                            Pause.manage.tracks[9].Stop();
                            Pause.manage.tracks[10].Stop();
                            Pause.manage.tracks[11].Stop();
                            Pause.manage.tracks[12].Stop();
                            Pause.manage.tracks[13].Stop();
                            Pause.manage.tracks[14].Stop();
                        }
                    }

                    #endregion


                    #region WIN Checkpoint Mode

                    if (MainMenuManager.manage.isCheckpoint1 && !isChkWin1)
                    {
                        //if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0)
                        //{
                         //   print("LV#2");
                         //   isChkWin1 = true;
                         //   ArenaManager.manage.Winner();

                         //   Amplitude.Instance.logEvent("PlayerIsWinCheckpoint#LV2");
                        //    PlayerPrefs.SetInt("ArenaWinsLevel1", PlayerPrefs.GetInt("ArenaWinsLevel1") + 1);
                        //    PlayerPrefs.SetInt("CheckpointWin2", 1);
                        //    Pause.manage.tracks[0].Stop();
                        //    Pause.manage.tracks[1].Stop();
                         //   Pause.manage.tracks[2].Stop();
                         //   Pause.manage.tracks[3].Stop();
                         //   Pause.manage.tracks[4].Stop();
                         //   Pause.manage.tracks[5].Stop();
                         //   Pause.manage.tracks[6].Stop();
                         //   Pause.manage.tracks[7].Stop();
                         //   Pause.manage.tracks[8].Stop();
                         //   Pause.manage.tracks[9].Stop();
                         //   Pause.manage.tracks[10].Stop();
                         //   Pause.manage.tracks[11].Stop();
                         //   Pause.manage.tracks[12].Stop();
                         //   Pause.manage.tracks[13].Stop();
                         //   Pause.manage.tracks[14].Stop();
                       // }
                    }


                    #endregion

                    #region Arena Frag

                    if (MainMenuManager.manage.isArena1)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }
                    }

                    if (MainMenuManager.manage.isArena2)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {
                            
                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }
                    }

                    if (MainMenuManager.manage.isArena3)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }

                        if (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead3)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead3 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead3");
                        }
                    }

                    if (MainMenuManager.manage.isArena4)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }

                        if (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead3)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead3 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead3");
                        }

                        if (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead4)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead4 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead4");
                        }
                    }

                    if (MainMenuManager.manage.isArena5)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }

                        if (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead3)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead3 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead3");
                        }
                    }

                    if (MainMenuManager.manage.isArena6)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }
                    }

                    if (MainMenuManager.manage.isArena7)
                    {
                        if (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }
                    }

                    if (MainMenuManager.manage.isArena8)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }

                        if (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead3)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead3 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead3");
                        }
                    }

                    if (MainMenuManager.manage.isArena9)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }

                        if (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead3)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead3 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead3");
                        }

                        if (ArenaManager.manage.InstantiatedCar4.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead4)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead4 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead4");
                        }
                    }

                    if (MainMenuManager.manage.isArena10)
                    {
                        if (ArenaManager.manage.InstantiatedCar6.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }
                    }

                    if (MainMenuManager.manage.isArena11)
                    {
                        if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }


                        if (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }
                    }

                    if (MainMenuManager.manage.isArena12)
                    {
                        if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }
                    }

                    if (MainMenuManager.manage.isArena13)
                    {
                        if (ArenaManager.manage.InstantiatedCar7.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }


                        if (ArenaManager.manage.InstantiatedCar5.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead2");
                        }

                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead3)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead3 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead3");
                        }
                    }

                    if (MainMenuManager.manage.isArena14)
                    {
                        if (ArenaManager.manage.InstantiatedCar8.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("ArenaLV - CarIsDead1");

                        }
                    }

                    #endregion

                    #region Checkpoint Frag

                    if (MainMenuManager.manage.isCheckpoint1)
                    {
                        if (ArenaManager.manage.InstantiatedCar1.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead1)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead1 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("CheckpointLV - CarIsDead1");

                        }

                        if (ArenaManager.manage.InstantiatedCar2.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead2)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead2 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("CheckpointLV - CarIsDead2");

                        }

                        if (ArenaManager.manage.InstantiatedCar3.GetComponentInChildren<AICarBehaiovour>().energy <= 0 && !AiIsDead3)
                        {

                            frag += 1;
                            PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                            enemyDestroy.GetComponent<Text>().text = frag.ToString();
                            AiIsDead3 = true;
                            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
                            Amplitude.Instance.logEvent("CheckpointLV - CarIsDead3");

                        }
                    }

                    #endregion
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "level_top_speed_test")
        {

            if (other.CompareTag("Car") && !isDead && !Checkpoint.manage.isWin || other.CompareTag("CarAI") && !isDead && !Checkpoint.manage.isWin)
            {
                energyBarProgress = GameObject.Find("LifeGauge");
                enemyDestroy = GameObject.Find("currenemy");
                PlayerPrefs.SetInt("crashed", 0);
                HP1 = GameObject.Find("LifeCar1");
                HP2 = GameObject.Find("LifeCar2");
                HP3 = GameObject.Find("LifeCar3");
                lbHP1 = GameObject.Find("carLbl");
                lbHP2 = GameObject.Find("carLbl1");
                lbHP3 = GameObject.Find("carLbl2");
                HP1.GetComponent<Text>().text = (CarAi.manage.energy).ToString();
                HP2.GetComponent<Text>().text = (CarAi1.manage.energy).ToString();
                HP3.GetComponent<Text>().text = (CarAi2.manage.energy).ToString();
                energy -= 1;
                energyBarProgress.GetComponent<Text>().text = energy.ToString();
                if (GetComponent<RCC_CarControllerV3>().speed > 50)
                {
                    energy -= 3;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }

                if (GetComponent<RCC_CarControllerV3>().speed > 150)
                {
                    energy -= 5;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }

                if (energy <= 0)
                {
                    energy = 0;
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    PlayerPrefs.SetInt("crashed", 1);
                    energyBarProgress.GetComponent<Text>().text = "0";
                    LevelManager.manage.Lose();
                    isDead = true;
                    Pause.manage.tracks[0].Stop();
                    Pause.manage.tracks[1].Stop();
                    Pause.manage.tracks[2].Stop();
                    Pause.manage.tracks[3].Stop();
                    Pause.manage.tracks[4].Stop();
                    Pause.manage.tracks[5].Stop();
                    Pause.manage.tracks[6].Stop();
                    Pause.manage.tracks[7].Stop();
                    Pause.manage.tracks[8].Stop();
                    Pause.manage.tracks[9].Stop();
                    Pause.manage.tracks[10].Stop();
                    Pause.manage.tracks[11].Stop();
                    Pause.manage.tracks[12].Stop();
                    Pause.manage.tracks[13].Stop();
                    Pause.manage.tracks[14].Stop();
                    Amplitude.Instance.logEvent("PlayerIsDead");
                }

            }


            #region Enemy Car Display
            energyBarProgress.GetComponent<Text>().text = energy.ToString();
            HP1.GetComponent<Text>().text = (CarAi.manage.energy).ToString();

            if (CarAi.manage.energy > 50)
            {
                lbHP1.GetComponent<Text>().color = Color.green;
            }
            if (CarAi.manage.energy < 50)
            {
                lbHP1.GetComponent<Text>().color = Color.yellow;
            }

            if (CarAi.manage.energy < 20)
            {
                lbHP1.GetComponent<Text>().color = Color.red;
            }

            HP2.GetComponent<Text>().text = (CarAi1.manage.energy).ToString();

            if (CarAi1.manage.energy > 50)
            {
                lbHP2.GetComponent<Text>().color = Color.green;
            }
            if (CarAi1.manage.energy < 50)
            {
                lbHP2.GetComponent<Text>().color = Color.yellow;
            }

            if (CarAi1.manage.energy < 20)
            {
                lbHP2.GetComponent<Text>().color = Color.red;
            }
            HP3.GetComponent<Text>().text = (CarAi2.manage.energy).ToString();
            if (CarAi2.manage.energy > 50)
            {
                lbHP3.GetComponent<Text>().color = Color.green;
            }
            if (CarAi2.manage.energy < 50)
            {
                lbHP3.GetComponent<Text>().color = Color.yellow;
            }

            if (CarAi2.manage.energy < 20)
            {
                lbHP3.GetComponent<Text>().color = Color.red;
            }

            if (CarAi.manage.energy <= 0 && !AiIsDead)
            {
                CarAi.manage.energy = 0;
                frag += 1;
                PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                enemyDestroy.GetComponent<Text>().text = frag.ToString();
                AiIsDead = true;
                Amplitude.Instance.logEvent("CarIsDead1");

            }

            if (CarAi1.manage.energy <= 0 && !AiIsDead1)
            {
                CarAi1.manage.energy = 0;
                frag += 1;
                PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                enemyDestroy.GetComponent<Text>().text = frag.ToString();
                AiIsDead1 = true;
                Amplitude.Instance.logEvent("CarIsDead2");
            }

            if (CarAi2.manage.energy <= 0 && !AiIsDead2)
            {
                CarAi.manage.energy = 0;
                frag += 1;
                PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                enemyDestroy.GetComponent<Text>().text = frag.ToString();
                AiIsDead2 = true;
                Amplitude.Instance.logEvent("CarIsDead3");
            }
        }
        #endregion

        if (SceneManager.GetActiveScene().name == "battle_online")

        {
            if (!PV.IsMine)
                return;
 
                

            if (other.CompareTag("Car") && !isDead)
            {
                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 40f)
                {
                    energy -= 2;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 80f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 80f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 100f)
                {
                    energy -= 3;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 120f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 120f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 180f)
                {
                    energy -= 6;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 180f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 180f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 250f)
                {
                    energy -= 12;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 250f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 250f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 300f)
                {
                    energy -= 40;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    print(gameObject.name);
                    Amplitude.Instance.logEvent("CarHitOnspeed > 300f");
                }
                else
                {
                    energy -= 0;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }

                if (energy <= 0)
                {
                    energy = 0;
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    energyBarProgress.GetComponent<Text>().text = "0";
                    PlayerPrefs.SetInt("crashed", 1);
                    netManager.manage.CarCrashedNetwork();
                    isDead = true;
                    Amplitude.Instance.logEvent("DeadFromTrafficCar");

                }
            }

            if (other.CompareTag("Player") && !isDead)
            {

                    GetComponent<IDamageble>()?.TakeDamage(1);
                    energy -= 1;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 100f)
                {
                    GetComponent<IDamageble>()?.TakeDamage(3);
                    energy -= 3;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                }

                if (energy <= 0)
                {
                    energy = 0;
                    GetComponent<RCC_CarControllerV3>().KillEngine();
                    PlayerPrefs.SetInt("crashed", 1);
                    netManager.manage.CarCrashedNetwork();
                    Amplitude.Instance.logEvent("DeadFromOtherNetworkPlayer");
                    isDead = true;
                }
            }
        }
    }

    void Latency()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void StartEngine()
    {
        GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
    }

    void TargetRay()
    {
        Debug.DrawRay(transform.position + transform.up / 2f, transform.forward * 100.34f, Color.yellow);
        RaycastHit info;
        int mask = 1 << 10;
        if (Physics.Raycast(transform.position + transform.up / 2f, transform.forward * 100.34f, out info, 100.34f, mask))
        {
            print(gameObject.name);
        }
    }

    public void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]

    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;
        print("Damage +" +  damage);
    }
}
