using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine;

public class InsideOutsideCar : MonoBehaviourPunCallbacks
{
    public static InsideOutsideCar manage;
    private RCC_CarControllerV3 carController;
    public GameObject FPScontroller;
    public GameObject OutFromCarButton;
    private RCC_EnterExitCar rcc_ent;
    private Transform playerCar;
    private GameObject OutPos;
    public int count = 0;

    [Header("Control")]
    public GameObject carInput;
    public GameObject _UI;
    public GameObject Rcc_canvas;
    public GameObject characterInput;

    [Header("Panel")]
    public GameObject enter_panel;

    [Header("Camera")]
    public Transform mainCam;
    public GameObject mainCamTag;

    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        if (MainMenuManager.manage.isAllvsYou)
        {
            OutFromCarButton.SetActive(true);
        }

        FPScontroller.SetActive(false);
        characterInput.SetActive(false);
        carInput.SetActive(true);
        _UI.SetActive(true);
        Rcc_canvas.SetActive(true);
    }

    public void InOut()
    {
        if (RCC_EnterExitCar.manage.isPlayerIn)
        {
            if (SceneManager.GetActiveScene().name != "city_online" || SceneManager.GetActiveScene().name != "battle_online")
            {
                SickscoreGames.HUDNavigationSystem.HUDNavigationSystem.Instance.count = 0;
                playerCar.GetComponentInChildren<BoxCollider>().tag = "PlayerCar";
                SickscoreGames.HUDNavigationSystem.HUDNavigationSystem.Instance.PlayerCamera = mainCam.GetComponentInChildren<Camera>();
            }
                FPScontroller.transform.position = OutPos.transform.position;
                FPScontroller.SetActive(true);
                carInput.SetActive(false);
                characterInput.SetActive(true);
                _UI.SetActive(false);
                Rcc_canvas.SetActive(false);
                RCC_EnterExitCar.manage.GetOut();
                Amplitude.Instance.logEvent("ExitFromCar");
              
        }
    }

    public void fixThaCam()
    {
        if (!RCC_EnterExitCar.manage.isPlayerIn)
        {
            mainCamTag.AddComponent<BoxCollider>().tag = "MainCamera";
        }
    }

    private void Update()
    {

        count += 1;
        if (count == 1)
        {
            playerCar = GameObject.FindGameObjectWithTag("Player").transform;
            OutPos = GameObject.FindGameObjectWithTag("inOut");
        }
    }
}
