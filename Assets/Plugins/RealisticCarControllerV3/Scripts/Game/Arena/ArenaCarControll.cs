using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCarControll : MonoBehaviour
{
    private GameObject PlayerCarControl;
    private GameObject _carAiControl;


    private void Start()
    {
        PlayerCarControl = GameObject.FindGameObjectWithTag("Player");
        //_carAiControl = GameObject.FindGameObjectWithTag("CarAI");
        PlayerCarControl.GetComponentInChildren<RCC_CarControllerV3>().enabled = true;
        //_carAiControl.GetComponent<RCC_CarControllerV3>().enabled = true;
        //_carAiControl.GetComponent<RCC_AICarController>().enabled = true;
        ArenaManager.manage.cars[0].GetComponent<RCC_CarControllerV3>().enabled = true;
        ArenaManager.manage.cars[1].GetComponent<RCC_CarControllerV3>().enabled = true;
        ArenaManager.manage.cars[0].GetComponent<RCC_AICarController>().enabled = true;
        ArenaManager.manage.cars[1].GetComponent<RCC_AICarController>().enabled = true;
    }
}
