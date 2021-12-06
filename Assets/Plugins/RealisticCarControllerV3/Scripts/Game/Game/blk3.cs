using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blk3 : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _crossroad4;
    public GameObject _block3_1;
    public GameObject _block3_2;
    public GameObject _block3_3;
    public GameObject _block3_4;
    public GameObject people;
    [Header("Optimizations")]
    [SerializeField] GameObject Small_city1;
    [SerializeField] GameObject Small_city2;
    [SerializeField] GameObject Small_city3;
    [SerializeField] GameObject block1;
    [SerializeField] GameObject block2;
    [SerializeField] GameObject block3;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(true);
            people.SetActive(true);
            Amplitude.Instance.logEvent("block3_right");
            
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                _main.SetActive(false);
                _block3_2.SetActive(true);
                _block3_4.SetActive(true);
                Small_city1.SetActive(false);
                Small_city2.SetActive(false);
                Small_city3.SetActive(false);
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
           
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(true);
            people.SetActive(true);
            
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                Small_city1.SetActive(false);
                Small_city2.SetActive(false);
                Small_city3.SetActive(false);
                _main.SetActive(false);
                _block3_2.SetActive(true);
                _block3_4.SetActive(true);
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
            
            _crossroad1.SetActive(true);
            _crossroad2.SetActive(true);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(false);
            people.SetActive(false);
            
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                Small_city1.SetActive(true);
                Small_city2.SetActive(true);
                Small_city3.SetActive(true);
                _main.SetActive(true);
                _block3_2.SetActive(false);
                _block3_4.SetActive(false);
                block1.SetActive(false);
                block2.SetActive(false);
                block3.SetActive(false);
            }
        }
    }
}
