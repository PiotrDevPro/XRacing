using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OnlineCheckpoint : MonoBehaviour
{
    public static OnlineCheckpoint manage;
    private RCC_CarControllerV3 carController;
    int counter = 0;
    [SerializeField] GameObject cashSnd;
    [SerializeField] GameObject checkpointSound;
    [SerializeField] GameObject animPanel;
    [SerializeField] GameObject MainPoint;
    [SerializeField] GameObject Point1;
    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        cashSnd = GameObject.Find("ch");
        checkpointSound = GameObject.Find("checkpointSnd");
        print(PlayerPrefs.GetInt("AppActive"));
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 1000);
            animPanel.SetActive(true);
            animPanel.GetComponentInChildren<Text>().text = "+1000";
            cashSnd.GetComponent<AudioSource>().Play();
            checkpointSound.GetComponent<AudioSource>().Play();
            Invoke("latency",0.5f);
            MainPoint.transform.position = Point1.transform.position;
            counter += 1;

            if (counter == 2)
            {
                MainPoint.SetActive(false);
            }
        }
    }

    void latency()
    {
        animPanel.SetActive(false);
    }
}
