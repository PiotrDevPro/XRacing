using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CityCheckpoint : MonoBehaviour
{
    public static CityCheckpoint manage;
    private RCC_CarControllerV3 carController;
    [Header("Checkpoint")]
    [SerializeField] GameObject task_zone;
    [SerializeField] GameObject MainPoint;
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [SerializeField] GameObject point3;
    [SerializeField] GameObject point4;
    [SerializeField] GameObject point5;
    [Header("Sound")]
    [SerializeField] GameObject sound;
    [SerializeField] GameObject soundCheckpoint;
    [SerializeField] GameObject soundCheckpointFinish;
    [Header("Res")]
    [SerializeField] GameObject infoPanel;
    [SerializeField] Text timer;
    [SerializeField] GameObject timerContainer;
    private GameObject cashSnd;
    private float starttime = 45f;
    public float curr = 0f;
    private int count = 0;
    private int countUpd = 0;
    private int countPass = 0;

    bool isWin = false;
    bool isMissionStart = false;

    private void Awake()
    {
        manage = this;
    }
    void Update()
    {
        if (isMissionStart)
        {
            Timer();
            timer.text = curr.ToString("0");
        }
    }


    private void Start()
    {
        carController = FindObjectOfType<RCC_CarControllerV3>();
        cashSnd = GameObject.Find("ch");
        timerContainer.SetActive(false);
        isWin = false;
        isMissionStart = false;
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "city_single")
        {
            if (!RCC_EnterExitCar.manage.isPlayerIn)
            {
               ReminderAnim.manage.ManualDial_task1_no_car();
            }
            
        }
        #region City_Online
        if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "city_online")
        {
                countPass += 1;
                timerContainer.SetActive(true);

                if (countPass == 1)
                {
                    MainPoint.transform.position = point1.transform.position;
                    MainPoint.transform.rotation = point1.transform.rotation;
                    MainPoint.transform.localScale = point1.transform.localScale;
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 5);
                    isMissionStart = true;
                    Amplitude.Instance.logEvent("Task#1");
                    sound.GetComponent<AudioSource>().Play();
                    curr += 55f;
                    ReminderAnim.manage.ManualDial_task1();
                }

                if (countPass == 2)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 3);
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    RCC_DashboardInputs.manage.infoPanelAboutPeople.SetActive(true);
                    RCC_DashboardInputs.manage.PeoplePanel();
                    MainPoint.transform.position = point2.transform.position;
                    MainPoint.transform.rotation = point2.transform.rotation;
                    MainPoint.transform.localScale = point2.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point2");
                    curr += 45f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 3)
                {

                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 5);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    MainPoint.transform.position = point3.transform.position;
                    MainPoint.transform.rotation = point3.transform.rotation;
                    MainPoint.transform.localScale = point3.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point3");
                    curr += 45f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 4)
                {

                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    infoPanel.SetActive(true);
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 7);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    MainPoint.transform.position = point4.transform.position;
                    MainPoint.transform.rotation = point4.transform.rotation;
                    MainPoint.transform.localScale = point4.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point4");
                    curr += 25f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 5)
                {

                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 3);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    MainPoint.transform.position = point5.transform.position;
                    MainPoint.transform.rotation = point5.transform.rotation;
                    MainPoint.transform.localScale = point5.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point5");
                    curr += 15f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 6)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 10000);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+10000";
                    soundCheckpointFinish.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    Amplitude.Instance.logEvent("Task#1_point_finish");
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 40);
                    ReminderAnim.manage.ManualDial_task1_finish();
                    Invoke("latency", 5.7f);
                    Invoke("Win", 0.7f);
                    count = 0;
                    StopAllCoroutines();
                    MainPoint.SetActive(false);
                }
            }
        #endregion

        #region City_Single_Game
        if (col.gameObject.tag == "Player" && RCC_EnterExitCar.manage.isPlayerIn)
        {
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                countPass += 1;
                timerContainer.SetActive(true);

                if (countPass == 1)
                {
                    MainPoint.transform.position = point1.transform.position;
                    MainPoint.transform.rotation = point1.transform.rotation;
                    MainPoint.transform.localScale = point1.transform.localScale;
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 5);
                    isMissionStart = true;
                    Amplitude.Instance.logEvent("Task#1");
                    sound.GetComponent<AudioSource>().Play();
                    curr += 55f;
                    ReminderAnim.manage.ManualDial_task1();
                }

                if (countPass == 2)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 3);
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    RCC_DashboardInputs.manage.infoPanelAboutPeople.SetActive(true);
                    RCC_DashboardInputs.manage.PeoplePanel();
                    MainPoint.transform.position = point2.transform.position;
                    MainPoint.transform.rotation = point2.transform.rotation;
                    MainPoint.transform.localScale = point2.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point2");
                    curr += 35f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 3)
                {

                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 5);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    MainPoint.transform.position = point3.transform.position;
                    MainPoint.transform.rotation = point3.transform.rotation;
                    MainPoint.transform.localScale = point3.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point3");
                    curr += 50f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 4)
                {

                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    infoPanel.SetActive(true);
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 7);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    MainPoint.transform.position = point4.transform.position;
                    MainPoint.transform.rotation = point4.transform.rotation;
                    MainPoint.transform.localScale = point4.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point4");
                    curr += 35f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 5)
                {

                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 250);
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 3);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+250";
                    soundCheckpoint.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    MainPoint.transform.position = point5.transform.position;
                    MainPoint.transform.rotation = point5.transform.rotation;
                    MainPoint.transform.localScale = point5.transform.localScale;
                    Amplitude.Instance.logEvent("Task#1_point5");
                    curr += 30f;
                    count = 0;
                    Invoke("latency", 0.7f);
                    StopAllCoroutines();
                }

                if (countPass == 6)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 10000);
                    infoPanel.SetActive(true);
                    infoPanel.GetComponentInChildren<Text>().text = "+10000";
                    soundCheckpointFinish.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    Amplitude.Instance.logEvent("Task#1_point_finish");
                    PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 40);
                    ReminderAnim.manage.ManualDial_task1_finish();
                    Invoke("latency", 5.7f);
                    Invoke("Win", 0.7f);
                    count = 0;
                    StopAllCoroutines();
                    MainPoint.SetActive(false);
                }
            }
        }
            #endregion
        }

        void latency()
        {
            infoPanel.SetActive(false);
        }

        void Timer()
        {

            curr -= 1 * Time.deltaTime;
            if (curr <= 5)
            {
                count += 1;
                if (count == 1)
                {
                    StartCoroutine(TimeOver());
                }
            }

            if (curr <= 0)
            {
                curr = 0;
            }

        }


        IEnumerator TimeOver()
        {
            GameObject snd = GameObject.Find("alert");
            snd.GetComponent<AudioSource>().Play();
            //anim.GetComponent<Animator>().SetBool("push", true);
            yield return new WaitForSeconds(1f);
            snd.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
            snd.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
            snd.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
            snd.GetComponent<AudioSource>().Play();
            GameObject timerSnd1 = GameObject.Find("lose");
            timerSnd1.GetComponent<AudioSource>().Play();
            // anim.GetComponent<Animator>().SetBool("push", false);
            Invoke("Lose", 1f);

        }

        void Lose()
        {

            MainPoint.transform.position = task_zone.transform.position;
            MainPoint.transform.rotation = task_zone.transform.rotation;
            MainPoint.transform.localScale = task_zone.transform.localScale;
            Amplitude.Instance.logEvent("TimeOver");
            countPass = 0;
            isMissionStart = false;
            timerContainer.SetActive(false);
            ReminderAnim.manage.ManualDial_task1_failed();
            // losePanel.SetActive(true);
            GameObject loseSnd = GameObject.Find("TimeOut");
            loseSnd.GetComponent<AudioSource>().Play();
            //carController.KillOrStartEngine();

        }

        void Win()
        {
            isMissionStart = false;
            countPass = 0;
            timerContainer.SetActive(false);

        }
    }

