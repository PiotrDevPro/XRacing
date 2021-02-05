using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine;

    public class Checkpoint : MonoBehaviour
    {
        public static Checkpoint manage;
        private Pause pauza;
        private RCC_CarControllerV3 carController;
        private RCC_AIWaypointsContainer _waypointsContainer;
        private RCC_AICarController _ai_carController;
        private GameObject _player;
        private GameObject ArrowActive;
        public GameObject losePanel;
        public GameObject winPanel;
        private float starttime = 45f;
        public float curr = 0f;


        private int count = 0;
        private int countUpd = 0;
        private int countPlayerEnter = 0;
        private int countPass = 0;

        private float erndCoin = 0;
        private int totalPoint = 0;

        private GameObject anim;
        private GameObject coinAddAnim;
        private GameObject cashSnd;
        private GameObject loseSnd;
        private GameObject winSnd;
        private GameObject winSnd2;
        private GameObject checkpointSound;
        private GameObject cashTx;
        private GameObject erndCoinTx;
        [SerializeField] Text maxPointNumberWinPanel;
        [SerializeField] Text maxPointNumberLosePanel;
        [SerializeField] GameObject CheckpointContainer;
        [SerializeField] Text energy;
        [SerializeField] GameObject EnergyBar;
        private GameObject checkPointlb;
        private GameObject point1;
        private GameObject point2;
        private GameObject point3;
        private GameObject point4;
        private GameObject point5;
        private GameObject point6;
        private GameObject finish;
        private GameObject CheckPointNetwork;
        private GameObject AnimBonus;
        private GameObject cashRegSnd;

        public bool isWin = false;

        private GameObject[] carAi1;
        private void Awake()
        {
            manage = this;
        }

        private void Start()
        {
            pauza = FindObjectOfType<Pause>();
            carController = FindObjectOfType<RCC_CarControllerV3>();
            _waypointsContainer = FindObjectOfType<RCC_AIWaypointsContainer>();
            isWin = false;

            curr = starttime;


            if (SceneManager.GetActiveScene().name != "battle_online" && !MainMenuManager.manage.isFreerideActive && !MainMenuManager.manage.isAllvsYou)
            {
                anim = GameObject.Find("Tm");

            }
            else
            {
                CheckPointNetwork = GameObject.Find("Finish_point");

                //anim.SetActive(false);

            }

            point1 = GameObject.Find("point1");
            point2 = GameObject.Find("point2");
            point3 = GameObject.Find("point3");
            point4 = GameObject.Find("point4");
            point5 = GameObject.Find("point5");
            point6 = GameObject.Find("point6");
            finish = GameObject.Find("finish");
            cashSnd = GameObject.Find("ch");
            checkpointSound = GameObject.Find("checkpointSnd");
            if (SceneManager.GetActiveScene().name != "battle_online" && !MainMenuManager.manage.isFreerideActive && !MainMenuManager.manage.isAllvsYou)
            {
                coinAddAnim = GameObject.Find("CoinzPlus");
                cashRegSnd = GameObject.Find("CashReg");
                cashTx = GameObject.Find("cash");

                checkPointlb = GameObject.Find("point_num");
                AnimBonus = GameObject.Find("WinPanel");

                loseSnd = GameObject.Find("TimeOut");
                winSnd = GameObject.Find("Winner");
                winSnd2 = GameObject.Find("winwin");

                losePanel.SetActive(false);
                winPanel.SetActive(false);
            }



            PlayerPrefs.SetInt("erndcoin", 0);
            SaveManager.laps = 0;

            if (SceneManager.GetActiveScene().name == "level_top_speed_test")
            {
                curr += 55f;
                maxPointNumberWinPanel.text = "/2";
                maxPointNumberLosePanel.text = "/2";
            }

            if (SceneManager.GetActiveScene().name == "level_lap6")
            {
                maxPointNumberLosePanel.text = "/10";
                maxPointNumberWinPanel.text = "/10";
            }
        }
        private void Update()
        {

            if (SceneManager.GetActiveScene().name != "battle_online" && !MainMenuManager.manage.isFreerideActive && !MainMenuManager.manage.isAllvsYou)
            {
                if (CountDown.manage.isStartTime)
                {
                    Timer();
                    anim.GetComponent<Text>().text = curr.ToString("0");
                }
            }

            countUpd += 1;
            if (countUpd == 1)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
                carAi1 = GameObject.FindGameObjectsWithTag("CarAI");
                ArrowActive = GameObject.FindGameObjectWithTag("ArrowActive");
                if (MainMenuManager.manage.isFreerideActive || MainMenuManager.manage.isAllvsYou)
                {
                    ArrowActive.SetActive(false);
                    EnergyBar.SetActive(true);
                    point1.SetActive(false);
                    point2.SetActive(false);
                    point3.SetActive(false);
                    point4.SetActive(false);
                    point5.SetActive(false);
                    point6.SetActive(false);
                    CheckpointContainer.SetActive(false);
                }
                if (MainMenuManager.manage.isAllvsYou)
                {
                    EnergyBar.SetActive(true);
                    PlayerPrefs.SetInt("AiCrashed", 0);
                    PlayerPrefs.SetInt("AiCrashed1", 0);
                    PlayerPrefs.SetInt("AiCrashed2", 0);
                    _waypointsContainer.target = _player.transform;
                    carAi1[0].GetComponent<RCC_AICarController>()._AIType = RCC_AICarController.AIType.ChasePlayer;
                    carAi1[1].GetComponent<RCC_AICarController>()._AIType = RCC_AICarController.AIType.ChasePlayer;
                    carAi1[2].GetComponent<RCC_AICarController>()._AIType = RCC_AICarController.AIType.ChasePlayer;
                }
            }
        }
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {
                countPlayerEnter += 1;
                if (countPass == 0 && countPlayerEnter == 1)
                {


                    if (SceneManager.GetActiveScene().name == "level_lap6")
                    {
                        curr += 25f;
                        Amplitude.Instance.logEvent("CheckpointMode1");
                    }

                    if (SceneManager.GetActiveScene().name == "level_top_speed_test")
                    {
                        curr += 40f;
                        Amplitude.Instance.logEvent("TopSpeedMode1");
                    }
                    if (SceneManager.GetActiveScene().name == "battle_online")
                    {
                        // curr += 40f;
                        Amplitude.Instance.logEvent("Battle_Online_Checkpoint1");
                        CheckPointNetwork.transform.position = point2.transform.position;
                    }
                    else if (SceneManager.GetActiveScene().name != "battle_online")
                    {
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                        //print(PlayerPrefs.GetFloat("DriftCoin"));
                        checkpointSound.GetComponent<AudioSource>().Play();
                        totalPoint += 1;
                        coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                        anim.GetComponent<Animator>().SetBool("push", false);
                        erndCoin += 150;
                        checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                        StopAllCoroutines();
                    }
                    countPass += 1;
                    count = 0;
                    countPlayerEnter = 0;
                    cashSnd.GetComponent<AudioSource>().Play();
                    checkpointSound.GetComponent<AudioSource>().Play();
                    point1.transform.position = point2.transform.position;

                }

                if (countPass == 1 && countPlayerEnter == 1)
                {
                    if (SceneManager.GetActiveScene().name != "battle_online")
                    {
                        PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                        Amplitude.Instance.logEvent("CheckpointMode2");
                        countPass += 1;
                        totalPoint += 1;
                        curr += 25f;
                        coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                        anim.GetComponent<Animator>().SetBool("push", false);
                        erndCoin += 150;
                        checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                        StopAllCoroutines();
                    }
                    if (SceneManager.GetActiveScene().name == "level_top_speed_test")
                    {
                        Amplitude.Instance.logEvent("TopSpeedModeWin");
                        pauza.tracks[0].Stop();
                        pauza.tracks[1].Stop();
                        pauza.tracks[2].Stop();
                        pauza.tracks[3].Stop();
                        pauza.tracks[4].Stop();
                        pauza.tracks[5].Stop();
                        pauza.tracks[6].Stop();
                        Invoke("Win", 1f);
                    }
                    if (SceneManager.GetActiveScene().name == "battle_online")
                    {
                        Amplitude.Instance.logEvent("Battle_Online_Checkpoint2");
                        CheckPointNetwork.transform.position = point3.transform.position;
                    }

                    count = 0;
                    countPlayerEnter = 0;
                    checkpointSound.GetComponent<AudioSource>().Play();
                    cashSnd.GetComponent<AudioSource>().Play();
                    point1.transform.position = point3.transform.position;
                }

                if (countPass == 2 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode3");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 20f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = point4.transform.position;

                }

                if (countPass == 3 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode3");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 20f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = point5.transform.position;

                }

                if (countPass == 4 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode4");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 10f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = point6.transform.position;
                    point1.transform.rotation = point6.transform.rotation;
                    finish.GetComponent<BoxCollider>().enabled = true;


                }

                if (countPass == 5 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode5");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 15f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = point2.transform.position;
                    point1.transform.rotation = point2.transform.rotation;
                }

                if (countPass == 6 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode6");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 20f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = point3.transform.position;
                    //point1.transform.rotation = point6.transform.rotation;
                }

                if (countPass == 7 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode7");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 20f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = point4.transform.position;
                    //point1.transform.rotation = point6.transform.rotation;
                }

                if (countPass == 8 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode8");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 25f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = point5.transform.position;
                    finish.GetComponent<BoxCollider>().enabled = true;
                    //point1.transform.rotation = point6.transform.rotation;
                }

                if (countPass == 9 && countPlayerEnter == 1)
                {
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 150);
                    Amplitude.Instance.logEvent("CheckpointMode9");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    countPass += 1;
                    totalPoint += 1;
                    curr += 20f;
                    coinAddAnim.GetComponent<Animator>().SetBool("push", true);
                    cashSnd.GetComponent<AudioSource>().Play();
                    anim.GetComponent<Animator>().SetBool("push", false);
                    erndCoin += 150;
                    checkPointlb.GetComponent<Text>().text = totalPoint.ToString();
                    StopAllCoroutines();
                    count = 0;
                    countPlayerEnter = 0;
                    point1.transform.position = finish.transform.position;
                    pauza.tracks[0].Stop();
                    pauza.tracks[1].Stop();
                    pauza.tracks[2].Stop();
                    pauza.tracks[3].Stop();
                    pauza.tracks[4].Stop();
                    pauza.tracks[5].Stop();
                    pauza.tracks[6].Stop();
                    carController.KillOrStartEngine();
                    Invoke("Win", 1f);

                }
            }
        }
        private void OnTriggerExit(Collider col)
        {
            if (SceneManager.GetActiveScene().name != "battle_online")
                if (col.gameObject.tag == "Player")
                {
                    coinAddAnim.GetComponent<Animator>().SetBool("push", false);
                }
        }

        void Timer()
        {

            curr -= 1 * Time.deltaTime;
            if (curr <= 0)
            {
                curr = 0;
            }
            if (curr <= 5)
            {
                count += 1;
                if (count == 1)
                {
                    StartCoroutine(TimeOver());
                }
            }

        }

        void LatencyFinishActive()
        {
            finish.GetComponent<BoxCollider>().enabled = false;
        }

        IEnumerator TimeOver()
        {
            if (curr <= 5)
            {
                GameObject snd = GameObject.Find("alert");
                snd.GetComponent<AudioSource>().Play();
                anim.GetComponent<Animator>().SetBool("push", true);
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
                anim.GetComponent<Animator>().SetBool("push", false);
                Invoke("Lose", 1f);

            }
        }

        void Lose()
        {

            Amplitude.Instance.logEvent("TimeOver");
            losePanel.SetActive(true);
            loseSnd.GetComponent<AudioSource>().Play();
            erndCoinTx = GameObject.Find("coinNum");
        if (SceneManager.GetActiveScene().name == "level_top_speed_test")
        {
            erndCoinTx.GetComponent<Text>().text = (CarAi.manage.coin + CarAi1.manage.coin + CarAi2.manage.coin).ToString();
        }
        else
        {
            erndCoinTx.GetComponent<Text>().text = erndCoin.ToString();
        }
            GameObject chkPointCounter = GameObject.Find("pointNumCurr");
            chkPointCounter.GetComponent<Text>().text = totalPoint.ToString();
            GameObject lapsCounter = GameObject.Find("lapNum");
            lapsCounter.GetComponent<Text>().text = SaveManager.laps.ToString();
            carController.KillOrStartEngine();
            Invoke("EngineRunning", 1f);

        }

        void Win()
        {
            Amplitude.Instance.logEvent("Win");
            winPanel.SetActive(true);
            isWin = true;
            winSnd.GetComponent<AudioSource>().Play();
            winSnd2.GetComponent<AudioSource>().Play();
            erndCoinTx = GameObject.Find("coinNum");
        if (SceneManager.GetActiveScene().name == "level_top_speed_test")
        {
            erndCoinTx.GetComponent<Text>().text = (CarAi.manage.coin + CarAi1.manage.coin + CarAi2.manage.coin).ToString();
        }
        else
        {
            erndCoinTx.GetComponent<Text>().text = erndCoin.ToString();
        }
            GameObject chkPointCounter = GameObject.Find("pointNumCurr");
            chkPointCounter.GetComponent<Text>().text = totalPoint.ToString();
            GameObject lapsCounter = GameObject.Find("lapNum");
            lapsCounter.GetComponent<Text>().text = SaveManager.laps.ToString();
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 1000f);
            carController.KillOrStartEngine();
            Invoke("WinSound", 0.2f);
            Invoke("EngineRunning", 2f);
        }

        void WinSound()
        {
            cashRegSnd.GetComponent<AudioSource>().Play();
        }

        void EngineRunning()
        {
            carController.KillOrStartEngine();
        }

        public void Restart()
        {
            Time.timeScale = 1;
            Amplitude.Instance.logEvent("Restart");
            if (PlayerPrefs.GetInt("NoAds") != 1)
            {
                //ironSourceManager.manage.ShowRewardedVideoButtonClicked();
                //AppDealManager.manage.ShowRewardedAds20Video();
            }
            SceneManager.LoadScene(Application.loadedLevel);
            AudioListener.pause = false;
            SaveManager.laps = 0;
        }

        public void Menu()
        {
            Time.timeScale = 1;
            Amplitude.Instance.logEvent("Menu");
            MainMenuManager.manage.isFreerideActive = false;
            MainMenuManager.manage.isAllvsYou = false;
            PhotonNetwork.Destroy(_player.gameObject);
            if (PlayerPrefs.GetInt("NoAds") != 1)
            {
                //ironSourceManager.manage.ShowRewardedVideoButtonClicked();
                //AppDealManager.manage.ShowInterstatial();
            }
            SceneManager.LoadScene("garage");

        }

    public void ContinueAdsForTimeCheckpoint()
    {
        losePanel.SetActive(false);
        curr += 25;
        carController.StartEngine();
        count = 0;
        StopAllCoroutines();
    }
}

