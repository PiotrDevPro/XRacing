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

    public Player Player { get; private set; }

    int count = 0;
    public int frag;
    private int fragRec;

    private void Update()
    {
        count += 1;
        if (SceneManager.GetActiveScene().name == "battle_online" && count ==1)
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
                        PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI")+1);
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead = true;
                        PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 35);
                        Amplitude.Instance.logEvent("CarIsDead1");

                    }

                    if (CarAi1.manage.energy <= 0 && !AiIsDead1)
                    {
                        frag += 1;
                        PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead1 = true;
                        PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 35);
                        Amplitude.Instance.logEvent("CarIsDead2");
                    }

                    if (CarAi2.manage.energy <= 0 && !AiIsDead2)
                    {
                        frag += 1;
                        PlayerPrefs.SetInt("fragAI", PlayerPrefs.GetInt("fragAI") + 1);
                        enemyDestroy.GetComponent<Text>().text = frag.ToString();
                        AiIsDead2 = true;
                        PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 35);
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
                    energy -= 8;
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
                    energy -= 3;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 80f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 80f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 100f)
                {
                    energy -= 5;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 120f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 120f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 180f)
                {
                    energy -= 10;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 180f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 180f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 250f)
                {
                    energy -= 15;
                    energyBarProgress.GetComponent<Text>().text = energy.ToString();
                    Amplitude.Instance.logEvent("CarHitOnspeed > 250f");
                    print(gameObject.name);
                    print("CarHitOnspeed > 250f");
                }

                if (netManager.manage.newVehicle.GetComponent<RCC_CarControllerV3>().speed > 300f)
                {
                    energy -= 45;
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
