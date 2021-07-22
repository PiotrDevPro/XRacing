using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ReminderAnim : MonoBehaviour
{
    public static ReminderAnim manage;
    private Animator _anim;
    [Header("Res")]
    [SerializeField] GameObject ReminderSound;
    [Header("Object")]
    [SerializeField] GameObject taskPanel;
    [Header("Tasks_city")]
    [SerializeField] GameObject task1;
    [SerializeField] GameObject task1_1;
    [SerializeField] GameObject task1_2;
    [SerializeField] GameObject task1_failed;
    [Header("Tasks_highway")]
    [SerializeField] GameObject task1_start_highway;
    private void Awake()
    {
        manage = this;
    }

    void Start()
    {
        taskPanel.SetActive(false);
        _anim = GetComponentInChildren<Animator>();
        if (SceneManager.GetActiveScene().name == "city_online")
        {
            task1.SetActive(true);
            task1_1.SetActive(false);
            task1_2.SetActive(false);
            task1_failed.SetActive(false);
            Invoke("show", 0.8f);
            Invoke("showSMS", 1.2f);
            Invoke("hide", 3.2f);
            Invoke("hideSMS", 5.2f);
        }
        if (SceneManager.GetActiveScene().name == "battle_online")
        {
            task1_start_highway.SetActive(true);
            task1.SetActive(false);
            task1_1.SetActive(false);
            task1_2.SetActive(false);
            task1_failed.SetActive(false);
            Invoke("show", 0.8f);
            Invoke("showSMS", 1.2f);
            Invoke("hide", 3.2f);
            Invoke("hideSMS", 5.2f);
        }
    }

    #region sms_reminder_tasks
    public void ManualDial_task1()
    {
        if (SceneManager.GetActiveScene().name == "city_online")
        {
            task1.SetActive(false);
            task1_1.SetActive(true);
            task1_2.SetActive(false);
            task1_failed.SetActive(false);
            Invoke("show", 0.8f);
            Invoke("showSMS", 1.2f);
            Invoke("hide", 3.2f);
            Invoke("hideSMS", 5.2f);
        }
    }

    public void ManualDial_task1_finish()
    {
        if (SceneManager.GetActiveScene().name == "city_online")
        {
            task1.SetActive(false);
            task1_1.SetActive(false);
            task1_2.SetActive(true);
            task1_failed.SetActive(false);
            Invoke("show", 0.8f);
            Invoke("showSMS", 1.2f);
            Invoke("hide", 3.2f);
            Invoke("hideSMS", 5.2f);
        }
    }

    public void ManualDial_task1_failed()
    {
        if (SceneManager.GetActiveScene().name == "city_online")
        {
            task1.SetActive(false);
            task1_1.SetActive(false);
            task1_2.SetActive(false);
            task1_failed.SetActive(true);
            Invoke("show", 0.8f);
            Invoke("showSMS", 1.2f);
            Invoke("hide", 3.2f);
            Invoke("hideSMS", 5.2f);
        }
    }



    #endregion
    void show()
    {
        _anim.SetBool("push", true);
        
        ReminderSound.GetComponent<AudioSource>().Play();
    }

    void hide()
    {
        _anim.SetBool("push", false);
    }


    void showSMS()
    {
        taskPanel.SetActive(true);
    }

    void hideSMS()
    {
        taskPanel.SetActive(false);
    }
}
