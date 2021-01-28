using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager manage;
    public GameObject Crashed;

    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        Crashed.SetActive(false);
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
