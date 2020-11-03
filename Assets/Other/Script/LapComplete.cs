using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LapComplete : MonoBehaviour
{
    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;

    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;

    public GameObject LapCounter;
    public int LapsDone;

    private GameObject finishBoxCollider;

    private void Start()
    {
        finishBoxCollider = GameObject.Find("finish");
    }


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            LapsDone += 1;
            SaveManager.laps += 1;
            //SaveManager.UpdateLaps();

            if (SaveManager.laps == 1)
            {

                if (LapTimeManager.SecondCount <= 9)

                {

                    SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";

                }
                else
                {
                    SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";

                }

                if (LapTimeManager.MinuteCount <= 9)

                {

                    MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ":";

                }
                else
                {
                    MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ":";

                }

                SaveManager.min = LapTimeManager.MinuteCount;
                SaveManager.UpdateMin();
                SaveManager.sec = LapTimeManager.SecondCount;
                SaveManager.UpdateSec();
                SaveManager.milli = LapTimeManager.MilliCount;
                SaveManager.UpdateMilli();

                LapTimeManager.MinuteCount = 0;
                LapTimeManager.SecondCount = 0;
                LapTimeManager.MilliCount = 0;

                LapCounter.GetComponent<Text>().text = "" + LapsDone;
                finishBoxCollider.GetComponent<BoxCollider>().enabled = false;
                //HalfLapTrig.SetActive(true);
                //LapCompleteTrig.SetActive(false);
                //Debug.Log(SaveManager.laps);
            }
            else
            {
                if ((SaveManager.sec) >= (LapTimeManager.SecondCount) && (SaveManager.min) >= (LapTimeManager.MinuteCount))

                {

                    if (LapTimeManager.SecondCount <= 9)

                {

                    SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";

                }
                else
                {
                    SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";

                }

                if (LapTimeManager.MinuteCount <= 9)

                {

                    MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ":";

                }
                else
                {
                    MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ":";

                }

                
                    SaveManager.min = LapTimeManager.MinuteCount;
                    SaveManager.UpdateMin();
                    SaveManager.sec = LapTimeManager.SecondCount;
                    SaveManager.UpdateSec();
                    SaveManager.milli = LapTimeManager.MilliCount;
                    SaveManager.UpdateMilli();
                }
                LapTimeManager.MinuteCount = 0;
                LapTimeManager.SecondCount = 0;
                LapTimeManager.MilliCount = 0;

                LapCounter.GetComponent<Text>().text = "" + LapsDone;
                finishBoxCollider.GetComponent<BoxCollider>().enabled = false;
                //HalfLapTrig.SetActive(true);
                //LapCompleteTrig.SetActive(false);

            }
        }
    }
}
