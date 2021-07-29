using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manage;
    public GameObject Crashed;
    public GameObject CarDisplayPanel;

    private void Awake()
    {
        manage = this;
    }

    private void Update()
    {


    }

    private void Start()
    {
        Crashed.SetActive(false);
        CarDisplayPanel.SetActive(true);
        //fog
        //RenderSettings.fog = true;
        //RenderSettings.fogColor = new Color(1, 0.7424529f, 0.504717f);
        //RenderSettings.fogMode = FogMode.Exponential;
        //RenderSettings.fogDensity = 0.002f;


    }

    public void Lose()
    {
        Invoke("LatencyLose", 1f);
    }

    void LatencyLose()
    {
        Crashed.SetActive(true);
        
    }
}
