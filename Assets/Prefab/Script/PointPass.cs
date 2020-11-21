using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PointPass : MonoBehaviour
{
    public static PointPass manage;
    private bool isCheck = false;

    public int count = 0;
    private int countPlayerEnter = 0;
    private int countPass = 0;

    private GameObject anim;
    private void Awake()
    {
        manage = this;
    }
    private void Start()
    {
        anim = GameObject.Find("Tm");
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
        }
    }
}
