using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using UnityStandardAssets.ImageEffects;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public GameObject PausePanel;
    public GameObject steeringWheelControl;
    public GameObject buttonControl;
    public GameObject Traffic;
    public GameObject peopleAI;
    public AudioSource[] tracks;

    public Toggle vibroToggle,Steeringwheel,buttonControltgl,musicToggle,cameraMotionBlur, traff, people;

    private GameObject _player;
    private CameraMotionBlur _blur;
    
    int count = 0;

    void Awake()
    {
        
        vibroToggle.isOn = (PlayerPrefs.GetInt("VibrationActive") == 0) ? true : false;
        Steeringwheel.isOn = (PlayerPrefs.GetInt("SteeringWheel") == 0) ? true : false;
        buttonControltgl.isOn = (PlayerPrefs.GetInt("ButtonMode") == 0) ? true : false;
        musicToggle.isOn = (PlayerPrefs.GetInt("Soundtrack") == 0) ? true : false;
        if (SceneManager.GetActiveScene().name == "battle_online" || SceneManager.GetActiveScene().name == "city_online")
        {
            traff.isOn = (PlayerPrefs.GetInt("Traffic") == 0) ? true : false;
            people.isOn = (PlayerPrefs.GetInt("PeopleAI") == 0) ? true : false;
        }

        if (SceneManager.GetActiveScene().name == "level_top_speed_test")
        {
            traff.isOn = (PlayerPrefs.GetInt("Traffic") == 0) ? true : false;
        }
            
    }

    void Start()
    {
        

        if (PlayerPrefs.GetInt("Soundtrack") == 0)
        {
                tracks[Random.Range(0, 6)].Play();
        }

        if (SceneManager.GetActiveScene().name == "city_online")
        {
            if (PlayerPrefs.GetInt("Soundtrack") == 1)
            {
                tracks[7].Play();
            }
        }
        PausePanel.SetActive(false);
        _blur = FindObjectOfType<CameraMotionBlur>();

    }

    private void Update()
    {
        count += 1;
        if (count == 1)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            cameraMotionBlur.isOn = (PlayerPrefs.GetInt("Blur") == 0) ? true : false;
        }

    }

    public void PausePressed()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Amplitude.Instance.logEvent("Pause");
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Amplitude.Instance.logEvent("Resume");

        if (PlayerPrefs.GetInt("Soundtrack") == 1)
        {
            tracks[0].Stop();
            tracks[1].Stop();
            tracks[2].Stop();
            tracks[3].Stop();
            tracks[4].Stop();
            tracks[5].Stop();
            tracks[6].Stop();
        }
    }

    public void MainMenu()
    {
        
        PhotonNetwork.Destroy(_player.gameObject);
        Amplitude.Instance.logEvent("MainMenu");
        MainMenuManager.manage.isFreerideActive = false;
        SceneManager.LoadScene("garage");
        Time.timeScale = 1f;
        
    }

    public void DisableSteeringControl(Toggle toggle)
    {
        if (toggle.isOn)
        {
            steeringWheelControl.SetActive(true);
            PlayerPrefs.SetInt("SteeringWheel", 0);
        } else
        {
            steeringWheelControl.SetActive(false);
            PlayerPrefs.SetInt("SteeringWheel", 1);
        }
    }

    public void DisableBlur(Toggle toggle)
    {
        if (toggle.isOn)
        {
            _blur.enabled = true;
            PlayerPrefs.SetInt("Blur", 0);
            Amplitude.Instance.logEvent("BlurON");
        }
        else
        {
            _blur.enabled = false;
            PlayerPrefs.SetInt("Blur", 1);
            Amplitude.Instance.logEvent("BlurFalse");
        }
    }

    public void DisableTraffic(Toggle toggle)
    {
        if (toggle.isOn)
        {
            Traffic.SetActive(true);
            PlayerPrefs.SetInt("Traffic", 0);
            Amplitude.Instance.logEvent("TrafficEnable");
        }
        else
        {
            Traffic.SetActive(false);
            PlayerPrefs.SetInt("Traffic", 1);
            Amplitude.Instance.logEvent("TrafficDisable");
        }
    }

    public void DisablePeople(Toggle toggle)
    {
        if (toggle.isOn)
        {
            peopleAI.SetActive(true);
            PlayerPrefs.SetInt("PeopleAI", 0);
            Amplitude.Instance.logEvent("PeopleEnable");
        }
        else
        {
            peopleAI.SetActive(false);
            PlayerPrefs.SetInt("PeopleAI", 1);
            Amplitude.Instance.logEvent("PeopleDisable");
        }
    }

    public void DisableButtonControl(Toggle toggle)
    {
        if (toggle.isOn)
        {
            buttonControl.SetActive(true);
            PlayerPrefs.SetInt("ButtonMode",0);
            
        }
        else
        {
            buttonControl.SetActive(false);
            PlayerPrefs.SetInt("ButtonMode", 1);
        }
    }

    public void DisableVibro(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("VibrationActive", 0);
            Amplitude.Instance.logEvent("VibroEnable");
        }
        else
        {
            PlayerPrefs.SetInt("VibrationActive", 1);
            Amplitude.Instance.logEvent("VibroDisable");
        }
            
    }

    public void DisableSoundButton(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("Soundtrack", 0);
            tracks[Random.Range(0, 7)].GetComponent<AudioSource>().Play();
            Amplitude.Instance.logEvent("SoundeEnable");
            if (SceneManager.GetActiveScene().name == "city_online")
            {
                tracks[7].GetComponent<AudioSource>().Stop();
            }
        }
        else
        {
            PlayerPrefs.SetInt("Soundtrack", 1);
            tracks[0].GetComponent<AudioSource>().Stop();
            tracks[1].GetComponent<AudioSource>().Stop();
            tracks[2].GetComponent<AudioSource>().Stop();
            tracks[3].GetComponent<AudioSource>().Stop();
            tracks[4].GetComponent<AudioSource>().Stop();
            tracks[5].GetComponent<AudioSource>().Stop();
            tracks[6].GetComponent<AudioSource>().Stop();
            Amplitude.Instance.logEvent("SoundeDisable");
            if (SceneManager.GetActiveScene().name == "city_online")
            {
                tracks[7].GetComponent<AudioSource>().Play();
            }
            
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

