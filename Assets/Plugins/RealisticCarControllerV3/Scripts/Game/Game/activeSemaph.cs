using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeSemaph : MonoBehaviour
{
    public GameObject _semph;
    public GameObject _semph1;
    public GameObject _semph2;
    public GameObject _semph3;
    public GameObject block3;
    public GameObject block3_1;
    public GameObject block3_2;
    public GameObject block3_3;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            //_block3.SetActive(false);
            //_block3_1.SetActive(false);
            //_block3_2.SetActive(false);
            //_block3_3.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            //_block3.SetActive(false);
            //_block3_1.SetActive(false);
            //_block3_2.SetActive(false);
            //_block3_3.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            //_block3.SetActive(true);
           // _block3_1.SetActive(true);
            //_block3_2.SetActive(true);
            //_block3_3.SetActive(true);
        }
    }
}
