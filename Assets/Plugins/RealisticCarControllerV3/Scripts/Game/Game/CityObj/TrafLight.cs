using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TrafLight : MonoBehaviour
{
    [SerializeField] GameObject obj;
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name != "city_online" && RCC_EnterExitCar.manage.isPlayerIn)
            {
                Destroy(obj);
            }
            else if (SceneManager.GetActiveScene().name == "city_online")
            {
                Destroy(obj);
            }

        }

        if (col.gameObject.CompareTag("Player") && !RCC_EnterExitCar.manage.isPlayerIn)
        {
            if (SceneManager.GetActiveScene().name != "city_online" && !RCC_EnterExitCar.manage.isPlayerIn)
            {
                GetComponentInChildren<Rigidbody>().isKinematic = true;
                print("isPlayerFPSTraffLightDetectedTrue");
            }
            else if (SceneManager.GetActiveScene().name == "city_online")
            {
                print("isPlayerCarTraffLightDetectedTrue");
                Destroy(obj);
            }
        }
    }
}
