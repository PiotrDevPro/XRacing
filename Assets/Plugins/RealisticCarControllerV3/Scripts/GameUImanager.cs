using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameUImanager : MonoBehaviour
{

    public GameObject DriftText;
    public GameObject playerCar;

    public Text txDriftAmount;
    public Text txDriftX;
    public Text txDriftScore;
    public float driftAmount;
    public float _driftScore;



    void FixedUpdate()
    {
        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 20f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {

            driftAmount += Time.deltaTime * 12;
            txDriftAmount.text = ((int)driftAmount).ToString();
            Debug.Log("speed > 20f");
            DriftText.SetActive(true);

        }
        else
        {
            DriftText.SetActive(false);// }

            // if (DriftText.SetActive(false)) { 
            //_driftAmount = 0;
        }

        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 40f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            DriftText.SetActive(true);
            driftAmount += Time.deltaTime * 20;
            txDriftAmount.text = ((int)driftAmount).ToString();
            Debug.Log("speed > 40f");
        }

        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 60f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            DriftText.SetActive(true);
            driftAmount += Time.deltaTime * 35;
            txDriftAmount.text = ((int)driftAmount).ToString();
            Debug.Log("speed > 60f");

        }

        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 80f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            DriftText.SetActive(true);
            driftAmount += Time.deltaTime * 60;
            txDriftAmount.text = ((int)driftAmount).ToString();
            Debug.Log("speed > 80f");

        }

        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 100f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            DriftText.SetActive(true);
            driftAmount += Time.deltaTime * 85;
            txDriftAmount.text = ((int)driftAmount).ToString();
            Debug.Log("speed > 100f");
        }


        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 130f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            DriftText.SetActive(true);
            driftAmount += Time.deltaTime * 150;
            txDriftAmount.text = ((int)driftAmount).ToString();
            Debug.Log("speed > 130f");

        }

    }


}



    
    

    
