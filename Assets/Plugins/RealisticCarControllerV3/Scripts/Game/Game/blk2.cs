using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blk2 : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _block3_2;
    public GameObject people1;
    public GameObject people2;
    [Header("Optim")]
    [SerializeField] GameObject from_main_block;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            people1.SetActive(true);
            people2.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(true);
            Amplitude.Instance.logEvent("center2");
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                from_main_block.SetActive(false);
                _main.SetActive(true);
                _block3_2.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            people1.SetActive(true);
            people2.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(true);
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                from_main_block.SetActive(false);
                _main.SetActive(true);
                _block3_2.SetActive(false);
            }
            //   print("OnTriggerStay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            people1.SetActive(false);
            people2.SetActive(false);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                from_main_block.SetActive(false);
                _main.SetActive(true);
                _block3_2.SetActive(false);
            }
            //  print("OnTriggerExit");
        }
    }
}

