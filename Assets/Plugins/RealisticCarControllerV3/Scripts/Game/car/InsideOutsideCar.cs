using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InsideOutsideCar : MonoBehaviour
{
    public static InsideOutsideCar manage;
    [SerializeField] GameObject FPScontroller;
    [SerializeField] GameObject OutFromCarActive;
    private RCC_EnterExitCar rcc_ent;
    private Transform playerCar;
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
            OutFromCarActive.SetActive(false);
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
            FPScontroller.SetActive(true);
            FPScontroller.transform.position = playerCar.position;
            carInput.SetActive(false);
            characterInput.SetActive(true);
            _UI.SetActive(false);
            Rcc_canvas.SetActive(false);
            Amplitude.Instance.logEvent("ExitFromCar");
            playerCar.GetComponentInChildren<BoxCollider>().tag = "Untagged"; 
            RCC_EnterExitCar.manage.GetOut();
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
        print(RCC_EnterExitCar.manage.isPlayerIn);
        count += 1;
        if (count == 1)
        {
            playerCar = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
