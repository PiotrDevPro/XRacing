using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CarAi : MonoBehaviour
{
    public Text energy;
    int a = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MainMenuManager.manage.isAllvsYou)
            {
                if (GetComponent<RCC_CarControllerV3>().speed > 0)
                {
                    PlayerPrefs.SetInt("punch", a++);
                    energy.text = (100 - PlayerPrefs.GetInt("punch")).ToString();
                }
            }
        }
    }
}
