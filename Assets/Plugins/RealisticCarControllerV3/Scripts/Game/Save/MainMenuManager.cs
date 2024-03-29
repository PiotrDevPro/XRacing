﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public enum PanelsUI { MainMenu = 0, SelectCar = 1, SelectLevel = 2, Settings = 3, _NetworkRoom = 4, Auth = 5, ArenaLevel =6}

public class MainMenuManager : MonoBehaviour
{

    public static MainMenuManager manage;
    private RCC_PhotonNetwork photonNet;

    int tEngine, tHandling, tBrake, tWheel;
    public CarSetting[] carSetting;
    public Transform CarManager;
    public Transform qualityPanel;
    public MenuPanels menuPanels;
    public LevelSetting levelSettingz;
    public SvCheck svChecked;
    public MenuGUI menuGUI;
    public AudioUI audioUi;
    public AudioSource[] tracks;
    public Text cashAmount;
    private GameObject _car_mirrorActive;
    [SerializeField] Text Rating;
    
    public Text energyTx;
    [SerializeField] private GameObject versionApp;
    public GameObject network_manager_active;
    [HideInInspector] public bool isFreerideActive = false;
    [HideInInspector] public bool isCheckpointLevel = false;
    [HideInInspector] public bool isAllvsYou = false;
    [HideInInspector] public bool isTopSpeedActive = false;
    [HideInInspector] public bool isQuickRace = false;
    [HideInInspector] public bool isCity = false;
    //Arena Mode
    [HideInInspector] public bool isArena = false;
    [HideInInspector] public bool isArena1 = false;
    [HideInInspector] public bool isArena2 = false;
    [HideInInspector] public bool isArena3 = false;
    [HideInInspector] public bool isArena4 = false;
    [HideInInspector] public bool isArena5 = false;
    [HideInInspector] public bool isArena6 = false;
    [HideInInspector] public bool isArena7 = false;
    [HideInInspector] public bool isArena8 = false;
    [HideInInspector] public bool isArena9 = false;
    [HideInInspector] public bool isArena10 = false;
    [HideInInspector] public bool isArena11 = false;
    [HideInInspector] public bool isArena12 = false;
    [HideInInspector] public bool isArena13 = false;
    [HideInInspector] public bool isArena14 = false;
    [HideInInspector] public bool isArena15 = false;

    // Checkpoint Mode
    [HideInInspector] public bool isCheckpoint = false;
    [HideInInspector] public bool isCheckpoint1 = false;
    // Menu Settings
    [HideInInspector] public bool isMainMenu = false;
    [HideInInspector] public bool isSelectLevel = false;
    [HideInInspector] public bool isSelectCar = false;
    [HideInInspector] public bool isNetMenu = false;
    [HideInInspector] public bool isSettings = false;
    [HideInInspector] public bool isAuthMenu = false;
    [HideInInspector] public bool isArenaMenu = false;
    [Header("Pool Prefab Manager")]
    public GameObject PoolPrefActive;
    [Header("Other obj")]
    [SerializeField] GameObject RatingForCarPanel;
    [SerializeField] Text inAppCarPrice;
    [SerializeField] Text ratingforCar;
    public GameObject loadingPanel;
    [Header("InAppCars")]
    public GameObject inAppLamboPanel;
    public GameObject inAppRRPanel;
    public GameObject inAppVettyPanel;
    [SerializeField] Text inAppCarPriceLambo;
    [SerializeField] Text inAppCarPriceRR;
    [SerializeField] Text inAppCarPriceVetty;
    [Header("Achiv")]
    [SerializeField] Text AchivText;
    [SerializeField] Text CrownText;
    //public bool isCityNetworkRoom;
    //public GameObject maxSpeedActive;

    #region isChecked static bool
    public static bool NOSisChecked = false;
    public static bool TurboisChecked = false;
    public static bool ABSisChecked = false;
    public static bool _EngineisChecked = true;
    public static bool WheelSusspIsChecked = false;
    public static bool TCSisChecked = false;
    public static bool ESPisChecked = false;
    public static bool TractionIsChecked = false;
    public static bool WheelDriveisChecked = false;
    #endregion

    private float speedfill;
    private float turbofill;
    private float handlingfill;
    private float driftfill;

    private SystemLanguage _systemLang;

    [System.Serializable]
    public class CarSetting
    {
        public string name = "Car 1";

        public int price = 20000;

        public float priceUSD;

        public int nitroPrice = 1000;
        public int turboPrice = 500;
        public int _ABSPrice = 500;

        public int[] WheelSusspensionPrice;
        public int[] TCSPrice;
        public int[] ESPPrice;
        public int[] TractionPrice;
        public int[] WheelDrivePrice;
        public int energy;
        public int CarRating;
        public bool isInAppCar = false;


        public GameObject car;

        public Material body, wheel, detail;

        public Color[] Colors;

        public GameObject[] achievements_;

        public CarPower carPower;

        [HideInInspector]
        public bool Bought = false;

        [System.Serializable]
        public class CarPower
        {
            public int maxUpgradeLevel = 10;
            public int maxUpgradeLevelHandling = 10;
            public int maxUpgradeLevelBrake = 10;
            public int[] engine;
            public int[] handling;
            public int[] brake;
            public int speed = 220;
        }
    }

    [System.Serializable]
    public class AudioUI
    {
        //public AudioSource menuMusic;
        public AudioSource fxAudio;
        public AudioSource fxCash;
        public AudioSource fxAudioturbo;
        public AudioSource fxAudioturbo2;
        public AudioSource fxAudioclick2;
        public AudioSource fxAudioTuning;
        
        //public AudioClip tuning;
        public AudioSource audSource;
        public AudioSource ColorSelect;
        public AudioSource ColorBuy;
        public AudioSource Denied;
        
    }

    [System.Serializable]
    public class MenuPanels
    {
        public GameObject MainMenu;
        public GameObject SelectCar;
        public GameObject SelectLevel;
        public GameObject _NetworkRoom;
        public GameObject EnoughMoney;
        //public GameObject FreeridePanel;
        public GameObject ArenaPanel;
        public GameObject TopSpeedPanel;
        public GameObject CheckpointPanel;
        public GameObject AchivmentPanel;
        public GameObject FreerideMainPanel;
        public GameObject CityPanel;
        public GameObject Settings;
        public GameObject Auth;
        public GameObject Leaderboard;
        public GameObject Ratingboard;
        [Header("Buttons")]
        public Button FreerideBtn;
    }

    [System.Serializable]
    public class LevelSetting
    {
        public bool locked = true;
        public Button panel;
        public Text bestTime;
        public Image lockImage;
        public StarClass stars;
        [Header("LockImageArena")]
        public GameObject lock1;
        public GameObject lock2;
        public GameObject lock3;
        public GameObject lock4;
        public GameObject lock5;
        public GameObject lock6;
        public GameObject lock7;
        public GameObject lock8;
        public GameObject lock9;
        public GameObject lock10;
        public GameObject lock11;
        public GameObject lock12;
        public GameObject lock13;
        public GameObject lock14;


        [Header("ArenaLevels")]
        public Button ArenaLv2;
        public Button ArenaLv3;
        public Button ArenaLv4;
        public Button ArenaLv5;
        public Button ArenaLv6;
        public Button ArenaLv7;
        public Button ArenaLv8;
        public Button ArenaLv9;
        public Button ArenaLv10;
        public Button ArenaLv11;
        public Button ArenaLv12;
        public Button ArenaLv13;
        public Button ArenaLv14;

        [Header("CheckpointLevels")]
        public Button CheckpointLv2;

        [Header("LockImageCheckpoint")]
        public GameObject lockCHK1;

        [System.Serializable]
        public class StarClass
        {
            public Image Star1, Star2, Star3;
        }
    }

    [System.Serializable]
    public class SvCheck
    {

        public bool isCheckedNOS = false;
        public bool isCheckedTurbo = false;
        public bool _ABSisChecked_ = true;


        #region Engine Check
        public bool engine0 = false;
        public bool engine1 = false;
        public bool engine2 = false;
        public bool engine3 = false;
        public bool engine4 = false;
        public bool engine5 = false;
        public bool engine6 = false;
        public bool engine7 = false;
        public bool engine8 = false;
        public bool engine9 = false;
        public bool engine10 = false;
        public bool engine11 = false;
        public bool engine12 = false;
        public bool engine13 = false;
        public bool engine14 = false;
        public bool engine15 = false;
        #endregion

        #region Engine select
        public bool engin0 = true;
        public bool engin1 = true;
        public bool engin2 = true;
        public bool engin3 = true;
        public bool engin4 = true;
        public bool engin5 = true;
        public bool engin6 = true;
        public bool engin7 = true;
        public bool engin8 = true;
        public bool engin9 = true;
        public bool engin10 = true;
        public bool engin11 = true;
        public bool engin12 = true;
        public bool engin13 = true;
        public bool engin14 = true;
        public bool engin15 = true;
        #endregion

        #region Handling Check
        public bool handling0 = false;
        public bool handling1 = false;
        public bool handling2 = false;
        public bool handling3 = false;
        public bool handling4 = false;
        public bool handling5 = false;
        public bool handling6 = false;
        public bool handling7 = false;
        public bool handling8 = false;
        public bool handling9 = false;
        public bool handling10 = false;
        public bool handling11 = false;
        public bool handling12 = false;

        #endregion

        #region Handling Select
        public bool handl0 = true;
        public bool handl1 = true;
        public bool handl2 = true;
        public bool handl3 = true;
        public bool handl4 = true;
        public bool handl5 = true;
        public bool handl6 = true;
        public bool handl7 = true;
        public bool handl8 = true;
        public bool handl9 = true;
        public bool handl10 = true;
        public bool handl11 = true;
        public bool handl12 = true;

        #endregion

        #region Brake Check
        public bool brake0 = false;
        public bool brake1 = false;
        public bool brake2 = false;
        public bool brake3 = false;
        public bool brake4 = false;
        public bool brake5 = false;
        public bool brake6 = false;
        public bool brake7 = false;
        public bool brake8 = false;
        public bool brake9 = false;
        public bool brake10 = false;
        #endregion

        #region Brake Select
        public bool brak0 = true;
        public bool brak1 = true;
        public bool brak2 = true;
        public bool brak3 = true;
        public bool brak4 = true;
        public bool brak5 = true;
        public bool brak6 = true;
        public bool brak7 = true;
        public bool brak8 = true;
        public bool brak9 = true;
        public bool brak10 = true;
        #endregion

        #region Color select
        public bool isCheckBody = false;
        public bool isCheckWheels = false;
        public bool isCheckDetail = false;
        #endregion
    }

    [System.Serializable]
    public class MenuGUI
    {
        //int tEngine, tHandling, tBrake;
        //public Text GameScore;
        public Button adsEnergyBtn;
        public Text _playerName;
        public Text CarName;
        public Text CarPrice;
       // public Text CarMaxSpeed;
        public Text SusspFrontTx;
        public Text SusspRearTx;
        public Text TCSTx;
        public Text TractionTx;
        public Text ESPTx;
        public Image barSpeed;
        public Image barTurbo;
        public Image barHandling;
        public Image barDrift;

        public Image barTuningSpeed;
        public Image barTuningTurbo;
        public Image barTuningHandling;
        public Image barTuningDrift;

        public Slider sensitivity;
        public Slider frontS;
        public Slider RearS;
        public Slider TCS;
        public Slider ESP;
        public Slider Traction;

        public Toggle audio;
        public Toggle sound;
        public Toggle music;
        public Toggle vibrateToggle;
        public Toggle ButtonMode, SteeringWheelMode;
        public Toggle TCStgl;
        public Toggle ESPtgl;
        public Toggle RWD, FWD, AWD;
        public Toggle TractionTgl;


        public Image bodyColor, smokeColor;
        public Image loadingBar;
        public GameObject lockedNos, lockedTurbo, lockedABS, lockedWheelSusspF, lockedWheelSusspR, lockedTCS, lockedESP, lockedTraction, lockedWheelDrive;

        //CoinFx
        public Transform rewardPrefab;
        public Transform destanition;
        public Transform parent;
        public int amount;

        public GameObject loading;
        public GameObject customizeVehicle;
        public Button buyNewVehicle;
        public GameObject levelChooser;
        public GameObject RUSure;
        public GameObject NoAdsBtn;

        public Button Nitro, Turbo, ABS;

        public int xResolution = 2160, yResolution = 1080;

        [Header("Cars Upgrade System")]
        [Space(3)]
        public Text NitroPrice;
        public Text TurboPrice;
        public Text ABSPrice;
        public Text EnginePrice;
        public Text engineUpLevel;
        public Text HandlingPrice;
        public Text HandlingUpLevel;
        public Text BrakeUpLevel;
        public Text BrakePrice;
        public Text WheelSusspPriceF;
        public Text WheelSusspPriceR;
        public Text TCSPrice;
        public Text ESPPrice;
        public Text TractionPrice;
        public Text WheelDrivePrice;
        public Toggle carUseNos;
        public Toggle carUseTurbo;
        public Toggle carUseABS;
        // public Button HandlingBtn;

    }

    private CarSetting currentCar;
    public int currentCarNumber = 0;
    private PanelsUI activePanel = PanelsUI.MainMenu;
    private AsyncOperation sceneLoadingOperation = null;
    private Color mainColor;



    #region Setting

    public void CurrentPanel(int current)
    {
        activePanel = activePanel = (PanelsUI)current;


        if (currentCarNumber != PlayerPrefs.GetInt("CurrentCar"))
        {
            currentCarNumber = PlayerPrefs.GetInt("CurrentCar");
            

            foreach (CarSetting VSetting in carSetting)
            {

                if (VSetting == carSetting[currentCarNumber])
                {
                    
                    VSetting.car.SetActive(true);
                    print(currentCarNumber);
                    currentCar = VSetting;

                }
                else
                {
                    VSetting.car.SetActive(false);
            
                }
            }
        }

        switch (activePanel)
        {

            case PanelsUI.MainMenu:
                
                network_manager_active.SetActive(false);
                menuPanels.MainMenu.SetActive(true);
                menuPanels.SelectCar.SetActive(false);
                menuPanels.SelectLevel.SetActive(false);
                menuPanels._NetworkRoom.SetActive(false);
                menuGUI._playerName.text = PlayerPrefs.GetString("Player");
                Amplitude.Instance.logEvent("MainMenu");
                isMainMenu = true;
                isSelectLevel = false;
                isSelectCar = false;
                isAuthMenu = false;
                isNetMenu = false;
                isSettings = false;
                break;
            case PanelsUI.SelectCar:
                menuPanels.MainMenu.gameObject.SetActive(false);
                menuPanels.SelectCar.SetActive(true);
                menuPanels.SelectLevel.SetActive(false);
                menuPanels._NetworkRoom.SetActive(false);
                menuGUI._playerName.text = PlayerPrefs.GetString("Player");
                Amplitude.Instance.logEvent("SelectCar");
                energyTx.text = "Energy: " + (carSetting[currentCarNumber].energy + PlayerPrefs.GetInt("Energy"));
                isMainMenu = false;
                isSelectLevel = false;
                isSelectCar = true;
                isAuthMenu = false;
                isNetMenu = false;
                isSettings = false;
                break;
            case PanelsUI.SelectLevel:
                menuPanels.MainMenu.SetActive(false);
                menuPanels.SelectCar.SetActive(false);
                menuPanels.SelectLevel.SetActive(true);
                menuPanels._NetworkRoom.SetActive(false);
                menuGUI._playerName.text = PlayerPrefs.GetString("Player");
                Amplitude.Instance.logEvent("SelectLevel");
                isMainMenu = false;
                isSelectLevel = true;
                isSelectCar = false;
                isAuthMenu = false;
                isNetMenu = false;
                isSettings = false;

                break;
            case PanelsUI.Auth:
                menuPanels.MainMenu.SetActive(false);
                menuPanels.SelectCar.SetActive(false);
                menuPanels.SelectLevel.SetActive(false);
                menuPanels._NetworkRoom.SetActive(false);
                menuPanels.Auth.SetActive(true);
                Amplitude.Instance.logEvent("AuthPanel");
                isMainMenu = false;
                isSelectLevel = false;
                isSelectCar = false;
                isAuthMenu = true;
                isNetMenu = false;
                isSettings = false;
                break;
            case PanelsUI._NetworkRoom:
                menuPanels.MainMenu.SetActive(false);
                menuPanels.SelectCar.SetActive(false);
                menuPanels.SelectLevel.SetActive(false);
                menuPanels._NetworkRoom.SetActive(true);
                Amplitude.Instance.logEvent("NetworkRoom");
                isMainMenu = false;
                isSelectLevel = false;
                isSelectCar = false;
                isAuthMenu = false;
                isNetMenu = true;
                isSettings = false;
                break;
            case PanelsUI.Settings:
                menuPanels.MainMenu.SetActive(false);
                menuPanels.SelectCar.SetActive(false);
                menuPanels.SelectLevel.SetActive(false);
                Amplitude.Instance.logEvent("Options");
                isMainMenu = false;
                isSelectLevel = false;
                isSelectCar = false;
                isAuthMenu = false;
                isNetMenu = false;
                isSettings = true;
                break;
            case PanelsUI.ArenaLevel:
                menuPanels.MainMenu.SetActive(false);
                menuPanels.SelectCar.SetActive(false);
                menuPanels.SelectLevel.SetActive(false);
                menuPanels.ArenaPanel.SetActive(true);
                Amplitude.Instance.logEvent("ArenaPanels");
                isMainMenu = false;
                isSelectLevel = false;
                isSelectCar = false;
                isAuthMenu = false;
                isNetMenu = false;
                isSettings = false;
                isArenaMenu = false;
                break;
        }

    }

    public void SettingActive(bool activePanel)
    {
        menuPanels.Settings.gameObject.SetActive(activePanel);
    }

    public void DisableAudioButton(Toggle toggle)
    {
        if (toggle.isOn)
        {
            audioUi.fxAudio.mute = false;
            audioUi.fxAudioclick2.mute = false;
            audioUi.fxAudioTuning.mute = false;
            audioUi.fxAudioturbo.mute = false;
            audioUi.fxAudioturbo2.mute = false;
            audioUi.fxCash.mute = false;
            PlayerPrefs.SetInt("AudioActive", 0);
        }
        else
        {
            //AudioListener.volume = 0;
            audioUi.fxAudio.mute = true;
            audioUi.fxAudioclick2.mute = true;
            audioUi.fxAudioTuning.mute = true;
            audioUi.fxAudioturbo.mute = true;
            audioUi.fxAudioturbo2.mute = true;
            audioUi.fxCash.mute = true;
            PlayerPrefs.SetInt("AudioActive", 1);
        }
    }

    public void DisableMusicButton(Toggle toggle)
    {
        if (toggle.isOn)
        {
            tracks[Random.Range(0, 12)].GetComponentInChildren<AudioSource>().Play();

            #region MusicFromAssetBundle
            // CathingLoadFiles.manage.track12_isLoaded.GetComponentInChildren<AudioSource>().volume = 0.732f;
            // CathingLoadFiles.manage.track13_isLoaded.GetComponentInChildren<AudioSource>().volume = 0.23f;
            // CathingLoadFiles.manage.track15_isLoaded.GetComponent<AudioSource>().volume = 0.7f;

            // AudioSource[] _tracks = new AudioSource[] { tracks[1], tracks[2], tracks[3], tracks[4], tracks[5], 
            //   CathingLoadFiles.manage.track12_isLoaded.GetComponentInChildren<AudioSource>(), CathingLoadFiles.manage.track11_isLoaded.GetComponentInChildren<AudioSource>(),
            //       CathingLoadFiles.manage.track13_isLoaded.GetComponentInChildren<AudioSource>(),CathingLoadFiles.manage.track14_isLoaded.GetComponentInChildren<AudioSource>(),
            //           CathingLoadFiles.manage.track15_isLoaded.GetComponentInChildren<AudioSource>(), CathingLoadFiles.manage.track16_isLoaded.GetComponentInChildren<AudioSource>(),
            //               CathingLoadFiles.manage.track17_isLoaded.GetComponentInChildren<AudioSource>(), CathingLoadFiles.manage.track18_isLoaded.GetComponentInChildren<AudioSource>()};
            //_tracks[Random.Range(0, _tracks.Length)].Play();
            #endregion

            PlayerPrefs.SetInt("MusicActive", 0);
            Amplitude.Instance.logEvent("MainMenu->MusicON");
        }
        else
        {
            tracks[0].Stop();
            tracks[1].Stop();
            tracks[2].Stop();
            tracks[3].Stop();
            tracks[4].Stop();
            tracks[5].Stop();
            tracks[6].Stop();
            tracks[7].Stop();
            tracks[8].Stop();
            tracks[9].Stop();
            tracks[10].Stop();
            tracks[11].Stop();
            tracks[12].Stop();
            //  CathingLoadFiles.manage.track11_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            // CathingLoadFiles.manage.track12_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            // CathingLoadFiles.manage.track13_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            // CathingLoadFiles.manage.track14_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            // CathingLoadFiles.manage.track15_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            // CathingLoadFiles.manage.track16_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            // CathingLoadFiles.manage.track17_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            // CathingLoadFiles.manage.track18_isLoaded.GetComponentInChildren<AudioSource>().Stop();
            PlayerPrefs.SetInt("MusicActive", 1);
            Amplitude.Instance.logEvent("MainMenu->MusicOFF");
        }
    }

    public void DisableSoundButton(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("SoundActive", 0);
            Amplitude.Instance.logEvent("MainMenu->SoundON");
        }
        else
        {
            PlayerPrefs.SetInt("SoundActive", 1);
            Amplitude.Instance.logEvent("MainMenu->SoundOFF");
        }
    }

    public void DisableVibration(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("VibrationActive", 0);
            Amplitude.Instance.logEvent("MainMenu->VibroON");
        }

        else
        {
            PlayerPrefs.SetInt("VibrationActive", 1);
            Amplitude.Instance.logEvent("MainMenu->VibroOFF");
        }
            
    }

    public void DisableSteeringWheelMode(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("SteeringWheel", 0);
        }
        else
        {
            PlayerPrefs.SetInt("SteeringWheel", 1);
        }
    }

    public void DisableButtonMode(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("ButtonMode", 0);
        }
        else
        {
            PlayerPrefs.SetInt("ButtonMode", 1);
        }
    }

    public void SoundOnOff()
    {
        AudioListener.pause = ((PlayerPrefs.GetInt("SoundActive") == 1) ? true : false);

    }

    public void Soundtracks()
    {
        if (PlayerPrefs.GetInt("MusicActive") != 1)
        {
            tracks[Random.Range(0,12)].GetComponent<AudioSource>().Play();
        }
    }

    public void StartSoundtrack()
    {
        #region MusicFromAssetBundle
        // CathingLoadFiles.manage.track12_isLoaded.GetComponentInChildren<AudioSource>().volume = 0.732f;
        // CathingLoadFiles.manage.track15_isLoaded.GetComponent<AudioSource>().volume = 0.7f;
        // CathingLoadFiles.manage.track13_isLoaded.GetComponentInChildren<AudioSource>().volume = 0.23f;

        // AudioSource[] _tracks = new AudioSource[] { tracks[1], tracks[2], tracks[3], tracks[4], tracks[5], CathingLoadFiles.manage.track12_isLoaded.GetComponentInChildren<AudioSource>(), CathingLoadFiles.manage.track11_isLoaded.GetComponentInChildren<AudioSource>(), 
        //     CathingLoadFiles.manage.track13_isLoaded.GetComponentInChildren<AudioSource>(),CathingLoadFiles.manage.track14_isLoaded.GetComponentInChildren<AudioSource>(),
        //         CathingLoadFiles.manage.track15_isLoaded.GetComponentInChildren<AudioSource>(), CathingLoadFiles.manage.track16_isLoaded.GetComponentInChildren<AudioSource>(),
        //             CathingLoadFiles.manage.track17_isLoaded.GetComponentInChildren<AudioSource>(), CathingLoadFiles.manage.track18_isLoaded.GetComponentInChildren<AudioSource>()};
        // _tracks[Random.Range(0, _tracks.Length)].Play();
        #endregion

    }

    public void RWDMode(Toggle value)
    {
        if (value.isOn)
            PlayerPrefs.SetString("DriveMode", "RWD");
    }
    public void FWDMode(Toggle value)
    {
        if (value.isOn)
            PlayerPrefs.SetString("DriveMode", "FWD");
    }
    public void AWDMode(Toggle value)
    {
        if (value.isOn)
            PlayerPrefs.SetString("DriveMode", "AWD");
    }

    public void LoadWheelDriveOnAwake()
    {
        switch (PlayerPrefs.GetString("DriveMode"))
        {
            case "":
                menuGUI.RWD.isOn = true;
                break;
            case "RWD":
                menuGUI.RWD.isOn = true;
                break;
            case "FWD":
                menuGUI.FWD.isOn = true;
                break;
            case "AWD":  
                menuGUI.AWD.isOn = true;
                break;
        }
        switch (PlayerPrefs.GetString("DriveMode"))
        {
            case "":
                PlayerPrefs.SetString("DriveMode","RWD");  
                menuGUI.RWD.isOn = true;
                break;
            case "RWD":               
                menuGUI.RWD.isOn = true;
                break;
            case "FWD":
                menuGUI.FWD.isOn = true;
                break;
            case "AWD":
                menuGUI.AWD.isOn = true;
                break;
        }
    }

    public void ButtonScale()
    {
        if (PlayerPrefs.GetInt("QualitySettings") == 0)
        {
            qualityPanel.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
        }

        else
        {
            qualityPanel.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(2).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(3).GetComponent<RectTransform>().localScale = Vector3.one;
        }

        if (PlayerPrefs.GetInt("QualitySettings") == 2)
        {
            qualityPanel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
        }
        if (PlayerPrefs.GetInt("QualitySettings") == 4)
        {
            qualityPanel.GetChild(2).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
        }

        if (PlayerPrefs.GetInt("QualitySettings") == 5)
        {
            qualityPanel.GetChild(3).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
        }
    }

    public void QualitySetting(int quality)
    {
        QualitySettings.SetQualityLevel(quality, true);
        PlayerPrefs.SetInt("QualitySettings", quality);

        if (PlayerPrefs.GetInt("QualitySettings") == 0)
        {
            qualityPanel.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
            qualityPanel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(2).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(3).GetComponent<RectTransform>().localScale = Vector3.one;
        }

        // else

        if (PlayerPrefs.GetInt("QualitySettings") == 2)
        {
            qualityPanel.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
            qualityPanel.GetChild(2).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(3).GetComponent<RectTransform>().localScale = Vector3.one;
        }
        if (PlayerPrefs.GetInt("QualitySettings") == 4)
        {
            qualityPanel.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(2).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f; ;
            qualityPanel.GetChild(3).GetComponent<RectTransform>().localScale = Vector3.one;
        }

        if (PlayerPrefs.GetInt("QualitySettings") == 5)
        {
            qualityPanel.GetChild(0).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(2).GetComponent<RectTransform>().localScale = Vector3.one;
            qualityPanel.GetChild(3).GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
            UniAdManager.manage.ShowInterstatial();
        }


    }

    public void LoadQualitySetting()
    {
        if (PlayerPrefs.GetInt("QualitySettings") == 0)
        {
            PlayerPrefs.SetInt("QualitySettings", 0);
            QualitySettings.SetQualityLevel(0, true);
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualitySettings"), true);
        }
    }

    public void ResolutionOnAwake()
    {
       // Screen.SetResolution(menuGUI.xResolution, menuGUI.yResolution, true);

     //   Camera.main.aspect = 18f / 9f;
    }

    public void ClickExitButton()
    {
        Application.Quit();
    }

    public void EnoughMoneyButtonOk()
    {
        menuPanels.EnoughMoney.SetActive(false);
        GameObject snd = GameObject.Find("Close");
        snd.GetComponent<AudioSource>().Play();
    }

    public void Shop()
    {
        menuPanels.EnoughMoney.SetActive(true);
        Amplitude.Instance.logEvent("Shop");
        GameObject snd = GameObject.Find("Open");
        snd.GetComponent<AudioSource>().Play();

    }

    #region Load Car Upgrade

    public void LoadWheelSusspension()
    {
        menuGUI.SusspFrontTx.text = (PlayerPrefs.GetFloat("WheelSusspFront", menuGUI.frontS.value)).ToString();
        menuGUI.SusspRearTx.text = (PlayerPrefs.GetFloat("WheelSusspRear", menuGUI.RearS.value)).ToString();
    }

    public void LoadUpgradeBrake()
    {
        tBrake = PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString());
        menuGUI.BrakeUpLevel.text = tBrake.ToString() + "/" + carSetting[currentCarNumber].carPower.maxUpgradeLevelBrake.ToString();
        if (tBrake < carSetting[currentCarNumber].carPower.maxUpgradeLevelBrake)
        {
            menuGUI.BrakePrice.text = carSetting[currentCarNumber].carPower.brake[tBrake] + "$";
        }
        else
            menuGUI.BrakePrice.text = "MAX";
    }

    public void LoadUpgradeHandling()
    {
        tHandling = PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString());
        menuGUI.HandlingUpLevel.text = tHandling.ToString() + "/" + carSetting[currentCarNumber].carPower.maxUpgradeLevelHandling.ToString();
        if (tHandling < carSetting[currentCarNumber].carPower.maxUpgradeLevelHandling)
        {
            menuGUI.HandlingPrice.text = carSetting[currentCarNumber].carPower.handling[tHandling] + "$";
        }
        else
            menuGUI.HandlingPrice.text = "MAX";
        //menuGUI.HandlingBtn.interactable = false;
    }

    public void LoadUpgradeOnAwake()
    {
        if (PlayerPrefs.GetInt("Nitro" + currentCarNumber.ToString()) == 1)
        {

            menuGUI.carUseNos.interactable = true;
            menuGUI.lockedNos.SetActive(false);
            menuGUI.NitroPrice.gameObject.SetActive(false);
            menuGUI.Nitro.interactable = false;
        }
        else
        {
            menuGUI.carUseNos.interactable = false;
            menuGUI.Nitro.interactable = true;
            menuGUI.lockedNos.SetActive(true);
            menuGUI.NitroPrice.gameObject.SetActive(true);


        }

        if (PlayerPrefs.GetInt("Turbo" + currentCarNumber.ToString()) == 1)
        {

            menuGUI.carUseTurbo.interactable = true;
            menuGUI.lockedTurbo.SetActive(false);
            menuGUI.TurboPrice.gameObject.SetActive(false);
            menuGUI.Turbo.interactable = false;
        }
        else
        {
            menuGUI.carUseTurbo.interactable = false;
            menuGUI.Turbo.interactable = true;
            menuGUI.lockedTurbo.SetActive(true);
            menuGUI.TurboPrice.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("ABS" + currentCarNumber.ToString()) == 1)
        {

            menuGUI.carUseABS.interactable = true;
            menuGUI.lockedABS.SetActive(false);
            menuGUI.ABSPrice.gameObject.SetActive(false);
            menuGUI.ABS.interactable = false;
        }
        else
        {
            menuGUI.carUseABS.interactable = false;
            menuGUI.ABS.interactable = true;
            menuGUI.lockedABS.SetActive(true);
            menuGUI.ABSPrice.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 1)
        {
            menuGUI.lockedWheelSusspF.SetActive(false);
            menuGUI.lockedWheelSusspR.SetActive(false);
            menuGUI.frontS.interactable = true;
            menuGUI.RearS.interactable = true;
            menuGUI.WheelSusspPriceF.gameObject.SetActive(false);
            menuGUI.WheelSusspPriceR.gameObject.SetActive(false);

        }
        else
        {
            menuGUI.lockedWheelSusspF.SetActive(true);
            menuGUI.lockedWheelSusspR.SetActive(true);
            menuGUI.frontS.interactable = false;
            menuGUI.RearS.interactable = false;
            menuGUI.WheelSusspPriceF.gameObject.SetActive(true);
            menuGUI.WheelSusspPriceR.gameObject.SetActive(true);
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;
        }

        if (PlayerPrefs.GetInt("TCS" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 1)
        {
            menuGUI.lockedTCS.SetActive(false);
            menuGUI.TCSPrice.gameObject.SetActive(false);
            menuGUI.TCStgl.interactable = true;
            if (TCSisChecked)
            {
                menuGUI.TCS.interactable = true;
            }
        }
        else
        {
            menuGUI.lockedTCS.SetActive(true);
            menuGUI.TCSPrice.gameObject.SetActive(true);
            menuGUI.TCS.interactable = false;
            menuGUI.TCStgl.interactable = false;
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCSThreshold = 0.25f;
        }

        if (PlayerPrefs.GetInt("ESP" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 1)
        {
            menuGUI.lockedESP.SetActive(false);
            menuGUI.ESPPrice.gameObject.SetActive(false);
            menuGUI.ESPtgl.interactable = true;
            if (ESPisChecked)
            {
                menuGUI.ESP.interactable = true;
            }
            
        }
        else
        {
            menuGUI.lockedESP.SetActive(true);
            menuGUI.ESPPrice.gameObject.SetActive(true);
            menuGUI.ESP.interactable = false;
            menuGUI.ESPtgl.interactable = false;
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESPThreshold = 0.25f;
            PlayerPrefs.SetInt("selectESP" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
        }

        if (PlayerPrefs.GetInt("Traction"+PlayerPrefs.GetInt("CurrentCar").ToString())== 1)
        {
            menuGUI.lockedTraction.SetActive(false);
            menuGUI.TractionPrice.gameObject.SetActive(false);
            menuGUI.TractionTgl.interactable = true;
            //TractionIsChecked = true;
            //menuGUI.Traction.interactable = true;
            if (TractionIsChecked)
            {
                menuGUI.Traction.interactable = true;
            }
        }
        else
        {
            menuGUI.lockedTraction.SetActive(true);
            menuGUI.TractionPrice.gameObject.SetActive(true);
            menuGUI.TractionTgl.interactable = false;
            //TractionIsChecked = false;
            menuGUI.Traction.interactable = false;
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().tractionHelperStrength = 0.1f;
            PlayerPrefs.SetInt("selectTraction" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
        }

        if (PlayerPrefs.GetInt("WD" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 1)
        {
            menuGUI.lockedWheelDrive.SetActive(false);
            menuGUI.WheelDrivePrice.gameObject.SetActive(false);
            menuGUI.RWD.interactable = true;
            menuGUI.FWD.interactable = true;
            menuGUI.AWD.interactable = true;
            WheelDriveisChecked = true;
        }
        else
        {
            menuGUI.lockedWheelDrive.SetActive(true);
            menuGUI.WheelDrivePrice.gameObject.SetActive(true);
            menuGUI.RWD.interactable = false;
            menuGUI.FWD.interactable = false;
            menuGUI.AWD.interactable = false;
            WheelDriveisChecked = false;
        }
    }

    public void LoadEngineUpgradeOnSelectedCar()
    {
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 0)
        {
            svChecked.engine0 = true;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque = carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque;

        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 1)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = true;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin1 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 15;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 250f;
                svChecked.engin1 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 2)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = true;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin2 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 30;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 350f;
                svChecked.engin2 = false;
            }

        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 3)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = true;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin3 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 50;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 500f;
                svChecked.engin3 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 4)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = true;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin4 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 70;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 600f;
                svChecked.engin4 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 5)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = true;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin5 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 80;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 700f;
                svChecked.engin5 = false;
            }
        }

        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 6)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = true;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin6 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 90;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 800f;
                svChecked.engin6 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 7)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = true;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin7 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 100;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 900f;
                svChecked.engin7 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 8)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = true;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin8 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 110;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1200f;
                svChecked.engin8 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 9)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = true;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin9 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 115;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1300f;
                svChecked.engin9 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 10)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = true;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin10 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 125;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1400f;
                svChecked.engin10 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 11)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = true;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin11 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 135;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1500f;
                svChecked.engin11 = false;
            }
        }

        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 12)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = true;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin12 == true)
            {

                carSetting[currentCarNumber].carPower.speed += 145;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1600f;
                svChecked.engin12 = false;

            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 13)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = true;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (svChecked.engin13 == true)
            {

                carSetting[currentCarNumber].carPower.speed += 155;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1700f;
                svChecked.engin13 = false;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 14)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = true;
            svChecked.engine15 = false;
            if (svChecked.engin14 == true)
            {

                carSetting[currentCarNumber].carPower.speed += 175;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1850f;
                svChecked.engine14 = false;

            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 15)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = true;
            if (svChecked.engin15 == true)
            {
                carSetting[currentCarNumber].carPower.speed += 195;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 2150f;
                svChecked.engin15 = false;
            }
        }
    }

    public void LoadEngineUprgadeOnAwake()
    {
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 0)
        {
            svChecked.engine0 = true;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;

            //carSetting[currentCarNumber].car.GetComponent<RCC_CarControllerV3>().engineTorque = carSetting[currentCarNumber].car.GetComponent<RCC_CarControllerV3>().engineTorque;

        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 1)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = true;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].carPower.speed += 15;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 250f;
            }

        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 2)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = true;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].carPower.speed += 30;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 350f;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 3)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = true;
            svChecked.engine4 = false;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].carPower.speed += 50;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 500f;
            }
        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 4)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = true;
            svChecked.engine5 = false;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].carPower.speed += 70;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 600f;
            }

        }
        if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 5)
        {
            svChecked.engine0 = false;
            svChecked.engine1 = false;
            svChecked.engine2 = false;
            svChecked.engine3 = false;
            svChecked.engine4 = false;
            svChecked.engine5 = true;
            svChecked.engine6 = false;
            svChecked.engine7 = false;
            svChecked.engine8 = false;
            svChecked.engine9 = false;
            svChecked.engine10 = false;
            svChecked.engine11 = false;
            svChecked.engine12 = false;
            svChecked.engine13 = false;
            svChecked.engine14 = false;
            svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].carPower.speed += 80;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 700f;
            }

            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 6)
            {
                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = true;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
                if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
                { 
                    carSetting[currentCarNumber].carPower.speed += 90;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 800f;
                }
            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 7)
            {


                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = true;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
                {
                    carSetting[currentCarNumber].carPower.speed += 100;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 900f;
                }
            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 8)
            {


                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = true;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
                {
                    carSetting[currentCarNumber].carPower.speed += 110;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1200f;
                }
            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 9)
            {

                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = true;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
                {
                    carSetting[currentCarNumber].carPower.speed += 115;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1300f;
                }
            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 10)
            {
                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = true;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                    carSetting[currentCarNumber].carPower.speed += 125;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1400f;
                }
            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 11)
            {
                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = true;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                    carSetting[currentCarNumber].carPower.speed += 135;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1500f;
                }

            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 12)
            {
                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = true;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                    carSetting[currentCarNumber].carPower.speed += 145;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1600f;
                }

            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 13)
            {
                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = true;
                svChecked.engine14 = false;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {

                    carSetting[currentCarNumber].carPower.speed += 155;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1700f;
                }

            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 14)
            {
                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = true;
                svChecked.engine15 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {

                    carSetting[currentCarNumber].carPower.speed += 175;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1850f;
                }

            }
            if (PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString()) == 15)
            {
                svChecked.engine0 = false;
                svChecked.engine1 = false;
                svChecked.engine2 = false;
                svChecked.engine3 = false;
                svChecked.engine4 = false;
                svChecked.engine5 = false;
                svChecked.engine6 = false;
                svChecked.engine7 = false;
                svChecked.engine8 = false;
                svChecked.engine9 = false;
                svChecked.engine10 = false;
                svChecked.engine11 = false;
                svChecked.engine12 = false;
                svChecked.engine13 = false;
                svChecked.engine14 = false;
                svChecked.engine15 = true;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {

                    carSetting[currentCarNumber].carPower.speed += 195;
                    carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 2150f;
                }
            }
        }
    }

    public void LoadHandlingOnSelectedCar()
    {
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 0)
        {
            svChecked.handling0 = true;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;

        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 1)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = true;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl1 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
                svChecked.handl1 = false;
            }


        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 2)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = true;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl2 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.1f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.1f;
                svChecked.handl2 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 3)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = true;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl3 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.15f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.15f;
                svChecked.handl3 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 4)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = true;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl4 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.2f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.2f;
                svChecked.handl4 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 5)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = true;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl5 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.25f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.25f;
                svChecked.handl5 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 6)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = true;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl6 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.28f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.28f;
                svChecked.handl6 = false;
            }
        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 7)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = true;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl7 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.35f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.35f;
                svChecked.handl7 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 8)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = true;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl8 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.45f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.45f;
                svChecked.handl8 = false;
            }
        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 9)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = true;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl9 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.6f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.6f;
                svChecked.handl9 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 10)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = true;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (svChecked.handl10 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.7f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.7f;
                svChecked.handl10 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 11)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = true;
            svChecked.handling12 = false;
            if (svChecked.handl11 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.8f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.8f;
                svChecked.handl11 = false;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 12)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = true;
            if (svChecked.handl12 == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.9f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.9f;
                svChecked.handl12 = false;
            }
        }
    }

    public void LoadHandlingOnAwake()
    {
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 0)
        {
            svChecked.handling0 = true;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;

        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 1)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = true;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 2)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = true;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.1f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.1f;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 3)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = true;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.15f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.15f;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 4)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = true;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.2f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.2f;
            }
        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 5)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = true;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.25f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.25f;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 6)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = true;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.28f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.28f;
            }
        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 7)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = true;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.35f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.35f;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 8)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = true;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.45f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.45f;
            }
        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 9)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = true;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.6f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.6f;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 10)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = true;
            svChecked.handling11 = false;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.7f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.7f;
            }
        }

        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 11)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = true;
            svChecked.handling12 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.8f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.8f;
            }
        }
        if (PlayerPrefs.GetInt("Handling" + currentCarNumber.ToString()) == 12)
        {
            svChecked.handling0 = false;
            svChecked.handling1 = false;
            svChecked.handling2 = false;
            svChecked.handling3 = false;
            svChecked.handling4 = false;
            svChecked.handling5 = false;
            svChecked.handling6 = false;
            svChecked.handling7 = false;
            svChecked.handling8 = false;
            svChecked.handling9 = false;
            svChecked.handling10 = false;
            svChecked.handling11 = false;
            svChecked.handling12 = true;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.9f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.9f;
            }
        }
    }

    public void LoadBrakeOnAwake()
    {
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 0)
        {
            svChecked.brake0 = true;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;

        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 1)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = true;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.025f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 100f;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 2)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = true;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.05f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 200f;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 3)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = true;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.075f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 300f;

            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 4)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = true;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.125f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 400f;
            }
        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 5)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = true;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.2f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 500f;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 6)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = true;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.225f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 600f;
            }
        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 7)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = true;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.3f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 700f;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 8)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = true;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.35f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 800f;
            }
        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 9)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = true;
            svChecked.brake10 = false;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.4f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 900f;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 10)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = true;
            if (PlayerPrefs.GetInt("CurrentCar") != 0 && PlayerPrefs.GetInt("CurrentCar") != 1 && PlayerPrefs.GetInt("CurrentCar") != 2 && PlayerPrefs.GetInt("CurrentCar") != 3 && PlayerPrefs.GetInt("CurrentCar") != 4 && PlayerPrefs.GetInt("CurrentCar") != 5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.45f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 1000f;
            }
        }
    }

    public void LoadBrakeOnSelectedCar()
    {
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 0)
        {
            svChecked.brake0 = true;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 1)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = true;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak1)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.025f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 100f;
                svChecked.brak1 = false;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 2)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = true;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak2)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.05f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 200f;
                svChecked.brak2 = false;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 3)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = true;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak3)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.075f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 300f;
                svChecked.brak3 = false;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 4)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = true;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak4)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.125f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 400f;
                svChecked.brak4 = false;
            }
        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 5)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = true;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak5)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.2f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 500f;
                svChecked.brak5 = false;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 6)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = true;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak6)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.225f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 600f;
                svChecked.brak6 = false;
            }
        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 7)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = true;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak7)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.3f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 700f;
                svChecked.brak7 = false;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 8)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = true;
            svChecked.brake9 = false;
            svChecked.brake10 = false;
            if (svChecked.brak8)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.35f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 800f;
                svChecked.brak8 = false;
            }
        }

        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 9)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = true;
            svChecked.brake10 = false;
            if (svChecked.brak9)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.4f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 900f;
                svChecked.brak9 = false;
            }
        }
        if (PlayerPrefs.GetInt("Brake" + currentCarNumber.ToString()) == 10)
        {
            svChecked.brake0 = false;
            svChecked.brake1 = false;
            svChecked.brake2 = false;
            svChecked.brake3 = false;
            svChecked.brake4 = false;
            svChecked.brake5 = false;
            svChecked.brake6 = false;
            svChecked.brake7 = false;
            svChecked.brake8 = false;
            svChecked.brake9 = false;
            svChecked.brake10 = true;
            if (svChecked.brak10)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.45f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 1000f;
                svChecked.brak10 = false;
            }
        }
    }

    public void LoadUpgrade()
    {

        tEngine = PlayerPrefs.GetInt("Engine" + currentCarNumber.ToString());
        menuGUI.engineUpLevel.text = tEngine.ToString() + "/" + carSetting[currentCarNumber].carPower.maxUpgradeLevel.ToString();
        if (tEngine < carSetting[currentCarNumber].carPower.maxUpgradeLevel)
        {
            menuGUI.EnginePrice.text = carSetting[currentCarNumber].carPower.engine[tEngine].ToString() + "$";

        }
        else
            menuGUI.EnginePrice.text = "MAX";

    }

    #endregion

    #endregion

    #region CarSelected
    public void NextCar()
    {
        currentCarNumber++;
        currentCarNumber = (int)Mathf.Repeat(currentCarNumber, carSetting.Length);

        foreach (CarSetting VSetting in carSetting)
        {

            if (VSetting == carSetting[currentCarNumber])
            {
                
                VSetting.car.SetActive(true);
                currentCar = VSetting;
                PlayerPrefs.SetInt("CurrentCar", currentCarNumber);
                print(PlayerPrefs.GetInt("CurrentCar"));
                energyTx.text = "Energy: " + (carSetting[currentCarNumber].energy + PlayerPrefs.GetInt("Energy"));

                if (PlayerPrefs.GetInt("CurrentCar") == 0)
                {
                    Amplitude.Instance.logEvent("Hevy666");
                //    _car_mirrorActive = GameObject.FindGameObjectWithTag("CarMirror");
               //     _car_mirrorActive.SetActive(false);
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 4)
                {
                    Amplitude.Instance.logEvent("Rasta 1970");
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 5)
                {
                    Amplitude.Instance.logEvent("Cuda Killer");
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 8)
                {
                    Amplitude.Instance.logEvent("The Bison Monster");
                }

                /// AssetBundle Cars
                /// 
                if (PlayerPrefs.GetInt("CurrentCar") == 1)
                {
                    Amplitude.Instance.logEvent("HotRodd 1937");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;

                }
                if (PlayerPrefs.GetInt("CurrentCar") == 2)
                {
                    Amplitude.Instance.logEvent("Buggy GTR");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 3)
                {
                    Amplitude.Instance.logEvent("GT 350");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 6)
                {
                    Amplitude.Instance.logEvent("BMW i8");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;

                }

                if (PlayerPrefs.GetInt("CurrentCar") == 7)
                {
                    Amplitude.Instance.logEvent("Lambo LP-750");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;


                }
                if (PlayerPrefs.GetInt("CurrentCar") == 9)
                {
                    Amplitude.Instance.logEvent("Tesla T");


                }

                if (PlayerPrefs.GetInt("CurrentCar") == 10)
                {
                    Amplitude.Instance.logEvent("TownCar");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;


                }

                if (PlayerPrefs.GetInt("CurrentCar") == 11)
                {
                    Amplitude.Instance.logEvent("Ikarus280");

                }

                if (PlayerPrefs.GetInt("CurrentCar") == 12)
                {
                    Amplitude.Instance.logEvent("ChevyGodlenWings");

                }

                if (PlayerPrefs.GetInt("CurrentCar") == 13)
                {
                    Amplitude.Instance.logEvent("RollsRoyce");
                }

                // if (PlayerPrefs.GetInt("CurrentCar") == 14)
                // {
                //     Amplitude.Instance.logEvent("Vetty");
                // }
            }
            else
            {
                VSetting.car.SetActive(false);

            }
        }
    }

    public void PrevCar()
    {
        currentCarNumber--;
        currentCarNumber = (int)Mathf.Repeat(currentCarNumber, carSetting.Length);

        foreach (CarSetting VSetting in carSetting)
        {
            if (VSetting == carSetting[currentCarNumber])
            {

                VSetting.car.SetActive(true);
                currentCar = VSetting;
                PlayerPrefs.SetInt("CurrentCar", currentCarNumber);
                energyTx.text = "Energy: " + (carSetting[currentCarNumber].energy + PlayerPrefs.GetInt("Energy"));
                if (PlayerPrefs.GetInt("CurrentCar")==0)
                {
                    Amplitude.Instance.logEvent("Hevy666");
                   // _car_mirrorActive = GameObject.FindGameObjectWithTag("CarMirror");
                   // _car_mirrorActive.SetActive(false);
                }
                
                if (PlayerPrefs.GetInt("CurrentCar") == 4)
                {
                    Amplitude.Instance.logEvent("Rasta 1970");
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 5)
                {
                    Amplitude.Instance.logEvent("Cuda Killer");
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 8)
                {
                    Amplitude.Instance.logEvent("The Bison Monster");
                }


                /// AssetBundle Cars
                /// 
                if (PlayerPrefs.GetInt("CurrentCar") == 1)
                {
                    Amplitude.Instance.logEvent("HotRodd 1937");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 2)
                {
                    Amplitude.Instance.logEvent("Buggy GTR");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 3)
                {
                    Amplitude.Instance.logEvent("GT 350");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 6)
                {
                    Amplitude.Instance.logEvent("BMW i8");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    // CathingLoadFiles.manage.i8_ab.GetComponentInChildren<RCC_CarControllerV3>().StartEngineNow();
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 7)
                {
                    Amplitude.Instance.logEvent("Lambo LP-750");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    // CathingLoadFiles.manage.lambo_ab.GetComponentInChildren<RCC_CarControllerV3>().StartEngineNow();
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 9)
                {
                    Amplitude.Instance.logEvent("Tesla T");
                }
                
                if (PlayerPrefs.GetInt("CurrentCar") == 10)
                {
                    Amplitude.Instance.logEvent("TownCar");
                    carSetting[currentCarNumber].car.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    //carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().StartEngineNow();
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 11)
                {
                    Amplitude.Instance.logEvent("Ikarus280");

                }

                if (PlayerPrefs.GetInt("CurrentCar") == 12)
                {
                    Amplitude.Instance.logEvent("ChevyGodlenWings");
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 13)
                {
                    Amplitude.Instance.logEvent("RollsRoyce");
                }

               // if (PlayerPrefs.GetInt("CurrentCar") == 14)
               // {
               //     Amplitude.Instance.logEvent("Vetty");
               // }
            }
            else
            {

                VSetting.car.SetActive(false);


            }
        }
    }

    public void CarUpdate()
    {
        //maxSpeedActive.SetActive(true);
        if (PlayerPrefs.GetInt("Nitro" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("select" + currentCarNumber.ToString()) == 1)
        {

            menuGUI.carUseNos.isOn = true;
            NOSisChecked = true;
            if (svChecked.isCheckedNOS == true)
            {
                carSetting[currentCarNumber].carPower.speed += 35;
                svChecked.isCheckedNOS = false;
            }

        }
        else
        {
            menuGUI.carUseNos.isOn = false;
            NOSisChecked = false;
        }
        if (PlayerPrefs.GetInt("Turbo" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectTurbo" + currentCarNumber.ToString()) == 1)
        {
            menuGUI.carUseTurbo.isOn = true;
            TurboisChecked = true;
            if (svChecked.isCheckedTurbo == true)
            {
                carSetting[currentCarNumber].carPower.speed += 15;
                svChecked.isCheckedTurbo = false;
            }


        }
        else
        {
            menuGUI.carUseTurbo.isOn = false;
            TurboisChecked = false;
        }

        if (PlayerPrefs.GetInt("ABS" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectABS" + currentCarNumber.ToString()) == 1)
        {
            menuGUI.carUseABS.isOn = true;
            ABSisChecked = true;
            if (svChecked._ABSisChecked_ == true)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.06f;
                svChecked._ABSisChecked_ = false;
            }


        }
        else
        {
            menuGUI.carUseABS.isOn = false;
            ABSisChecked = false;
        }

        if (PlayerPrefs.GetInt("TCS" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectTCS" + currentCarNumber.ToString()) == 1)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCS = true;
            menuGUI.TCStgl.isOn = true;
            menuGUI.TCS.interactable = true;

        }
        else
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCS = false;
            menuGUI.TCStgl.isOn = false;
            menuGUI.TCStgl.interactable = false;
            menuGUI.TCS.interactable = false;
        }

        if (PlayerPrefs.GetInt("ESP" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectESP" + currentCarNumber.ToString()) == 1)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESP = true;
            menuGUI.ESPtgl.isOn = true;
            menuGUI.ESP.interactable = true;

        }
        else
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESP = false;
            menuGUI.ESPtgl.isOn = false;
            menuGUI.ESPtgl.interactable = false;
            menuGUI.ESP.interactable = false;
        }

        if (PlayerPrefs.GetInt("Traction"+ currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectTraction" + currentCarNumber.ToString()) == 1)
        {
            menuGUI.TractionTgl.isOn = true;
            menuGUI.Traction.interactable = true;
        }
        else
        {
            menuGUI.TractionTgl.isOn = false;
            menuGUI.TractionTgl.interactable = false;
            menuGUI.Traction.interactable = false;
        }
    }

    public void BuyCar()
    {
        menuGUI.RUSure.SetActive(true);
        GameObject snd = GameObject.Find("Open");
        snd.GetComponent<AudioSource>().Play();
        Amplitude.Instance.logEvent("BuyCar");
    }

    public void BuyCarNo()
    {
        menuGUI.RUSure.SetActive(false);
        GameObject snd = GameObject.Find("Close");
        snd.GetComponent<AudioSource>().Play();
        Amplitude.Instance.logEvent("BuyCarNo");
    }

    public void BuyCarYes()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].price)
        {
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].price);
            carSetting[currentCarNumber].Bought = true;
            PlayerPrefs.SetInt("BoughtCar"+currentCarNumber, 1);
            GameObject snd = GameObject.Find("Purchased");
            snd.GetComponent<AudioSource>().Play();
            menuGUI.RUSure.SetActive(false);
            Amplitude.Instance.logEvent("BuyCarYes");
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
        }
    }

    #endregion

    #region CarCustomize

    public void NitroDisable(Toggle toggle)
    {
        if (toggle.isOn)
        {
            carSetting[currentCarNumber].carPower.speed += 35;
            NOSisChecked = true;
            PlayerPrefs.SetInt("select" + currentCarNumber.ToString(), 1);
            Amplitude.Instance.logEvent("NitroON");
        }
        else
        {
            carSetting[currentCarNumber].carPower.speed -= 35;
            NOSisChecked = false;
            PlayerPrefs.SetInt("select" + currentCarNumber.ToString(), 0);
            Amplitude.Instance.logEvent("NitroOFF");
        }
    }

    public void TurboDisable(Toggle toggle)
    {
        if (toggle.isOn)
        {

            carSetting[currentCarNumber].carPower.speed += 10;
            TurboisChecked = true;
            PlayerPrefs.SetInt("selectTurbo" + currentCarNumber.ToString(), 1);
            Amplitude.Instance.logEvent("TurboON");

        }
        else
        {

            carSetting[currentCarNumber].carPower.speed -= 10; ;
            TurboisChecked = false;
            PlayerPrefs.SetInt("selectTurbo" + currentCarNumber.ToString(), 0);
            Amplitude.Instance.logEvent("TurboOFF");

        }
    }

    public void ABSDisable(Toggle toggle)
    {
        if (toggle.isOn)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.06f;
            PlayerPrefs.SetInt("selectABS" + currentCarNumber.ToString(), 1);
            ABSisChecked = true;
            Amplitude.Instance.logEvent("AbsON");
        }
        else
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold -= 0.06f;
            PlayerPrefs.SetInt("selectABS" + currentCarNumber.ToString(), 0);
            ABSisChecked = false;
            Amplitude.Instance.logEvent("AbsOFF");
        }
    }

    public void DisableTCS(Toggle toggle)
    {
        if (toggle.isOn)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCS = true;
            TCSisChecked = true;
            menuGUI.TCS.interactable = true;
            PlayerPrefs.SetInt("selectTCS" + currentCarNumber.ToString(), 1);
        }
        else
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCS = false;
            TCSisChecked = false;
            menuGUI.TCS.interactable = false;
            PlayerPrefs.SetInt("selectTCS" + currentCarNumber.ToString(), 0);
        }
    }

    public void DisableESP(Toggle toggle)
    {
        if (toggle.isOn)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESP = true;
            ESPisChecked = true;
            menuGUI.ESP.interactable = true;
            PlayerPrefs.SetInt("selectESP" + currentCarNumber.ToString(), 1);
        }
        else
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESP = false;
            ESPisChecked = false;
            menuGUI.ESP.interactable = false;
            PlayerPrefs.SetInt("selectESP" + currentCarNumber.ToString(), 0);
        }
    }

    public void DisableTraction(Toggle toggle)
    {
        if (toggle.isOn)
        {
            TractionIsChecked = true;
            menuGUI.Traction.interactable = true;
            PlayerPrefs.SetInt("selectTraction" + currentCarNumber.ToString(), 1);
        }
        else
        {
            TractionIsChecked = false;
            menuGUI.Traction.interactable = false;
            PlayerPrefs.SetInt("selectTraction" + currentCarNumber.ToString(), 0);
        }
    }

    public void buyNitro()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].nitroPrice)
        {
            PlayerPrefs.SetInt("Nitro" + currentCarNumber.ToString(), 1);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].nitroPrice);
            menuGUI.lockedNos.SetActive(false);
            menuGUI.NitroPrice.gameObject.SetActive(false);
            menuGUI.Nitro.interactable = false;
            menuGUI.carUseNos.interactable = true;
            menuGUI.carUseNos.isOn = true;
            NOSisChecked = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("NitroBUY");
            UniAdManager.manage.ShowInterstatial();
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            PlayerPrefs.SetInt("Nitro" + currentCarNumber.ToString(), 0);
            menuGUI.lockedNos.SetActive(true);
            menuGUI.Nitro.interactable = true;
            menuGUI.carUseNos.interactable = false;
            menuGUI.carUseNos.isOn = false;
            audioUi.Denied.Play();
            Amplitude.Instance.logEvent("NitroEnoughMoney");
        }
    }

    public void buyTurbo()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].turboPrice)
        {
            PlayerPrefs.SetInt("Turbo" + currentCarNumber.ToString(), 1);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].turboPrice);
            menuGUI.lockedTurbo.SetActive(false);
            menuGUI.TurboPrice.gameObject.SetActive(false);
            menuGUI.Turbo.interactable = false;
            menuGUI.carUseTurbo.interactable = true;
            menuGUI.carUseTurbo.isOn = true;
            TurboisChecked = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("TurboBUY");
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
            PlayerPrefs.SetInt("Turbo" + currentCarNumber.ToString(), 0);
            menuGUI.lockedTurbo.SetActive(true);
            menuGUI.Turbo.interactable = true;
            menuGUI.carUseTurbo.interactable = false;
            audioUi.Denied.Play();
            Amplitude.Instance.logEvent("TurboEnoughMoney");
        }
    }

    public void buyABS()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber]._ABSPrice)
        {
            PlayerPrefs.SetInt("ABS" + currentCarNumber.ToString(), 1);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber]._ABSPrice);
            menuGUI.lockedABS.SetActive(false);
            menuGUI.ABSPrice.gameObject.SetActive(false);
            menuGUI.ABS.interactable = false;
            menuGUI.carUseABS.interactable = true;
            menuGUI.carUseABS.isOn = true;
            ABSisChecked = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("AbsBUY");

        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
            PlayerPrefs.SetInt("ABS" + currentCarNumber.ToString(), 0);
            menuGUI.lockedABS.SetActive(true);
            menuGUI.ABS.interactable = true;
            menuGUI.carUseABS.interactable = false;
            audioUi.Denied.Play();
            Amplitude.Instance.logEvent("AbsEnoughMoney");
        }
    }

    public void buySusspensWheel()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].WheelSusspensionPrice[0])
        {
            PlayerPrefs.SetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar").ToString(), 1);
            PlayerPrefs.SetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar"), 0.2f);
            PlayerPrefs.SetFloat("WheelSusspRear" + PlayerPrefs.GetInt("CurrentCar"), 0.2f);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].WheelSusspensionPrice[0]);
            menuGUI.lockedWheelSusspF.SetActive(false);
            menuGUI.lockedWheelSusspR.SetActive(false);
            menuGUI.WheelSusspPriceF.gameObject.SetActive(false);
            menuGUI.WheelSusspPriceR.gameObject.SetActive(false);
            menuGUI.frontS.interactable = true;
            menuGUI.RearS.interactable = true;
            WheelSusspIsChecked = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("WheelSusspBUY");
            UniAdManager.manage.ShowInterstatial();
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
            PlayerPrefs.SetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
            
            menuGUI.lockedWheelSusspF.SetActive(true);
            menuGUI.lockedWheelSusspR.SetActive(true);
            menuGUI.WheelSusspPriceF.gameObject.SetActive(true);
            menuGUI.WheelSusspPriceR.gameObject.SetActive(true);
            WheelSusspIsChecked = false;
            Amplitude.Instance.logEvent("WheelSusspEnoughMoney");
            if (PlayerPrefs.GetInt("CurrentCar") != 8)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            }
            else if (PlayerPrefs.GetInt("CurrentCar") == 8)
            {
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.022f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.022f;
            }

            audioUi.Denied.Play();
        }
    }

    public void buyTCS()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].TCSPrice[0])
        {
            PlayerPrefs.SetInt("TCS" + PlayerPrefs.GetInt("CurrentCar").ToString(), 1);
            PlayerPrefs.SetFloat("TCSsetup" + PlayerPrefs.GetInt("CurrentCar"), 0.25f);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].TCSPrice[0]);
            menuGUI.lockedTCS.SetActive(false);
            menuGUI.TCSPrice.gameObject.SetActive(false);
            TCSisChecked = true;
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCS = true;
            menuGUI.TCStgl.isOn = true;
            menuGUI.TCS.interactable = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("TcsBUY");
            UniAdManager.manage.ShowInterstatial();
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
            PlayerPrefs.SetInt("TCS" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
            menuGUI.lockedTCS.SetActive(true);
            menuGUI.TCSPrice.gameObject.SetActive(true);
            TCSisChecked = false;
            menuGUI.TCS.interactable = false;
            audioUi.Denied.Play();
            Amplitude.Instance.logEvent("TcsEnoughMoney");
        }
    }

    public void buyESP()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].ESPPrice[0])
        {
            PlayerPrefs.SetInt("ESP" + PlayerPrefs.GetInt("CurrentCar").ToString(), 1);
            PlayerPrefs.SetFloat("ESPsetup" + PlayerPrefs.GetInt("CurrentCar"), 0.25f);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].ESPPrice[0]);
            menuGUI.lockedESP.SetActive(false);
            menuGUI.ESPPrice.gameObject.SetActive(false);
            ESPisChecked = true;
            menuGUI.ESPtgl.isOn = true;
            carSetting[currentCarNumber].car.GetComponent<RCC_CarControllerV3>().ESP = true;
            //carSetting[currentCarNumber].car.GetComponent<RCC_CarControllerV3>().ESPThreshold = 0.2f;
            menuGUI.ESP.interactable = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("EspBUY");
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
            PlayerPrefs.SetInt("ESP" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
            PlayerPrefs.SetInt("selectESP" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
            menuGUI.lockedESP.SetActive(true);
            menuGUI.ESPPrice.gameObject.SetActive(true);
            ESPisChecked = false;
            menuGUI.ESP.interactable = false;
            audioUi.Denied.Play();
            Amplitude.Instance.logEvent("EspEnoughMoney");
        }
    }

    public void buyTraction()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].TractionPrice[0])
        {
            PlayerPrefs.SetInt("Traction" + PlayerPrefs.GetInt("CurrentCar").ToString(), 1);
            PlayerPrefs.SetFloat("TractionSetup" + PlayerPrefs.GetInt("CurrentCar"), 0.1f);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].TractionPrice[0]);
            menuGUI.lockedTraction.SetActive(false);
            menuGUI.TractionPrice.gameObject.SetActive(false);
            TractionIsChecked = true;
            menuGUI.TractionTgl.isOn = true;
            menuGUI.Traction.interactable = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("TractionBUY");
            UniAdManager.manage.ShowInterstatial();
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
            PlayerPrefs.SetInt("Traction" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
            menuGUI.lockedTraction.SetActive(true);
            menuGUI.TractionTgl.isOn = false;
            menuGUI.TractionPrice.gameObject.SetActive(true);
            TractionIsChecked = false;
            menuGUI.Traction.interactable = false;
            audioUi.Denied.Play();
            Amplitude.Instance.logEvent("TractionEnoughMoney");
        }
    }

    public void buyWheelDrive()
    {
        if (PlayerPrefs.GetFloat("DriftCoin") >= carSetting[currentCarNumber].WheelDrivePrice[0])
        {
            PlayerPrefs.SetInt("WD" + PlayerPrefs.GetInt("CurrentCar").ToString(), 1);
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].WheelDrivePrice[0]);
            menuGUI.lockedWheelDrive.SetActive(false);
            menuGUI.WheelDrivePrice.gameObject.SetActive(false);
            menuGUI.RWD.interactable = true;
            menuGUI.FWD.interactable = true;
            menuGUI.AWD.interactable = true;
            audioUi.audSource.Play();
            Amplitude.Instance.logEvent("wheelDriveBUY");
            UniAdManager.manage.ShowInterstatial();
        }
        else
        {
            menuPanels.EnoughMoney.SetActive(true);
            Amplitude.Instance.logEvent("EnoughMoney");
            PlayerPrefs.SetInt("WD" + PlayerPrefs.GetInt("CurrentCar").ToString(), 0);
            menuGUI.lockedWheelDrive.SetActive(true);
            menuGUI.WheelDrivePrice.gameObject.SetActive(true);
            menuGUI.RWD.interactable = false;
            menuGUI.FWD.interactable = false;
            menuGUI.AWD.interactable = false;
            Amplitude.Instance.logEvent("wheelDriveEnoughMoney");
        }
    }

    public void EngineUpgrade()
    {
        if (tEngine < carSetting[currentCarNumber].carPower.maxUpgradeLevel)
        {
            
            if (PlayerPrefs.GetFloat("DriftCoin") > carSetting[currentCarNumber].carPower.engine[tEngine])
            {
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].carPower.engine[tEngine]);
                tEngine++;
                carSetting[currentCarNumber].carPower.speed += 15;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 150f;
                PlayerPrefs.SetInt("Engine" + currentCarNumber.ToString(), tEngine);
                menuGUI.engineUpLevel.text = carSetting[currentCarNumber].carPower.engine.ToString() + "/" + carSetting[currentCarNumber].carPower.maxUpgradeLevel.ToString();
                audioUi.audSource.Play();
                Amplitude.Instance.logEvent("EnigineUpgrade+1");
                if (tEngine < carSetting[currentCarNumber].carPower.maxUpgradeLevel)
                {
                    if (tEngine == 2 || tEngine == 4 || tEngine == 6 || tEngine == 8 || tEngine == 10 || tEngine == 12)
                    {
                        UniAdManager.manage.ShowInterstatial();
                    }
                    menuGUI.EnginePrice.text = carSetting[currentCarNumber].carPower.engine[tEngine].ToString() + "$";

                }
                else
                {
                    menuGUI.EnginePrice.text = "MAX";
                    Amplitude.Instance.logEvent("EnigineUpgradeMAX");
                }


            }
            else

                menuPanels.EnoughMoney.SetActive(true);
            //audioUi.Denied.Play();
        }
    }

    public void HandlingUpgrade()
    {
        if (tHandling < carSetting[currentCarNumber].carPower.maxUpgradeLevelHandling)
            if (PlayerPrefs.GetFloat("DriftCoin") > carSetting[currentCarNumber].carPower.handling[tHandling])
            {
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].carPower.handling[tHandling]);
                tHandling++;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
                PlayerPrefs.SetInt("Handling" + currentCarNumber.ToString(), tHandling);
                menuGUI.HandlingUpLevel.text = carSetting[currentCarNumber].carPower.handling.ToString() + "/" + carSetting[currentCarNumber].carPower.maxUpgradeLevelHandling.ToString();
                audioUi.audSource.Play();
                Amplitude.Instance.logEvent("HandlingUpgrade+1");

                if (tHandling < carSetting[currentCarNumber].carPower.maxUpgradeLevelHandling)
                {
                    menuGUI.HandlingPrice.text = carSetting[currentCarNumber].carPower.handling[tHandling] + "$";
                    if (tHandling == 3 || tHandling == 5 || tHandling == 8 || tHandling == 10 || tHandling == 12)
                    {
                        UniAdManager.manage.ShowInterstatial();
                    }
                }
                else
                {
                    menuGUI.HandlingPrice.text = "MAX";
                    Amplitude.Instance.logEvent("HandlingMAX");
                }

            }
            else
                
                menuPanels.EnoughMoney.SetActive(true);
               // audioUi.Denied.Play();
    }

    public void BrakeUpgrade()
    {
        if (tBrake < carSetting[currentCarNumber].carPower.maxUpgradeLevelBrake)
        {
            if (PlayerPrefs.GetFloat("DriftCoin") > carSetting[currentCarNumber].carPower.brake[tBrake])
            {
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") - carSetting[currentCarNumber].carPower.brake[tBrake]);
                tBrake++;
                carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.05f;
                PlayerPrefs.SetInt("Brake" + currentCarNumber.ToString(), tBrake);
                menuGUI.BrakeUpLevel.text = carSetting[currentCarNumber].carPower.brake.ToString() + "/" + carSetting[currentCarNumber].carPower.maxUpgradeLevelBrake.ToString();
                audioUi.audSource.Play();
                Amplitude.Instance.logEvent("BrakeUpgrade+1");
                if (tBrake < carSetting[currentCarNumber].carPower.maxUpgradeLevelBrake)
                {
                    menuGUI.BrakePrice.text = carSetting[currentCarNumber].carPower.brake[tBrake].ToString() + "$";
                    if (tBrake == 3 || tBrake == 5 || tBrake == 8 || tBrake == 10 || tBrake == 12)
                    {
                        UniAdManager.manage.ShowInterstatial();
                    }
                }
                else
                {
                    menuGUI.BrakePrice.text = "MAX";
                    Amplitude.Instance.logEvent("BrakeMAX");
                }

            }
            else
                
                menuPanels.EnoughMoney.SetActive(true);
               // audioUi.Denied.Play();
        }
    }

    public void CarSettingBuyNosTurboState()
    {
        if (PlayerPrefs.GetInt("Nitro" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("select" + currentCarNumber.ToString()) == 1)
        {
            carSetting[currentCarNumber].carPower.speed += 35;
            menuGUI.carUseNos.isOn = true;
            NOSisChecked = true;


        }
        else
        {
            menuGUI.carUseNos.isOn = false;
            NOSisChecked = false;

        }

        if (PlayerPrefs.GetInt("Turbo" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectTurbo" + currentCarNumber.ToString()) == 1)
        {
            carSetting[currentCarNumber].carPower.speed += 10;
            menuGUI.carUseTurbo.isOn = true;
            TurboisChecked = true;

        }
        else
        {
            menuGUI.carUseTurbo.isOn = false;
            TurboisChecked = false;

        }
        if (PlayerPrefs.GetInt("ABS" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectABS" + currentCarNumber.ToString()) == 1)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.06f;
            menuGUI.carUseABS.isOn = true;
            ABSisChecked = true;

        }
        else
        {
            menuGUI.carUseABS.isOn = false;
            ABSisChecked = false;
        }
        if(PlayerPrefs.GetInt("TCS"+currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectTCS" + currentCarNumber.ToString()) == 1)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCS = true;
            menuGUI.TCStgl.isOn = true;
            menuGUI.TCS.interactable = true;

        }
        else
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCS = false;
            menuGUI.TCStgl.isOn = false;
            menuGUI.TCStgl.interactable = false;
            menuGUI.TCS.interactable = false;
        }
        if (PlayerPrefs.GetInt("ESP" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectESP" + currentCarNumber.ToString()) == 1)
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESP = true;
            menuGUI.ESPtgl.isOn = true;
            menuGUI.ESP.interactable = true;

        }
        else
        {
            carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESP = false;
            menuGUI.ESPtgl.isOn = false;
            menuGUI.ESPtgl.interactable = false;
            menuGUI.ESP.interactable = false;
        }

        if (PlayerPrefs.GetInt("Traction" + currentCarNumber.ToString()) == 1 && PlayerPrefs.GetInt("selectTraction" + currentCarNumber.ToString()) == 1)
        {
            menuGUI.TractionTgl.isOn = true;
            menuGUI.Traction.interactable = true;
        }
        else
        {
            menuGUI.TractionTgl.isOn = false;
            menuGUI.TractionTgl.interactable = false;
            menuGUI.Traction.interactable = false;
        }


    }

    public void SetColor(int id)
    {
        if (svChecked.isCheckBody)
        {
            PlayerPrefs.SetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString(), id);
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[id];
            print(carSetting[currentCarNumber].Colors[id]);
        }
        if (svChecked.isCheckWheels)
        {
            PlayerPrefs.SetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString(), id);
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[id];
        }

        if (svChecked.isCheckDetail)
        {
            PlayerPrefs.SetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString(), id);
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[id];
        }

    }

    public void ActiveCurrentColor()
    {

        if(PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 0 && carSetting[currentCarNumber].body !=null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[0];

        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 1 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[1];
        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 2 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[2];
        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 3 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[3];
        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 4 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[4];
        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 5 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[5];
        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 6 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[6];
        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 7 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[7];
        }
        if (PlayerPrefs.GetInt("ColorBody" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 8 && carSetting[currentCarNumber].body != null)
        {
            carSetting[currentCarNumber].body.color = carSetting[currentCarNumber].Colors[8];
        }

        /////////

        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 0 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[0];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 1 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[1];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 2 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[2];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 3 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[3];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 4 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[4];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 5 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[5];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 6 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[6];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 7 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[7];
        }
        if (PlayerPrefs.GetInt("ColorWheel" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 8 && carSetting[currentCarNumber].wheel != null)
        {
            carSetting[currentCarNumber].wheel.color = carSetting[currentCarNumber].Colors[8];
        }

        ////////

        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 0 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[0];
        }
        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 1 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[1];
        }

        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 2 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[2];
        }
        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 3 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[3];
        }
        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 4 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[4];
        }
        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 5 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[5];
        }
        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 6 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[6];
        }
        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 7 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[7];
        }
        if (PlayerPrefs.GetInt("ColorDetail" + PlayerPrefs.GetInt("CurrentCar").ToString()) == 8 && carSetting[currentCarNumber].detail != null)
        {
            carSetting[currentCarNumber].detail.color = carSetting[currentCarNumber].Colors[8];
        }



        //if (menuGUI.bodyColor.gameObject.activeSelf)

        //carSetting[currentCarNumber].body.color = new Color(Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f), Random.Range(0.0f, 1.1f));

    }

    public void FrontWheelSusspession()
    {
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = menuGUI.frontS.value;
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = menuGUI.frontS.value;
        PlayerPrefs.SetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.frontS.value);

    }

    public void TCSsetup()
    {
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().TCSThreshold = menuGUI.TCS.value;
        PlayerPrefs.SetFloat("TCSsetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.TCS.value);
    }

    public void ESPsetup()
    {
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ESPThreshold = menuGUI.ESP.value;
        PlayerPrefs.SetFloat("ESPsetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.ESP.value);
    }

    public void TractionSetup()
    {
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().tractionHelperStrength = menuGUI.Traction.value;
        PlayerPrefs.SetFloat("TractionSetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.Traction.value);
    }

    public void RearWheelSusspession()
    {
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = menuGUI.RearS.value;
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = menuGUI.RearS.value;
        PlayerPrefs.SetFloat("WheelSusspRear"+ PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.RearS.value);

    }

    public void WheelSusspensionTxfront()
    {
        menuGUI.SusspFrontTx.text = PlayerPrefs.GetFloat("WheelSusspFront" + currentCarNumber.ToString(), menuGUI.frontS.value).ToString();
    }

    public void WheelSusspensionTxRear()
    {
        menuGUI.SusspRearTx.text = PlayerPrefs.GetFloat("WheelSusspRear" + currentCarNumber.ToString(), menuGUI.RearS.value).ToString();
    }

    public void TCSTx()
    {
        menuGUI.TCSTx.text = PlayerPrefs.GetFloat("TCSsetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.TCS.value).ToString();
    }

    public void ESPTx()
    {
        menuGUI.ESPTx.text = PlayerPrefs.GetFloat("ESPsetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.ESP.value).ToString();
    }

    public void TractionTx()
    {
        menuGUI.TractionTx.text = PlayerPrefs.GetFloat("TractionSetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.Traction.value).ToString();
    }

    public void EngineOnOff()
    {
        carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().KillOrStartEngine();
    }



    #endregion

    #region Level & UI
    public void loadlevel()
    {
        Amplitude.Instance.logEvent("CheckpointsLevel");
        isCheckpointLevel = true;
        SceneManager.LoadScene("level_lap6");
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
    }

    public void loadlevelFreeride()
    {
        Amplitude.Instance.logEvent("FreerideLevel");
        isFreerideActive = true;
        SceneManager.LoadScene("level_lap6");
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
    }

   public void loadlevelVsYou()
  {
        Amplitude.Instance.logEvent("BattleLevel");
        isAllvsYou = true;
        SceneManager.LoadScene("level_lap6");
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
       LoadBrakeOnSelectedCar();
    }

    public void levelTopSpeed()
    {
        Amplitude.Instance.logEvent("TopSpeedLevel");
        SceneManager.LoadScene("level_top_speed_test");
        isTopSpeedActive = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
    }

    public void levelQuickRace()
    {
        Amplitude.Instance.logEvent("QuickRaceLevel");
        SceneManager.LoadScene("level_top_speed_test");
        isQuickRace = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
    }

    public void level_city()
    {
        Amplitude.Instance.logEvent("level_City");
        isCity = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("city_single");

    }

    #region Arena Levels

    public void arena_1()
    {
        Amplitude.Instance.logEvent("arena#1");
        isArena1 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_1");

    }

    public void arena_2()
    {
        Amplitude.Instance.logEvent("arena#2");
        isArena = true;
        isArena2 = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_1");

    }

    public void arena_3()
    {
        Amplitude.Instance.logEvent("arena#3");
        isArena3 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_1");

    }

    public void arena_4()
    {
        Amplitude.Instance.logEvent("arena#4");
        isArena4 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_1");

    }

    public void arena_5()
    {
        Amplitude.Instance.logEvent("arena#5");
        isArena5 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_2");

    }

    public void arena_6()
    {
        Amplitude.Instance.logEvent("arena#6");
        isArena6 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_2");

    }

    public void arena_7()
    {
        Amplitude.Instance.logEvent("arena#7");
        isArena7 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_2");

    }

    public void arena_8()
    {
        Amplitude.Instance.logEvent("arena#8");
        isArena8 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_2");

    }

    public void arena_9()
    {
        Amplitude.Instance.logEvent("arena#9");
        isArena9 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_2");

    }

    public void arena_10()
    {
        Amplitude.Instance.logEvent("arena#10");
        isArena10 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_2");

    }

    public void arena_11()
    {
        Amplitude.Instance.logEvent("arena#11");
        isArena11 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_3");

    }

    public void arena_12()
    {
        Amplitude.Instance.logEvent("arena#12");
        isArena12 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_3");

    }

    public void arena_13()
    {
        Amplitude.Instance.logEvent("arena#13");
        isArena13 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_3");

    }

    public void arena_14()
    {
        Amplitude.Instance.logEvent("arena#14");
        isArena14 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_3");

    }

    public void arena_15()
    {
        Amplitude.Instance.logEvent("arena#15");
        isArena15 = true;
        isArena = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_3");

    }


    #endregion

    #region Checkpoint Levels
    public void checkpoint_2()
    {
        Amplitude.Instance.logEvent("checkpoint#2");
        isCheckpoint1 = true;
        isCheckpoint = true;
        LoadEngineUpgradeOnSelectedCar();
        LoadHandlingOnSelectedCar();
        LoadBrakeOnSelectedCar();
        SceneManager.LoadScene("_arena_4");

    }
    #endregion

    public void level_city_from_bundle()
    {
        if (PlayerPrefs.GetInt("citysingle") == 0)
        {
            PlayerPrefs.SetInt("citysingle", 1);
        }

        if (PlayerPrefs.GetInt("citysingle") == 1)
        {
            print("citysingle ==1");
            LoadAssetsBundle.manage.LoadCitySingle();
            Amplitude.Instance.logEvent("LoadAssetBundleCitySingle");
        }
        else
        {
            loadingPanel.SetActive(true);
            print("citysingle !=1");
            Amplitude.Instance.logEvent("level_City");
            isCity = true;
            SceneManager.LoadScene("city_single");
            LoadEngineUpgradeOnSelectedCar();
            LoadHandlingOnSelectedCar();
            LoadBrakeOnSelectedCar();
        }
    }

    public void levelBattleOnline()
    {
        Amplitude.Instance.logEvent("NetworkRoomHighway");
        menuPanels._NetworkRoom.SetActive(true);
        network_manager_active.SetActive(true);
    }


    public void AuthNetwork()
    {
        Amplitude.Instance.logEvent("AuthRoomHighwayEnter");
        menuPanels.Auth.SetActive(true);  
    }


    public void DeactiveNetwork()
    {
        network_manager_active.SetActive(false);
    }

    public void AuthPanel()
    {
        menuPanels.Auth.SetActive(false);
        Amplitude.Instance.logEvent("AuthRoomExit");
    }

    public void LeaderBoardOpen()
    {

        menuPanels.Leaderboard.SetActive(true);
        PlayFabLogin.manage.GetStats();
        PlayFabLogin.manage.SetStats();
        PlayFabLogin.manage.GetLeaderboard();
        PlayFabLogin.manage.GetLeaderboardCoinz();
        Amplitude.Instance.logEvent("LeaderboardOpen");
    }

    public void LeaderBoardClose()
    {
        PlayFabLogin.manage.CloseLeaderboardPanel();
        menuPanels.Leaderboard.SetActive(false);
        Amplitude.Instance.logEvent("LeaderboardClose");
    }


    public void RatingBoardOpen()
    {

        menuPanels.Ratingboard.SetActive(true);
        PlayFabLogin.manage.GetStats();
        PlayFabLogin.manage.SetStats();
        PlayFabLogin.manage.GetRatingboard();
        Amplitude.Instance.logEvent("RatingBoardOpen");
    }

    public void RatingBoardClose()
    {

        PlayFabLogin.manage.CloseRatingboardPanel();
        menuPanels.Ratingboard.SetActive(false);
        Amplitude.Instance.logEvent("RatingBoardClose");
    }

    public void ArenaOpen()
    {
        menuPanels.ArenaPanel.SetActive(true);
        Amplitude.Instance.logEvent("ArenaOpen");
        if (PlayerPrefs.GetInt("ArenaWin1") == 1)
        {
            levelSettingz.ArenaLv2.interactable = true;
            levelSettingz.lock1.SetActive(false);
        }
        if (PlayerPrefs.GetInt("ArenaWin2") == 1)
        {
            levelSettingz.ArenaLv3.interactable = true;
            levelSettingz.lock2.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin3") == 1)
        {
            levelSettingz.ArenaLv4.interactable = true;
            levelSettingz.lock3.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin4") == 1)
        {
            levelSettingz.ArenaLv5.interactable = true;
            levelSettingz.lock4.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin5") == 1)
        {
            levelSettingz.ArenaLv6.interactable = true;
            levelSettingz.lock5.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin6") == 1)
        {
            levelSettingz.ArenaLv7.interactable = true;
            levelSettingz.lock6.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin7") == 1)
        {
            levelSettingz.ArenaLv8.interactable = true;
            levelSettingz.lock7.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin8") == 1)
        {
            levelSettingz.ArenaLv9.interactable = true;
            levelSettingz.lock8.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin9") == 1)
        {
            levelSettingz.ArenaLv10.interactable = true;
            levelSettingz.lock9.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin10") == 1)
        {
            levelSettingz.ArenaLv11.interactable = true;
            levelSettingz.lock10.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin11") == 1)
        {
            levelSettingz.ArenaLv12.interactable = true;
            levelSettingz.lock11.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin12") == 1)
        {
            levelSettingz.ArenaLv13.interactable = true;
            levelSettingz.lock12.SetActive(false);
        }

        if (PlayerPrefs.GetInt("ArenaWin13") == 1)
        {
            levelSettingz.ArenaLv14.interactable = true;
            levelSettingz.lock13.SetActive(false);
        }
    }

    public void ArenaClose()
    {
        menuPanels.ArenaPanel.SetActive(false);
        Amplitude.Instance.logEvent("ArenaClose");
    }

    public void CheckpointOpen()
    {
        menuPanels.CheckpointPanel.SetActive(true);
        Amplitude.Instance.logEvent("CheckpointOpen");
        if (PlayerPrefs.GetInt("CheckpointWin1") == 1)
        {
            levelSettingz.CheckpointLv2.interactable = true;
            levelSettingz.lockCHK1.SetActive(false);
        }

    }

    public void CheckpointClose()
    {
        menuPanels.CheckpointPanel.SetActive(false);
        Amplitude.Instance.logEvent("CheckpointClose");
    }

    public void TopSpeedOpen()
    {
        menuPanels.TopSpeedPanel.SetActive(true);
        Amplitude.Instance.logEvent("TopSpeedOpen");
    }

    public void TopSpeedClose()
    {
        menuPanels.TopSpeedPanel.SetActive(false);
        Amplitude.Instance.logEvent("TopSpeedClose");
    }

    public void AchivOpen()
    {
        menuPanels.AchivmentPanel.SetActive(true);
        Amplitude.Instance.logEvent("AchivOpen");
        AchivText.text = PlayerPrefs.GetInt("ArenaWinsLevel1").ToString();
        CrownText.text = PlayerPrefs.GetInt("Crown").ToString();
    }

    public void AchivClose()
    {
        menuPanels.AchivmentPanel.SetActive(false);
        Amplitude.Instance.logEvent("AchiClose");
    }

    public void FreerideOpen()
    {
        menuPanels.FreerideMainPanel.SetActive(true);
        Amplitude.Instance.logEvent("FreerideOpen");
    }

    public void FreerideClose()
    {
        menuPanels.FreerideMainPanel.SetActive(false);
        Amplitude.Instance.logEvent("FreerideClose");
    }
    public void CityOpen()
    {
        menuPanels.CityPanel.SetActive(true);
        Amplitude.Instance.logEvent("CityOpen");
    }

    public void CityClose()
    {
        menuPanels.CityPanel.SetActive(false);
        Amplitude.Instance.logEvent("CityClose");
    }

    #endregion

    #region CoinFX

    public void animate(int amount)
    {

        for (int i = 0; i < amount; i++)
        {

            Transform t = Instantiate(menuGUI.rewardPrefab, transform.position, Quaternion.identity, menuGUI.parent);
            Vector3 randomPosition = new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1),
                                                    Random.Range(transform.position.y - 1, transform.position.y + 1), 0);
            Sequence coinSquence = DOTween.Sequence();
            coinSquence.Append(t.DOMove(randomPosition, Random.Range(.3f, .6f)).SetEase(Ease.OutBack));
            coinSquence.Append(t.DOMove(menuGUI.destanition.position, Random.Range(.25f, .5f)).SetEase(Ease.Linear));
            coinSquence.OnComplete(() =>
            {
                audioUi.fxCash.Play();
                Destroy(t.gameObject);
                
            });
        }
    }

    #endregion



    void Awake()
    {
        
        manage = this;
        Application.targetFrameRate = 500;
        ///SystemObj
        PoolPrefActive.SetActive(false);
        RatingForCarPanel.SetActive(false);
        Soundtracks();
        ///CarConfig
        ResolutionOnAwake();
        LoadEngineUprgadeOnAwake();
        LoadHandlingOnAwake();
        LoadHandlingOnSelectedCar();
        LoadEngineUpgradeOnSelectedCar();
        LoadBrakeOnAwake();
        //LoadUpgradeOnAwake();
        LoadBrakeOnSelectedCar();
        CarSettingBuyNosTurboState();
        ActiveCurrentColor();
        LoadWheelDriveOnAwake();
        //// Setting
        LoadQualitySetting();
        ButtonScale();
        SoundOnOff();
        
        //// Audio
        menuGUI.audio.isOn = (PlayerPrefs.GetInt("AudioActive") == 0) ? true : false;
        audioUi.fxAudio.GetComponent<AudioSource>().mute = (PlayerPrefs.GetInt("AudioActive") == 0) ? false : true;
        audioUi.fxAudioclick2.GetComponent<AudioSource>().mute = (PlayerPrefs.GetInt("AudioActive") == 0) ? false : true;
        audioUi.fxAudioTuning.GetComponent<AudioSource>().mute = (PlayerPrefs.GetInt("AudioActive") == 0) ? false : true;
        audioUi.fxAudioturbo.GetComponent<AudioSource>().mute = (PlayerPrefs.GetInt("AudioActive") == 0) ? false : true;
        audioUi.fxAudioturbo2.GetComponent<AudioSource>().mute = (PlayerPrefs.GetInt("AudioActive") == 0) ? false : true;
        menuGUI.music.isOn = (PlayerPrefs.GetInt("MusicActive") == 0) ? true : false;
        menuGUI.sound.isOn = (PlayerPrefs.GetInt("SoundActive") == 0) ? true : false;
        //// Control
        menuGUI.vibrateToggle.isOn = (PlayerPrefs.GetInt("VibrationActive") == 0) ? true : false;
        menuGUI.SteeringWheelMode.isOn = (PlayerPrefs.GetInt("SteeringWheel") == 0) ? true : false;
        menuGUI.ButtonMode.isOn = (PlayerPrefs.GetInt("ButtonMode") == 0) ? true : false;

    }
    void Start()
    {
        //config
        menuGUI.RUSure.SetActive(false);
        network_manager_active.SetActive(false);
        menuGUI._playerName.text = PlayerPrefs.GetString("Player");
        if (PlayerPrefs.GetInt("Energy") == 25)
        {
            menuGUI.adsEnergyBtn.interactable = false;
        }
        CurrentPanel(0);
    }




    #region Trash

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(Application.loadedLevel);
    }
    public void TuningButton()
    {
        Amplitude.Instance.logEvent("Tuning");
        Invoke("latencyAnim",0.1f);
    }

    void latencyAnim()
    {
        GameObject anim1 = GameObject.Find("Body");
        anim1.GetComponent<Animator>().SetBool("push", true);
    }

    public void engEvent()
    {
        Amplitude.Instance.logEvent("ENG");
    }

    public void GermanyEvent()
    {
        Amplitude.Instance.logEvent("Germany");
    }

    public void JapanEvent()
    {
        Amplitude.Instance.logEvent("Japan");
    }

    public void ruEvent()
    {
        Amplitude.Instance.logEvent("RU");
    }

    public void PolandEvent()
    {
        Amplitude.Instance.logEvent("Polish");
    }

    public void KoreanEvent()
    {
        Amplitude.Instance.logEvent("Korean");
    }

    public void PortoguesEvent()
    {
        Amplitude.Instance.logEvent("Portugalias");
    }

    public void GetMoney()
    {
        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 100000f);
    }

    public void ratingBtn()
    {
        PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") +100);
    }
    #endregion


    void Update()
    {

        LoadUpgradeOnAwake();
        LoadUpgrade();
        LoadUpgradeHandling();
        LoadUpgradeBrake();
        LoadWheelSusspension();
        WheelSusspensionTxRear();
        WheelSusspensionTxfront();
        TCSTx();
        ESPTx();
        TractionTx();
        SoundOnOff();
        #region Car Config setup
        menuGUI.frontS.value = PlayerPrefs.GetFloat("WheelSusspFront" + currentCarNumber.ToString(), menuGUI.frontS.value);
        menuGUI.RearS.value = PlayerPrefs.GetFloat("WheelSusspRear" + currentCarNumber.ToString(), menuGUI.RearS.value);
        menuGUI.TCS.value = PlayerPrefs.GetFloat("TCSsetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.TCS.value);
        menuGUI.ESP.value = PlayerPrefs.GetFloat("ESPsetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.ESP.value);
        menuGUI.Traction.value = PlayerPrefs.GetFloat("TractionSetup" + PlayerPrefs.GetInt("CurrentCar").ToString(), menuGUI.Traction.value);
        #endregion
       
        /////Coin Display
        cashAmount.text = ((int)PlayerPrefs.GetFloat("DriftCoin")).ToString() + "$";
        Rating.text = PlayerPrefs.GetInt("Rating").ToString();
        ratingforCar.text = carSetting[currentCarNumber].CarRating.ToString();
        //menuGUI.CarMaxSpeed.text = carSetting[currentCarNumber].carPower.speed.ToString() + "KMH";
        menuGUI.CarName.text = carSetting[currentCarNumber].name.ToString();
        //speedfill = (carSetting[currentCarNumber].car.GetComponent<RCC_CarControllerV3>().maxspeed) / 550f;
        speedfill = (carSetting[currentCarNumber].carPower.speed) / 550f;
        turbofill = ((carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().maxspeed + carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().engineTorque) / 8f) / 1000f;
        handlingfill = ((carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength * 600f) + (carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength * 600f + carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().maxspeed / 20f) + carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold * 100f) / 1000f;  
        driftfill = (carSetting[currentCarNumber].car.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold)*2f;
        menuGUI.barSpeed.fillAmount = speedfill;
        menuGUI.barTurbo.fillAmount = turbofill;
        menuGUI.barHandling.fillAmount = handlingfill;
        menuGUI.barDrift.fillAmount = driftfill;
        ////
        menuGUI.barTuningSpeed.fillAmount = speedfill;
        menuGUI.barTuningTurbo.fillAmount = turbofill;
        menuGUI.barTuningHandling.fillAmount = handlingfill;
        menuGUI.barTuningDrift.fillAmount = driftfill;
        //upgrade price
        menuGUI.NitroPrice.text = carSetting[currentCarNumber].nitroPrice.ToString() + "$";
        menuGUI.TurboPrice.text = carSetting[currentCarNumber].turboPrice.ToString() + "$";
        menuGUI.ABSPrice.text = carSetting[currentCarNumber]._ABSPrice.ToString() + "$";
        menuGUI.WheelSusspPriceF.text = carSetting[currentCarNumber].WheelSusspensionPrice[0].ToString() + "$";
        menuGUI.WheelSusspPriceR.text = carSetting[currentCarNumber].WheelSusspensionPrice[0].ToString() + "$";
        menuGUI.TCSPrice.text = carSetting[currentCarNumber].TCSPrice[0].ToString()+"$";
        menuGUI.ESPPrice.text = carSetting[currentCarNumber].ESPPrice[0].ToString() + "$";
        menuGUI.TractionPrice.text = carSetting[currentCarNumber].TractionPrice[0].ToString() + "$";
        menuGUI.WheelDrivePrice.text = carSetting[currentCarNumber].WheelDrivePrice[0].ToString() + "$";
        // Shop system for cars
        if (carSetting[currentCarNumber].Bought)
        {
            menuGUI.customizeVehicle.SetActive(true);
            menuGUI.buyNewVehicle.gameObject.SetActive(false);
            menuGUI.levelChooser.SetActive(true);
            menuGUI.CarName.text = carSetting[currentCarNumber].name;
            menuGUI.CarPrice.text = "";
            PlayerPrefs.SetInt("myCar", 1);
            PlayerPrefs.SetInt("CurrentVehicle", currentCarNumber);
        }
        else
        {
            menuGUI.customizeVehicle.SetActive(false);
            menuGUI.buyNewVehicle.gameObject.SetActive(true);

            menuGUI.CarName.text = carSetting[currentCarNumber].name;
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                menuGUI.CarPrice.text = "Price: " + carSetting[currentCarNumber].price.ToString();
            }
            else
            {
                menuGUI.CarPrice.text = "Цена " + carSetting[currentCarNumber].price.ToString();
            }




            menuGUI.levelChooser.SetActive(false);
            PlayerPrefs.SetInt("myCar", 0);
        }
        if (carSetting[currentCarNumber].price == 0)
        {
            menuGUI.customizeVehicle.SetActive(true);
            menuGUI.buyNewVehicle.gameObject.SetActive(false);
            carSetting[currentCarNumber].Bought = true;
            PlayerPrefs.SetInt("myCar", 1);
            menuGUI.CarName.text = carSetting[currentCarNumber].name;
            menuGUI.CarPrice.text = "";
            PlayerPrefs.SetInt("CurrentVehicle", currentCarNumber);
            menuGUI.levelChooser.SetActive(true);
        }
        if (PlayerPrefs.GetInt("BoughtCar"+currentCarNumber)==1)
        {
            menuGUI.customizeVehicle.SetActive(true);
            menuGUI.buyNewVehicle.gameObject.SetActive(false);
            menuGUI.levelChooser.SetActive(true);
            menuGUI.CarName.text = carSetting[currentCarNumber].name;
            menuGUI.CarPrice.text = "";
            PlayerPrefs.SetInt("CurrentVehicle", currentCarNumber);
            PlayerPrefs.SetInt("myCar", 1);
        }

        /// player rating for buy car
        if (carSetting[currentCarNumber].CarRating <= PlayerPrefs.GetInt("Rating"))
        {
            RatingForCarPanel.SetActive(false);
            menuGUI.buyNewVehicle.interactable = true;
        } else
        {
            RatingForCarPanel.SetActive(true);
            menuGUI.buyNewVehicle.interactable = false;
        }

        // players in-app cars
        //print("Car# " + PlayerPrefs.GetInt("CurrentVehicle: ", currentCarNumber));
        #region King In-App cars
        if (!carSetting[currentCarNumber].isInAppCar)
        {
            carSetting[currentCarNumber].achievements_[0].SetActive(false);
            inAppLamboPanel.SetActive(false);
            inAppRRPanel.SetActive(false);
            inAppVettyPanel.SetActive(false);
        }
        /// Rolls Royce
        
        if (PlayerPrefs.GetInt("CurrentVehicle: ", currentCarNumber) == 13)
        {
            if (carSetting[currentCarNumber].isInAppCar && PlayerPrefs.GetInt("RRBigBoss") == 0)
            {

                inAppCarPriceRR.text = carSetting[currentCarNumber].priceUSD + "$";
                inAppRRPanel.SetActive(true);
                menuGUI.customizeVehicle.SetActive(true);
                menuGUI.buyNewVehicle.gameObject.SetActive(false);
                menuGUI.levelChooser.SetActive(false);

                PlayerPrefs.SetInt("myCar", 0);
            }
            else
            {
                inAppRRPanel.SetActive(false);
            }

            if (carSetting[currentCarNumber].isInAppCar && PlayerPrefs.GetInt("RRBigBoss") == 1)
            {

                menuGUI.customizeVehicle.SetActive(true);
                menuGUI.buyNewVehicle.gameObject.SetActive(false);
                menuGUI.levelChooser.SetActive(true);
                menuGUI.CarName.text = carSetting[currentCarNumber].name;
                menuGUI.CarPrice.text = "";
                PlayerPrefs.SetInt("CurrentVehicle", currentCarNumber);
                PlayerPrefs.SetInt("myCar", 1);
                inAppRRPanel.SetActive(false);
                carSetting[currentCarNumber].achievements_[0].SetActive(true);

            }
            else
            {
                carSetting[currentCarNumber].achievements_[0].SetActive(false);
            }

        }

        /// Lambo LP-750

        if (PlayerPrefs.GetInt("CurrentVehicle: ", currentCarNumber) == 7)
        {
            if (carSetting[currentCarNumber].isInAppCar && PlayerPrefs.GetInt("boughtLP750") == 1)
            {

                menuGUI.customizeVehicle.SetActive(true);
                menuGUI.buyNewVehicle.gameObject.SetActive(false);
                menuGUI.levelChooser.SetActive(true);
                menuGUI.CarName.text = carSetting[currentCarNumber].name;
                menuGUI.CarPrice.text = "";
                PlayerPrefs.SetInt("CurrentVehicle", currentCarNumber);
                PlayerPrefs.SetInt("myCar", 1);
                inAppLamboPanel.SetActive(false);
                carSetting[currentCarNumber].achievements_[0].SetActive(true);

            }
            else
            {
                carSetting[currentCarNumber].achievements_[0].SetActive(false);
            }

            if (carSetting[currentCarNumber].isInAppCar && PlayerPrefs.GetInt("boughtLP750") == 0)
            {
                inAppCarPriceLambo.text = carSetting[currentCarNumber].priceUSD + "$";
                inAppLamboPanel.SetActive(true);
                menuGUI.customizeVehicle.SetActive(true);
                menuGUI.buyNewVehicle.gameObject.SetActive(false);
                menuGUI.levelChooser.SetActive(false);

                PlayerPrefs.SetInt("myCar", 0);
            }
            else
            {
                inAppLamboPanel.SetActive(false);
            }
        }

        //// Vetty
        if (PlayerPrefs.GetInt("CurrentVehicle: ", currentCarNumber) == 14)
        {
            if (carSetting[currentCarNumber].isInAppCar && PlayerPrefs.GetInt("Vetty") == 0)
            {
                inAppCarPriceVetty.text = carSetting[currentCarNumber].priceUSD + "$";
                inAppVettyPanel.SetActive(true);
                inAppLamboPanel.SetActive(false);
                inAppRRPanel.SetActive(false);
                menuGUI.customizeVehicle.SetActive(true);
                menuGUI.buyNewVehicle.gameObject.SetActive(false);
                menuGUI.levelChooser.SetActive(false);

                PlayerPrefs.SetInt("myCar", 0);
            }
            else
            {
                inAppVettyPanel.SetActive(false);
            }

            if (carSetting[currentCarNumber].isInAppCar && PlayerPrefs.GetInt("Vetty") == 1)
            {
                menuGUI.customizeVehicle.SetActive(true);
                menuGUI.buyNewVehicle.gameObject.SetActive(false);
                menuGUI.levelChooser.SetActive(true);
                menuGUI.CarName.text = carSetting[currentCarNumber].name;
                menuGUI.CarPrice.text = "";
                PlayerPrefs.SetInt("CurrentVehicle", currentCarNumber);
                PlayerPrefs.SetInt("myCar", 1);
                inAppVettyPanel.SetActive(false);
                inAppLamboPanel.SetActive(false);
                inAppRRPanel.SetActive(false);
                carSetting[currentCarNumber].achievements_[0].SetActive(true);

            }
            else
            {
                carSetting[currentCarNumber].achievements_[0].SetActive(false);
            }

        }


        #endregion

        ///Recource cache
        if (CathingLoadFiles.manage.car_gt500 && CathingLoadFiles.manage.car_hotrodd
            && CathingLoadFiles.manage.car_buggy && CathingLoadFiles.manage.car_modelt
            && CathingLoadFiles.manage.car_bus && CathingLoadFiles.manage.car_bullet && CathingLoadFiles.manage.car_i8 && CathingLoadFiles.manage.car_lambo
            && CathingLoadFiles.manage.car_rr) //&& CathingLoadFiles.manage.car_rasta && CathingLoadFiles.manage.car_vetty)
        {
            PoolPrefActive.SetActive(true);
        }

        //print(svChecked.isCheckBody);
        //print(svChecked.isCheckWheels);
        //print(svChecked.isCheckDetail);
    }
}


