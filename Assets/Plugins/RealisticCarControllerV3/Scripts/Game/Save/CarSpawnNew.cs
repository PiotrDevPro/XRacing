using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//public enum ControlModeSelect { Simple = 1, Mobile = 2 }

public class CarSpawnNew : MonoBehaviour
{
    public static CarSpawnNew manage;
    public Transform PlayerCarPoint;
    public Transform spawnPoint;
    public GameObject[] CarsPrefabs;
    public Text maxspd;
    int count = 0;

    public GameObject nosButton;
    public Slider noslevel;
    public GameObject InstantiatedCar;

    void Awake()
    {
        manage = this;

    }

    void Start()
    {
        // GameObject InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation) as GameObject;
        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation) as GameObject;
        InstantiatedCar.transform.SetParent(PlayerCarPoint);

        Amplitude.Instance.logEvent("LevelStart");

        #region Car Lights
        if (PlayerPrefs.GetInt("CurrentCar") == 4 || PlayerPrefs.GetInt("CurrentCar") == 6)
        { 
            GameObject light = GameObject.Find("Ilum");
            light.SetActive(false);
        }
        #endregion

        #region Load Wheel Susspens
        if (PlayerPrefs.GetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar"));
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar"));

            InstantiatedCar.GetComponent<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspRear" + PlayerPrefs.GetInt("CurrentCar"));
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspRear" + PlayerPrefs.GetInt("CurrentCar"));
        } else if (PlayerPrefs.GetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar")) == 0)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;

            InstantiatedCar.GetComponent<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;
        }
        
        #endregion

        #region TCS 
        if (PlayerPrefs.GetInt("TCS" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectTCS" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().TCSThreshold = PlayerPrefs.GetFloat("TCSsetup" + PlayerPrefs.GetInt("CurrentCar"));
        }
        else
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().TCS = false;
        }

        #endregion

        #region Traction 
        if (PlayerPrefs.GetInt("Traction" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectTraction" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().tractionHelperStrength = PlayerPrefs.GetFloat("TractionSetup" + PlayerPrefs.GetInt("CurrentCar"));
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().tractionHelper = true;
        }
        else
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().tractionHelper = false;
        }

        #endregion

        #region ESP
        if (PlayerPrefs.GetInt("ESP" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectESP" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ESPThreshold = PlayerPrefs.GetFloat("ESPsetup" + PlayerPrefs.GetInt("CurrentCar"));
        }
        else
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ESP = false;
        }
        #endregion

        #region load Car Config
        if (!MainMenuManager.NOSisChecked)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().useNOS = false;
            nosButton.SetActive(false);
            noslevel.interactable = false;
            noslevel.maxValue = 0;

        }
        else
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().useNOS = true;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 35f;
            nosButton.SetActive(true);
            noslevel.interactable = true;
        }
        if (!MainMenuManager.TurboisChecked)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().useTurbo = false;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().useExhaustFlame = false;

        }
        else
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().useTurbo = true;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 10f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().useExhaustFlame = true;
        }
        if (!MainMenuManager.ABSisChecked)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABS = false;
        }
        else
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABS = true;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.1f;
        }

        #endregion

        #region Load EngineUpdate
        if (MainMenuManager.manage.svChecked.engine0)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed = InstantiatedCar.GetComponent<RCC_CarControllerV3>().defMaxSpeed;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque = InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque;
        }
        if (MainMenuManager.manage.svChecked.engine1)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 15f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 250f;
        }
        if (MainMenuManager.manage.svChecked.engine2)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 30f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 350f;
        }

        if (MainMenuManager.manage.svChecked.engine3)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 50f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 500f;
        }

        if (MainMenuManager.manage.svChecked.engine4)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 70f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 600f;
        }

        if (MainMenuManager.manage.svChecked.engine5)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 80f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 700f;
        }
        if (MainMenuManager.manage.svChecked.engine6)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 90f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 800f;
        }

        if (MainMenuManager.manage.svChecked.engine7)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 100f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 900f;
        }

        if (MainMenuManager.manage.svChecked.engine8)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 110f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 1200f;
        }
        if (MainMenuManager.manage.svChecked.engine9)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 115f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 1300f;
        }

        if (MainMenuManager.manage.svChecked.engine10)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 125f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 1400f;
        }

        if (MainMenuManager.manage.svChecked.engine11)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 135f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 1500f;
        }
        if (MainMenuManager.manage.svChecked.engine12)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 145f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 1600f;
        }
        if (MainMenuManager.manage.svChecked.engine13)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 155f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 1700f;
        }

        if (MainMenuManager.manage.svChecked.engine14)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 175f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 1850f;
        }
        if (MainMenuManager.manage.svChecked.engine15)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed += 195f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().engineTorque += 2150f;
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
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
        }
        if (MainMenuManager.manage.svChecked.handling2)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.1f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.1f;
        }
        if (MainMenuManager.manage.svChecked.handling3)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.15f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.15f;
        }
        if (MainMenuManager.manage.svChecked.handling4)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.2f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.2f;
        }
        if (MainMenuManager.manage.svChecked.handling5)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.25f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.25f;
        }
        if (MainMenuManager.manage.svChecked.handling6)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.28f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.28f;
        }
        if (MainMenuManager.manage.svChecked.handling7)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.35f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.35f;
        }
        if (MainMenuManager.manage.svChecked.handling8)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.45f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.45f;
        }
        if (MainMenuManager.manage.svChecked.handling9)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.6f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.6f;
        }
        if (MainMenuManager.manage.svChecked.handling10)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.7f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.7f;
        }
        if (MainMenuManager.manage.svChecked.handling11)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.8f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.8f;
        }
        if (MainMenuManager.manage.svChecked.handling12)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.9f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.9f;
        }
        #endregion

        #region BrakeUpdate
        if (MainMenuManager.manage.svChecked.brake0)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold = 0.1f;
           // InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque = 1200f;

        }
        if (MainMenuManager.manage.svChecked.brake1)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.025f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 100f;
        }
        if (MainMenuManager.manage.svChecked.brake2)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.05f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 200f;
        }
        if (MainMenuManager.manage.svChecked.brake3)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.075f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 300f;
        }
        if (MainMenuManager.manage.svChecked.brake4)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.125f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 400f;
        }
        if (MainMenuManager.manage.svChecked.brake5)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.2f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 500f;
        }
        if (MainMenuManager.manage.svChecked.brake6)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.225f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 600f;
        }
        if (MainMenuManager.manage.svChecked.brake7)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.3f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 700f;
        }
        if (MainMenuManager.manage.svChecked.brake8)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.35f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 800f;
        }
        if (MainMenuManager.manage.svChecked.brake9)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.4f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 900f;
        }
        if (MainMenuManager.manage.svChecked.brake10)
        {
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().ABSThreshold += 0.45f;
            InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque += 1000f;

            #endregion

        #region Wheel Drive Mode
            if (MainMenuManager.WheelDriveisChecked)
            {
                switch (PlayerPrefs.GetString("DriveMode"))
                {
                    case "":
                        InstantiatedCar.GetComponent<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
                        break;
                    case "RWD":
                        InstantiatedCar.GetComponent<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
                        break;
                    case "FWD":
                        InstantiatedCar.GetComponent<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.FWD;
                        break;
                    case "AWD":
                        InstantiatedCar.GetComponent<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.AWD;
                        break;
                }
            }
            else
            {
                InstantiatedCar.GetComponent<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
            }
            #endregion

        }
    }

    void Update()
    {
        maxspd.text = "MAX:" + InstantiatedCar.GetComponent<RCC_CarControllerV3>().maxspeed.ToString() + "KM/H";
        if (InstantiatedCar.GetComponent<RCC_CarControllerV3>().speed > 30f)
        {
            count += 1;
          if (count == 1)
            {
                Amplitude.Instance.logEvent("StartRide > 30 kmh");

            }
        }
    }
}
