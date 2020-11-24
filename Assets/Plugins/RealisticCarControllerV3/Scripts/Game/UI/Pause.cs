using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine;

public class Pause : MonoBehaviour
{
    
    public GameObject PausePanel;
    public GameObject steeringWheelControl;
    public GameObject buttonControl;
    public AudioSource[] tracks;

    public Toggle vibroToggle,Steeringwheel,buttonControltgl,musicToggle;

    private GameObject _player;
    int count = 0;

    void Awake()
    {
        
        vibroToggle.isOn = (PlayerPrefs.GetInt("VibrationActive") == 0) ? true : false;
        Steeringwheel.isOn = (PlayerPrefs.GetInt("SteeringWheel") == 0) ? true : false;
        buttonControltgl.isOn = (PlayerPrefs.GetInt("ButtonMode") == 0) ? true : false;
        musicToggle.isOn = (PlayerPrefs.GetInt("Soundtrack") == 0) ? true : false;
    }

    void Start()
    {
        
        if (PlayerPrefs.GetInt("Soundtrack") == 0)
        {
                tracks[Random.Range(0, 8)].Play();
        }
        PausePanel.SetActive(false);
        
    }

    private void Update()
    {
        count += 1;
        if (count == 1)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    public void PausePressed()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (PlayerPrefs.GetInt("Soundtrack") == 1)
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
        }
    }

    public void MainMenu()
    {
        PhotonNetwork.Destroy(_player.gameObject);
        Amplitude.Instance.logEvent("MainMenu");
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
            PlayerPrefs.SetInt("VibrationActive", 0);
        else
            PlayerPrefs.SetInt("VibrationActive", 1);
    }

    public void DisableSoundButton(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("Soundtrack", 0);
            tracks[Random.Range(0, 8)].GetComponent<AudioSource>().Play();
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
            tracks[7].GetComponent<AudioSource>().Stop();
            tracks[8].GetComponent<AudioSource>().Stop();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

