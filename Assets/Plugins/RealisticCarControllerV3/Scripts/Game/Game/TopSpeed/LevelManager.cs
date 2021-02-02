using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    private void Start()
    {
        Crashed.SetActive(false);
        CarDisplayPanel.SetActive(true);
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
