using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class baseball_b : MonoBehaviour
{
    public static baseball_b manage;
    [Header("Sound")]
    private AudioSource m_AudioSource;
    private Animator _anim;
    [SerializeField] AudioClip whooh_hit;
    [SerializeField] AudioClip empty_hit;
    [SerializeField] AudioClip hit_woman;

    public bool isAiCarDetect = false;
    

    private void Awake()
    {
        if (manage != null)
            manage = this;
    }

    private void Start()
    {
        m_AudioSource = GetComponentInChildren<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
         if (ControlFreak2.CF2Input.GetButtonDown("Fire1"))
        {
            _anim.SetTrigger("hit");
            whoo_hit_bassball_b();
        }
    }

    public void whoo_hit_bassball_b()
    {
        m_AudioSource.clip = whooh_hit;
        m_AudioSource.Play();
    }

    public void empt_hit_bassball_b_()
    {
        m_AudioSource.clip = empty_hit;
        m_AudioSource.Play();
    }

    public void hit_bassball_b_woman()
    {
        m_AudioSource.clip = hit_woman;
        m_AudioSource.Play();
    }

    #region config

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Car"))
        {
            empt_hit_bassball_b_();
            Amplitude.Instance.logEvent("bassball_hitOnCar");
        }

        if (col.gameObject.CompareTag("CarAI"))
        {
            empt_hit_bassball_b_();
            Amplitude.Instance.logEvent("hitOnCarAI");
            isAiCarDetect = true;
            //PlayerPrefs.SetInt("damageAi", 1);
            //print(isAiCarDetect);

        }

        if (col.gameObject.CompareTag("Obstacle"))
        {
            empt_hit_bassball_b_();
            Amplitude.Instance.logEvent("bassball_hitOnObstacle");
        }

        if (col.gameObject.CompareTag("PlayerCar"))
        {
            empt_hit_bassball_b_();
            Amplitude.Instance.logEvent("bassball_hitOnPlayerCar");
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Car"))
        {

            
        }

        if (col.gameObject.CompareTag("CarAI"))
        {
            //isAiCarDetect = false;

        }

        if (col.gameObject.CompareTag("Obstacle"))
        {


        }
    }

    #endregion
}
