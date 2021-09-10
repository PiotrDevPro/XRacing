using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Death : MonoBehaviour
{
    public static Death manage;
    public GameObject ragdoll;

    private void Awake()
    {
        manage = this;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("CarAI"))
        {
            Instantiate(ragdoll, transform.position, transform.rotation);
            Amplitude.Instance.logEvent("DeadPeopleFromCarAI");
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "battle_online" || SceneManager.GetActiveScene().name == "city_online")
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
                    GameObject blood_fx = GameObject.Find("BloodSprayEffect");
                    blood_fx.transform.position = transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                    blood_fx.GetComponent<ParticleSystem>().Play();
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
                GameObject blood_fx = GameObject.Find("BloodSprayEffect");
                blood_fx.transform.position = transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                blood_fx.GetComponent<ParticleSystem>().Play();
                checkpointSound.GetComponent<AudioSource>().Play();
                RCC_DashboardInputs.manage.PeoplePanel();
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 100);

            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("baseball_bat"))
        {
            
            Instantiate(ragdoll, transform.position, transform.rotation);
            Amplitude.Instance.logEvent("DeadPiplFromBassball");
            Destroy(gameObject);
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 1);
            RCC_DashboardInputs.manage.infoPanelAboutPeople.SetActive(true);
            RCC_DashboardInputs.manage.infoPanelAboutPeople.GetComponentInChildren<Text>().text = "+100";
            GameObject hit_sound = GameObject.Find("hit_on");
            hit_sound.GetComponent<AudioSource>().Play();
            GameObject blood_fx = GameObject.Find("BloodSprayEffect");
            blood_fx.transform.position = transform.position = new Vector3(transform.position.x, transform.position.y+2f, transform.position.z);
            blood_fx.GetComponent<ParticleSystem>().Play();
            GameObject blood_strm_fx = GameObject.Find("BloodStreamEffect");
            blood_strm_fx.transform.position = transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            blood_strm_fx.GetComponent<ParticleSystem>().Play();
            RCC_DashboardInputs.manage.PeoplePanel();
            PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 100);
        }
    }
}
