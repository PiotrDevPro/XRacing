using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public static CountDown manage;
    public GameObject _CountDown;
    public GameObject Fight;
    public AudioSource GetReady;
    public AudioSource Go;
    public bool isStartTime = false;
    //public AudioSource levelMusic;
    public GameObject LapTimer;
    public GameObject LapTimerAI;
    public GameObject CarControls;
    private GameObject CarControlActive;
    int count = 0;

    private void Awake()
    {
        manage = this;
    }

    private void Update()
    {
        count += 1;
        if (count == 1)
        {
            CarControlActive = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "battle_online" && !MainMenuManager.manage.isAllvsYou)
        {
            
            StartCoroutine(CountStart());

        }

        if (MainMenuManager.manage.isAllvsYou)
        {
            StartCoroutine(CountStartSurviveMode());
        }

    }

    IEnumerator CountStart()
    {
        LapTimer.SetActive(false);
        LapTimerAI.SetActive(false);
       
        yield return new WaitForSeconds(0.2f);
        _CountDown.GetComponent<Text>().text = "3";
        GetReady.Play();
        CarControlActive.GetComponentInChildren<RCC_CarControllerV3>().enabled = false;
        _CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        _CountDown.GetComponent<Text>().text = "2";
        _CountDown.SetActive(false);
        GetReady.Play();
        _CountDown.SetActive(true);
        yield return new WaitForSeconds(1); 
        _CountDown.GetComponent<Text>().text = "1";
        _CountDown.SetActive(false);
        GetReady.Play();
        _CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        _CountDown.SetActive(false);
        Go.Play();
        //levelMusic.Play();
        RCC_EnterExitCar.manage.isPlayerIn = true;
        LapTimer.SetActive(true);
        LapTimerAI.SetActive(true);
        CarControls.SetActive(true);
        isStartTime = true;
       
    }

    IEnumerator CountStartSurviveMode()
    {
        LapTimer.SetActive(false);
        LapTimerAI.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        _CountDown.GetComponent<Text>().text = "3";
        GetReady.Play();
        CarControlActive.GetComponentInChildren<RCC_CarControllerV3>().enabled = false;
        _CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        _CountDown.GetComponent<Text>().text = "2";
        _CountDown.SetActive(false);
        GetReady.Play();
        _CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        _CountDown.GetComponent<Text>().text = "1";
        _CountDown.SetActive(false);
        GetReady.Play();
        _CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        Fight.SetActive(true);
        Fight.GetComponent<Text>().text = "FIGHT";
        Invoke("Latency",1f);
        _CountDown.SetActive(false);
        Go.Play();
        RCC_EnterExitCar.manage.isPlayerIn = true;
        //levelMusic.Play();
        LapTimer.SetActive(true);
        LapTimerAI.SetActive(true);
        CarControls.SetActive(true);
        
        isStartTime = true;

    }

    void Latency()
    {
        Fight.SetActive(false); 
    }

}
