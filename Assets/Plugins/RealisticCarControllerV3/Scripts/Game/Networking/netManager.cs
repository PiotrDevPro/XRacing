using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class netManager : MonoBehaviourPunCallbacks, IPunPrefabPool
{
    public static netManager manage;
    public ChatClient chatClient;
    public GameObject[] CarsPrefabs;
    public Transform spawnPoint;
    public Transform PlayerCarPoint;
    public GameObject nosButton;
    public Slider noslevel;
    public Text maxspd;
    public bool DangerSpeed;
    public bool isHighwayNetworkActive = false;
    int count = 0;
    private float starttime = 5f;
    private float curr = 0;

    //
    public GameObject LosePanel;
    

    public RCC_CarControllerV3 newVehicle;

    private void Awake()
    {
       manage = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 300;
        if (SceneManager.GetActiveScene().name == "battle_online")
        {
            LosePanel.SetActive(false);
        }
        
        curr = starttime;

        if (RCC_SceneManager.Instance.activePlayerVehicle)
            PhotonNetwork.Destroy(RCC_SceneManager.Instance.activePlayerVehicle.gameObject);

        if (PhotonNetwork.CurrentRoom.Name == "Highway") 
        {
            
            /// fog
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.9622642f, 0.7006145f, 0.4130474f);
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogDensity = 0.003f;

            if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 4
                || PlayerPrefs.GetInt("CurrentCar") == 5 ||  PlayerPrefs.GetInt("CurrentCar") == 8 || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("Cars/" + CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")].name, new Vector3(Random.Range(0f, -100.19f), Random.Range(4f, 6f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("hotrodd", new Vector3(Random.Range(0f, -100.19f), Random.Range(3.5f, 5f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponent<Rigidbody>().isKinematic = false;
                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("buggy", new Vector3(Random.Range(0f, -100.19f), Random.Range(3.5f, 5f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponent<Rigidbody>().isKinematic = false;
                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("gt500", new Vector3(Random.Range(0f, -100.19f), Random.Range(3.5f, 5f)), spawnPoint.rotation,0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("ig8", new Vector3(Random.Range(0f, -100.19f), Random.Range(3.5f, 5f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("lambo", new Vector3(Random.Range(0f, -100.19f), Random.Range(3.5f, 5f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("modelt", new Vector3(Random.Range(0f, -100.19f), Random.Range(4f, 6f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();

                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("towncar", new Vector3(Random.Range(0f, -100.19f), Random.Range(4f, 6f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
                isHighwayNetworkActive = true;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                Amplitude.Instance.logEvent("HighwayLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("karus_bus", new Vector3(Random.Range(0f, -100.19f), Random.Range(4f, 6f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();

                isHighwayNetworkActive = true;
            }

            
            

        } 
        else if (PhotonNetwork.CurrentRoom.Name == "City")
        {
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("hotrodd", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("buggy", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("gt500", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("ig8", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("lambo", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("modelt", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 10 )
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("towncar", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
                newVehicle.GetComponentInChildren<Rigidbody>().isKinematic = false;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("karus_bus", new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
            }
            else if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 4
                 || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 6 || PlayerPrefs.GetInt("CurrentCar") == 7 || PlayerPrefs.GetInt("CurrentCar") == 8 || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                Amplitude.Instance.logEvent("CityLevelNetwork");
                newVehicle = PhotonNetwork.Instantiate("Cars/" + CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")].name, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation, 0).GetComponent<RCC_CarControllerV3>();
            }

            
        }

        RCC.RegisterPlayerVehicle(newVehicle);
        RCC.SetControl(newVehicle, true);

        #region Car Lights
        if (PlayerPrefs.GetInt("CurrentCar") == 4 || PlayerPrefs.GetInt("CurrentCar") == 6)
        {
            GameObject light = GameObject.Find("Ilum");
            light.SetActive(false);
        }

        if (PlayerPrefs.GetInt("CurrentCar") == 12)
        {
            GameObject light = GameObject.Find("rearGroundFlares");
            light.SetActive(false);
        }
        #endregion

        #region Load Wheel Susspens
        if (PlayerPrefs.GetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar")) != 0)
        {
            if (PlayerPrefs.GetInt("CurrentCar") != 8 || PlayerPrefs.GetInt("CurrentCar") != 10)
            {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar"));
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar"));

            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspRear" + PlayerPrefs.GetInt("CurrentCar"));
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspRear" + PlayerPrefs.GetInt("CurrentCar"));

            
            //newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            //newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;

           //newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
           //newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 8)
            {
                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.02f;
                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.02f;

                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.02f;
                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.02f;
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {
                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.05f;
                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.05f;

                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.05f;
                newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.05f;
            }
        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;

            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;
        }

        #endregion
        
        #region Load EngineUpdate
        if (MainMenuManager.manage.svChecked.engine0)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().maxspeed = newVehicle.GetComponentInChildren<RCC_CarControllerV3>().defMaxSpeed;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().engineTorque = newVehicle.GetComponentInChildren<RCC_CarControllerV3>().engineTorque;
        }
        if (MainMenuManager.manage.svChecked.engine1)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 15f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 250f;
        }
        if (MainMenuManager.manage.svChecked.engine2)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 30f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 350f;
        }

        if (MainMenuManager.manage.svChecked.engine3)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 50f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 500f;
        }

        if (MainMenuManager.manage.svChecked.engine4)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 70f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 600f;
        }

        if (MainMenuManager.manage.svChecked.engine5)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 80f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 700f;
        }
        if (MainMenuManager.manage.svChecked.engine6)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 90f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 800f;
        }

        if (MainMenuManager.manage.svChecked.engine7)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 100f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 900f;
        }

        if (MainMenuManager.manage.svChecked.engine8)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 110f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 1200f;
        }
        if (MainMenuManager.manage.svChecked.engine9)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 115f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 1300f;
        }

        if (MainMenuManager.manage.svChecked.engine10)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 125f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 1400f;
        }

        if (MainMenuManager.manage.svChecked.engine11)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 135f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 1500f;
        }
        if (MainMenuManager.manage.svChecked.engine12)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 145f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 1600f;
        }
        if (MainMenuManager.manage.svChecked.engine13)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 155f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 1700f;
        }

        if (MainMenuManager.manage.svChecked.engine14)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 175f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 1850f;
        }
        if (MainMenuManager.manage.svChecked.engine15)
        {
            newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed += 195f;
            newVehicle.GetComponent<RCC_CarControllerV3>().engineTorque += 2150f;
        }
        #endregion

        #region TCS 
        if (PlayerPrefs.GetInt("TCS" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectTCS" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().TCSThreshold = PlayerPrefs.GetFloat("TCSsetup" + PlayerPrefs.GetInt("CurrentCar"));
        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().TCS = false;
        }
        #endregion

        #region ESP
        if (PlayerPrefs.GetInt("ESP" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectESP" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ESPThreshold = PlayerPrefs.GetFloat("ESPsetup" + PlayerPrefs.GetInt("CurrentCar"));
        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ESP = false;
        }
        #endregion

        #region Traction 
        if (PlayerPrefs.GetInt("Traction" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectTraction" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().tractionHelperStrength = PlayerPrefs.GetFloat("TractionSetup" + PlayerPrefs.GetInt("CurrentCar"));
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().tractionHelper = true;
        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().tractionHelper = false;
        }

        #endregion

        #region load Car Config
        if (!MainMenuManager.NOSisChecked)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().useNOS = false;
            nosButton.SetActive(false);
            noslevel.interactable = false;
            noslevel.maxValue = 0;

        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().useNOS = true;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 35f;
            nosButton.SetActive(true);
            noslevel.interactable = true;
        }
        if (!MainMenuManager.TurboisChecked)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().useTurbo = false;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().useExhaustFlame = false;

        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().useTurbo = true;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 10f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().useExhaustFlame = true;
        }
        if (!MainMenuManager.ABSisChecked)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABS = false;
        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABS = true;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.1f;
        }

        #endregion

        #region Load HandlingUpdate
        if (MainMenuManager.manage.svChecked.handling0)
        {
            //InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
            //InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
        }
        if (MainMenuManager.manage.svChecked.handling1)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
        }
        if (MainMenuManager.manage.svChecked.handling2)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.1f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.1f;
        }
        if (MainMenuManager.manage.svChecked.handling3)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.15f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.15f;
        }
        if (MainMenuManager.manage.svChecked.handling4)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.2f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.2f;
        }
        if (MainMenuManager.manage.svChecked.handling5)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.25f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.25f;
        }
        if (MainMenuManager.manage.svChecked.handling6)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.28f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.28f;
        }
        if (MainMenuManager.manage.svChecked.handling7)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.35f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.35f;
        }
        if (MainMenuManager.manage.svChecked.handling8)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.45f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.45f;
        }
        if (MainMenuManager.manage.svChecked.handling9)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.6f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.6f;
        }
        if (MainMenuManager.manage.svChecked.handling10)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.7f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.7f;
        }
        if (MainMenuManager.manage.svChecked.handling11)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.8f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.8f;
        }
        if (MainMenuManager.manage.svChecked.handling12)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.9f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.9f;
        }
        #endregion

        #region BrakeUpdate
        if (MainMenuManager.manage.svChecked.brake0)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold = 0.1f;
            // InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque = 1200f;

        }
        if (MainMenuManager.manage.svChecked.brake1)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.025f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 100f;
        }
        if (MainMenuManager.manage.svChecked.brake2)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.05f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 200f;
        }
        if (MainMenuManager.manage.svChecked.brake3)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.075f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 300f;
        }
        if (MainMenuManager.manage.svChecked.brake4)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.125f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 400f;
        }
        if (MainMenuManager.manage.svChecked.brake5)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.2f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 500f;
        }
        if (MainMenuManager.manage.svChecked.brake6)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.225f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 600f;
        }
        if (MainMenuManager.manage.svChecked.brake7)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.3f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 700f;
        }
        if (MainMenuManager.manage.svChecked.brake8)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.35f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 800f;
        }
        if (MainMenuManager.manage.svChecked.brake9)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.4f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 900f;
        }
        if (MainMenuManager.manage.svChecked.brake10)
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.45f;
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 1000f;
        }
        #endregion

        #region Wheel Drive Mode
        if (MainMenuManager.WheelDriveisChecked)
        {
            switch (PlayerPrefs.GetString("DriveMode"))
            {
                case "":
                    newVehicle.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
                    break;
                case "RWD":
                    newVehicle.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
                    break;
                case "FWD":
                    newVehicle.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.FWD;
                    break;
                case "AWD":
                    newVehicle.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.AWD;
                    break;
            }
        }
        else
        {
            newVehicle.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
        }
        #endregion

        //InstantiatedCar.GetComponent<BoxCollider>().tag = "";

    }

    private void Update()
    {
        maxspd.text = "MAX:" + newVehicle.GetComponent<RCC_CarControllerV3>().maxspeed.ToString() + "KM/H";
        if (newVehicle.GetComponent<RCC_CarControllerV3>().speed > 5f)
        {
            DangerSpeed = true;

            count += 1;
            if (count == 1)
            {
                Amplitude.Instance.logEvent("StartRide > 5 kmh");

            }

        } 
        else
        {
            DangerSpeed = false;
        }
    }
    public void Leave()
    {
        if (SceneManager.GetActiveScene().name == "battle_online")
        {
            PhotonNetwork.LeaveRoom();
            isHighwayNetworkActive = false;
            Time.timeScale = 1;
        } 
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("garage");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
        RCC_InfoLabel.Instance.ShowInfo("Join " + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left the room", otherPlayer.NickName);
        RCC_InfoLabel.Instance.ShowInfo("Left " + otherPlayer.NickName);
    }


    void Timer()
    {
        print(curr);
        curr -= 1 * Time.deltaTime;
        if (curr <= 0)
        {
            curr = 0;
        }
    }

    public void CarCrashedNetwork()
    {
        LosePanel.SetActive(true);
    }

    public void ForAds()
    {
      //if  (!photonView.IsMine )
        //    return;
            newVehicle.GetComponent<RCC_CarControllerV3>().KillOrStartEngine();
            CarDamage.manage.energy = CarDamage.manage.energy + 50;
            CarDamage.manage.energyBarProgress.GetComponent<Text>().text = CarDamage.manage.energy.ToString();
            CarDamage.manage.isDead = false;
            LosePanel.SetActive(false); 
    }
    
   public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
   //     newVehicle = Instantiate(CathingLoadFiles.manage.townCar_ab.name, new Vector3(Random.Range(0f, -100.19f), Random.Range(4f, 6f)), spawnPoint.rotation).GetComponent<RCC_CarControllerV3>();
        return null;
    }

    public void Destroy(GameObject gameObject)
    {
        
    }
   
}
