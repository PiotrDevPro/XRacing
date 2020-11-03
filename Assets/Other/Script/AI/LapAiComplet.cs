using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapAiComplet : MonoBehaviour
{
    public GameObject finishPoint;
    public GameObject halfPoint;

    public GameObject MinDisplay;
    public GameObject SecDisplay;
    public GameObject MilliDisplay;

   //public GameObject LapCounter;
    public int LapsDone;


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "CarAI")
        {
            SaveManager.laps += 1;
            SaveManager.UpdateLaps();
            //LapsDone += 1;

            if (SaveManager.laps == 1)
            {

                if (LapTimeManagerAI.SecondCount <= 9)

                {

                    SecDisplay.GetComponent<Text>().text = "0" + LapTimeManagerAI.SecondCount + ".";

                }
                else
                {
                    SecDisplay.GetComponent<Text>().text = "" + LapTimeManagerAI.SecondCount + ".";

                }

                if (LapTimeManagerAI.MinuteCount <= 9)

                {

                    MinDisplay.GetComponent<Text>().text = "0" + LapTimeManagerAI.MinuteCount + ":";

                }
                else
                {
                    MinDisplay.GetComponent<Text>().text = "" + LapTimeManagerAI.MinuteCount + ":";

                }

                SaveManager.min = LapTimeManagerAI.MinuteCount;
                SaveManager.UpdateMin();
                SaveManager.sec = LapTimeManagerAI.SecondCount;
                SaveManager.UpdateSec();
                SaveManager.milli = LapTimeManagerAI.MilliCount;
                SaveManager.UpdateMilli();

                LapTimeManagerAI.MinuteCount = 0;
                LapTimeManagerAI.SecondCount = 0;
                LapTimeManagerAI.MilliCount = 0;

               // LapCounter.GetComponent<Text>().text = "" + LapsDone;

                halfPoint.SetActive(true);
                finishPoint.SetActive(false);
                
            }
            else
            {
                if ((SaveManager.sec) >= (LapTimeManagerAI.SecondCount) && (SaveManager.min) >= (LapTimeManagerAI.MinuteCount))

                {

                    if (LapTimeManagerAI.SecondCount <= 9)

                    {

                        SecDisplay.GetComponent<Text>().text = "0" + LapTimeManagerAI.SecondCount + ".";

                    }
                    else
                    {
                        SecDisplay.GetComponent<Text>().text = "" + LapTimeManagerAI.SecondCount + ".";

                    }

                    if (LapTimeManagerAI.MinuteCount <= 9)

                    {

                        MinDisplay.GetComponent<Text>().text = "0" + LapTimeManagerAI.MinuteCount + ":";

                    }
                    else
                    {
                        MinDisplay.GetComponent<Text>().text = "" + LapTimeManagerAI.MinuteCount + ":";

                    }


                    SaveManager.min = LapTimeManagerAI.MinuteCount;
                    SaveManager.UpdateMin();
                    SaveManager.sec = LapTimeManagerAI.SecondCount;
                    SaveManager.UpdateSec();
                    SaveManager.milli = LapTimeManagerAI.MilliCount;
                    SaveManager.UpdateMilli();
                }
                LapTimeManagerAI.MinuteCount = 0;
                LapTimeManagerAI.SecondCount = 0;
                LapTimeManagerAI.MilliCount = 0;

                //LapCounter.GetComponent<Text>().text = "" + LapsDone;

                halfPoint.SetActive(true);
                finishPoint.SetActive(false);

            }
        }
    }
}
