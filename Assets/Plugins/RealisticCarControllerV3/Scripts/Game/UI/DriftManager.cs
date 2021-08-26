using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DriftManager : MonoBehaviour
{
    //private bool d_AutoTargetPlayer = true;
    public GameObject DriftText;
    //public GameObject DriftCoinBouns;
    private GameObject playerCar;
    public bool canDrift;

    public Text txDriftAmount;
    public Text txDriftX;
    public Text txDriftScore;
    public Text txDriftHighScore;
    public Text txDriftCoins;
    public Text txDriftCoinsBonus;
    private float driftAmount;
    private float _driftScore;
    private float driftHighScore;
    private float driftScore;
    private float totalDriftScore;
    private float driftCoins;
    private float timerDrift = 10.0f;
    int count = 0;
    int countDrift = 0;
    void Start()
    {
        //playerCar = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        count += 1;
        if (count == 1)
        {
            playerCar = GameObject.FindGameObjectWithTag("Player");
        }

    }

    void FixedUpdate()
    {
        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 45f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            txDriftX.GetComponent<Animator>().StartPlayback();
            driftAmount += 0.1f;
            driftScore += 0.1f;
            txDriftX.text = "1X";
            txDriftX.color = Color.red;

            if (driftAmount > 10)
            {
                countDrift = 0;
                driftScore += 0.1f * 2;
                driftAmount += 0.1f * 2;
                txDriftX.text = "2X";
                txDriftX.color = Color.green;
                txDriftX.GetComponent<Animator>().StopPlayback();
            }

            if (driftAmount > 100)
            {
                driftScore += 0.1f * 3;
                driftAmount += 0.1f * 3;
                //driftCoins += 0.2f;
                txDriftX.text = "3X";
                txDriftX.color = Color.cyan;
                txDriftX.GetComponent<Animator>().StopPlayback();
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 0.1f);

            }

            if (driftAmount > 700)
            {
                driftScore += 0.2f * 4;
                driftAmount += 0.2f * 4;
                //driftCoins += 0.3f;
                txDriftX.text = "4X";
                txDriftX.color = Color.magenta;
                txDriftX.GetComponent<Animator>().StopPlayback();
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 0.15f);
            }

            if (driftAmount > 2000)
            {
                driftScore += 0.3f * 5;
                driftAmount += 0.3f * 5;
                txDriftX.text = "5X";
                txDriftX.color = Color.yellow;
                txDriftX.GetComponent<Animator>().StopPlayback();
                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 0.2f);
            }

            if (driftAmount > 5000)
            {
                driftScore += 1.0f * 6;
                driftAmount += 1.0f * 6;
                txDriftX.text = "6X";
                txDriftX.color = Color.white;
                txDriftX.GetComponent<Animator>().StopPlayback();
                

                PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 0.3f);
            }

            txDriftAmount.text = ((int)driftAmount).ToString();
            DriftText.SetActive(true);
            //timerDrift = Mathf.MoveTowards(timerDrift, 0.0f, Time.deltaTime);
        }
        else
        {
            if (driftAmount > 3000)
            {
                PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            }
            if (driftAmount > 5000)
            {

                PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 5);
            }
            if (driftAmount > 15000)
            {

                PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 7);
            }

            if (driftAmount > 35000)
            {

                PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 10);
            }

            if (driftAmount > 100000)
            {

                PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 30);
            }
            if (driftAmount > 200000)
            {

                PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 50);
            }
            DriftText.SetActive(false);
            txDriftScore.text = ((int)driftScore).ToString();
            driftAmount = 0;
            txDriftCoins.text = ((int)PlayerPrefs.GetFloat("DriftCoin")).ToString();

            if ((SaveManager.drifthigh) < (int)driftScore)
            {
                SaveManager.drifthigh = (int)driftScore;
                SaveManager.UpdateDriftHighScore();
                
                txDriftHighScore.text = ((int)SaveManager.drifthigh).ToString() ;
                txDriftCoins.text = ((int)PlayerPrefs.GetFloat("DriftCoin")).ToString() + "$";
            }
        }


        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 55f && playerCar.GetComponentInChildren<RCC_CarControllerV3>().driftingNow)
        {
            txDriftX.GetComponent<Animator>().StartPlayback();
            driftAmount += 0.6f;
            driftScore += 0.6f;
            txDriftX.text = "1X";
            txDriftX.color = Color.red;

            if (driftAmount > 100)
            {
                driftScore += 0.6f * 2;
                driftAmount += 0.6f * 2;
                txDriftX.text = "2X";
                txDriftX.color = Color.green;
                txDriftX.GetComponent<Animator>().StopPlayback();
            
                if (driftAmount > 250)
                {
                    driftScore += 0.6f * 3;
                    driftAmount += 0.6f * 3;
                    //driftCoins += 0.3f;
                    txDriftX.text = "3X";
                    txDriftX.color = Color.cyan;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                    //SaveManager.driftcoin += 0.1f;
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin")+0.1f);
                   // SaveManager.UpdateDriftCoin();
                }


                if (driftAmount > 950)
                {
                    driftScore += 0.6f * 4;
                    driftAmount += 0.6f * 4;
                    txDriftX.text = "4X";
                    txDriftX.color = Color.magenta;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 0.15f);
                }

                if (driftAmount > 2150)
                {
                    driftScore += 0.6f * 5;
                    driftAmount += 0.6f * 5;
                    txDriftX.text = "5X";
                    txDriftX.color = Color.magenta;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 0.3f);
                }

                if (driftAmount > 7000)
                {
                    driftScore += 1.2f * 6;
                    driftAmount += 1.2f * 6;
                    txDriftX.text = "6X";
                    txDriftX.color = Color.white;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                    PlayerPrefs.SetFloat("DriftCoin", PlayerPrefs.GetFloat("DriftCoin") + 0.5f);
                }
            }
            txDriftAmount.text = ((int)driftAmount).ToString();
            DriftText.SetActive(true);

        }

        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 80f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            txDriftX.GetComponent<Animator>().StartPlayback();
            driftAmount += 1.2f;
            driftScore += 1.2f;

            if (driftAmount > 400)
            {
                driftScore += 2.2f * 2;
                driftAmount += 2.2f * 2;
                txDriftX.text = "2X";
                txDriftX.color = Color.green;
                txDriftX.GetComponent<Animator>().StopPlayback();

                if (driftAmount > 600)
                {
                    driftScore += 2.2f * 3;
                    driftAmount += 2.2f * 3;
                    txDriftX.text = "3X";
                    txDriftX.color = Color.cyan;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 1000)
                {
                    driftScore += 2.2f * 4;
                    driftAmount += 2.2f * 4;
                    txDriftX.text = "4X";
                    txDriftX.color = Color.magenta;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 2000)
                {
                    driftScore += 2.2f * 5;
                    driftAmount += 2.2f * 5;
                    txDriftX.text = "5X";
                    txDriftX.color = Color.yellow;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 8700)
                {
                    driftScore += 2.2f * 6;
                    driftAmount += 2.2f * 6;
                    txDriftX.text = "6X";
                    txDriftX.color = Color.yellow;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }
            }
            txDriftAmount.text = ((int)driftAmount).ToString();
            DriftText.SetActive(true);
        }

        if (playerCar.GetComponent<RCC_CarControllerV3>().speed > 180f && playerCar.GetComponent<RCC_CarControllerV3>().driftingNow)
        {
            txDriftX.GetComponent<Animator>().StartPlayback();
            driftAmount += 3.8f;
            driftScore += 3.8f;

            if (driftAmount > 1000)
            {
                driftScore += 3.2f * 2;
                driftAmount += 3.2f * 2;
                txDriftX.text = "2X";
                txDriftX.color = Color.green;
                txDriftX.GetComponent<Animator>().StopPlayback();

                if (driftAmount > 2000)
                {
                    driftScore += 3.2f * 3;
                    driftAmount += 3.2f * 3;
                    txDriftX.text = "3X";
                    txDriftX.color = Color.cyan;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 2500)
                {
                    driftScore += 3.2f * 4;
                    driftAmount += 3.2f * 4;
                    txDriftX.text = "4X";
                    txDriftX.color = Color.magenta;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 3500)
                {
                    driftScore += 3.2f * 5;
                    driftAmount += 3.2f * 5;
                    txDriftX.text = "5X";
                    txDriftX.color = Color.yellow;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 6500)
                {
                    driftScore += 3.2f * 6;
                    driftAmount += 3.2f * 6;
                    txDriftX.text = "6X";
                    txDriftX.color = Color.white;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }
            }
            txDriftAmount.text = ((int)driftAmount).ToString();
            DriftText.SetActive(true);

        }

        if (playerCar.GetComponentInChildren<RCC_CarControllerV3>().speed > 230f && playerCar.GetComponentInChildren<RCC_CarControllerV3>().driftingNow)
        {
            txDriftX.GetComponent<Animator>().StartPlayback();
            driftAmount += 5.8f;
            driftScore += 5.8f;

            if (driftAmount > 2000)
            {
                driftScore += 4.2f * 2;
                driftAmount += 4.2f * 2;
                txDriftX.text = "2X";
                txDriftX.color = Color.green;
                txDriftX.GetComponent<Animator>().StopPlayback();

                if (driftAmount > 3000)
                {
                    driftScore += 4.2f * 3;
                    driftAmount += 4.2f * 3;
                    txDriftX.text = "3X";
                    txDriftX.color = Color.cyan;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 4000)
                {
                    driftScore += 4.2f * 4;
                    driftAmount += 4.2f * 4;
                    txDriftX.text = "4X";
                    txDriftX.color = Color.magenta;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 5500)
                {
                    driftScore += 4.2f * 5;
                    driftAmount += 4.2f * 5;
                    txDriftX.text = "5X";
                    txDriftX.color = Color.yellow;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }

                if (driftAmount > 7500)
                {
                    driftScore += 4.6f * 6;
                    driftAmount += 4.6f * 6;
                    txDriftX.text = "6X";
                    txDriftX.color = Color.white;
                    txDriftX.GetComponent<Animator>().StopPlayback();
                }
            }
            txDriftAmount.text = ((int)driftAmount).ToString();
            DriftText.SetActive(true);
        }

    }
}

        


