using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class blk3center : MonoBehaviour
{
    public GameObject people1;
    public GameObject people2;
    [Header("Optim")]
    [SerializeField] GameObject from_main_block;
    [SerializeField] GameObject block1;
    [SerializeField] GameObject block2;
    [SerializeField] GameObject block3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            people1.SetActive(true);
            people2.SetActive(true);
            Amplitude.Instance.logEvent("center3");
            
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                from_main_block.SetActive(false);
                block1.SetActive(false);
                block2.SetActive(false);
                block3.SetActive(false);
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            people1.SetActive(true);
            people2.SetActive(true);
            
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                from_main_block.SetActive(false);
                block1.SetActive(false);
                block2.SetActive(false);
                block3.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            people1.SetActive(false);
            people2.SetActive(false);
            
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                from_main_block.SetActive(true);
                block1.SetActive(true);
                block2.SetActive(true);
                block3.SetActive(true);
            }
        }
    }
}
