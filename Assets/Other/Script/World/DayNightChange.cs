using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DayNightChange : MonoBehaviour
{
    public float time;
    public TimeSpan currenttime;
    public Transform sunTransform;
    public Light sun;
    //public Light moon;
    public Text daynightText;
    public Text timetext;
    public int days;
    public float intensity;
    public Color fogday = Color.grey;
    public Color fognight = Color.blue;
    public int speed;

    public GameObject AIlightsOn;
    public GameObject AIlightsOn1;
    public GameObject AIlightsOn2;
    public GameObject AIlightsOn3;

    void Update()
    {
        ChangeTime();
    }

    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if (time > 86400)
        {
            days += 1;
            time = 0;
        }

        currenttime = TimeSpan.FromSeconds(time);
        string[] temptime = currenttime.ToString().Split(":"[0]);
        timetext.text = temptime[0] + ":" + temptime[1];
        sunTransform.rotation = Quaternion.Euler(new Vector3((time - 21600) / 86400 * 360, 0, 0));
        if (time < 43200) {
            intensity = 1.3f - (43200 - time) / 43200;
        }
        else 
            intensity = 1.3f - ((43200 - time) / 43200 * -1.3f);
            RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity);
            sun.intensity = intensity; 
        if (time > 19500 && time < 32000) {
            daynightText.text = ("Morning :");
            AIlightsOn.SetActive(true);
            AIlightsOn1.SetActive(true);
            AIlightsOn2.SetActive(true);
            AIlightsOn3.SetActive(true);
            
        }
        if (time > 32000 && time < 65000) {
            daynightText.text = ("Day :");
            AIlightsOn.SetActive(false);
            AIlightsOn1.SetActive(false);
            AIlightsOn2.SetActive(false);
            AIlightsOn3.SetActive(false);
            
        }
        if (time > 65000 && time < 82000)
        {
            daynightText.text = ("Evening :");
            AIlightsOn.SetActive(true);
            AIlightsOn1.SetActive(true);
            AIlightsOn2.SetActive(true);
            AIlightsOn3.SetActive(true);
            
        }

        if (time > 82000 && time < 86400)
        {
            daynightText.text = ("Midnight :");
            AIlightsOn.SetActive(true);
            AIlightsOn1.SetActive(true);
            AIlightsOn2.SetActive(true);
            AIlightsOn3.SetActive(true);
            
        }

        if (time > 0 && time < 19500)
        {
            daynightText.text = ("Night :");
            AIlightsOn.SetActive(true);
            AIlightsOn1.SetActive(true);
            AIlightsOn2.SetActive(true);
            AIlightsOn3.SetActive(true);
        }
    }    
}
