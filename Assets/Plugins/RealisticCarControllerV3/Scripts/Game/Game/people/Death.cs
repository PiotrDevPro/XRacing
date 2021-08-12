using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject ragdoll;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CarAI"))
        {
            print("DestroyFromCarAI");
            Instantiate(ragdoll, transform.position, transform.rotation);
            Amplitude.Instance.logEvent("DeadPeople");
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name != "level_lap6")
        {
            if (netManager.manage.DangerSpeed)
            {
                if (col.CompareTag("Player"))
                {
                    Instantiate(ragdoll, transform.position, transform.rotation);
                    Amplitude.Instance.logEvent("DeadPeople");
                    Destroy(gameObject);
                    PlayerPrefs.SetInt("Rating",PlayerPrefs.GetInt("Rating")+1);
                    RCC_DashboardInputs.manage.infoPanelAboutPeople.SetActive(true);
                    RCC_DashboardInputs.manage.infoPanelAboutPeople.GetComponentInChildren<Text>().text = "+100";
                    GameObject checkpointSound = GameObject.Find("checkpointSnd");
                    checkpointSound.GetComponent<AudioSource>().Play();
                    RCC_DashboardInputs.manage.PeoplePanel();
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 100);
                }
            }
        }

        else if (CarSpawnNew.manage.InstantiatedCar.GetComponent<RCC_CarControllerV3>().speed > 5f)
        {
            if (col.CompareTag("Player"))
            {
                
                Instantiate(ragdoll, transform.position, transform.rotation);
                Amplitude.Instance.logEvent("DeadPeople");
                Destroy(gameObject);
                PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 1);
                RCC_DashboardInputs.manage.infoPanelAboutPeople.SetActive(true);
                RCC_DashboardInputs.manage.infoPanelAboutPeople.GetComponentInChildren<Text>().text = "+100";
                GameObject checkpointSound = GameObject.Find("checkpointSnd");
                checkpointSound.GetComponent<AudioSource>().Play();
                RCC_DashboardInputs.manage.PeoplePanel();
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 100);

            }
        }
    }
}
