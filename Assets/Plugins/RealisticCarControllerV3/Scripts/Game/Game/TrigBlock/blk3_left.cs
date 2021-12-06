using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class blk3_left : MonoBehaviour
{
    public GameObject _main;
    public GameObject _crossroad1;
    public GameObject _crossroad2;
    public GameObject _crossroad3;
    public GameObject _crossroad4;
    public GameObject _crossroad5;
    public GameObject _block3_1;
    public GameObject _block3_2;
    public GameObject people;
    [Header("Optimizations")]
    [SerializeField] GameObject Small_city;
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
            
            people.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(true);
            _crossroad5.SetActive(true);
            Amplitude.Instance.logEvent("block3_left");
            
            if (SceneManager.GetActiveScene().name == "city_single")
            {
                _main.SetActive(false);
                _block3_1.SetActive(true);
                _block3_2.SetActive(true);
                Small_city.SetActive(false);
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
            
            people.SetActive(true);
            _crossroad1.SetActive(false);
            _crossroad2.SetActive(false);
            _crossroad3.SetActive(false);
            _crossroad4.SetActive(true);
            _crossroad5.SetActive(true);

            if (SceneManager.GetActiveScene().name == "city_single")
            {
                _main.SetActive(false);
                _block3_1.SetActive(true);
                _block3_2.SetActive(true);
                Small_city.SetActive(false);
                Small_city1.SetActive(false);
                Small_city2.SetActive(false);
                Small_city3.SetActive(false);
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
            Invoke("Latency",0.5f);
        }
    }

    void Latency()
    {
        
        people.SetActive(false);
        _crossroad1.SetActive(true);
        _crossroad2.SetActive(true);
        _crossroad3.SetActive(false);
        _crossroad4.SetActive(false);
        _crossroad5.SetActive(false);
        
        if (SceneManager.GetActiveScene().name == "city_single")
        {
            _main.SetActive(true);
            _block3_1.SetActive(false);
            _block3_2.SetActive(false);
            Small_city.SetActive(false);
            Small_city1.SetActive(true);
            Small_city2.SetActive(true);
            Small_city3.SetActive(true);
            block1.SetActive(false);
            block2.SetActive(false);
            block3.SetActive(false);
        }
    }
}
