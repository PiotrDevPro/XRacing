using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public GameObject ColorActive, ColorActive1, ColorActive2;
    private Tuning _tuningScript;

    private void Start()
    {
        _tuningScript = FindObjectOfType<Tuning>();
    }

    public void BodySelect()
    {
        MainMenuManager.manage.svChecked.isCheckBody = true;
        MainMenuManager.manage.svChecked.isCheckWheels = false;
        MainMenuManager.manage.svChecked.isCheckDetail = false;
        ColorActive.SetActive(true);
        ColorActive1.SetActive(false);
        ColorActive2.SetActive(false);
        _tuningScript._bodycolor.GetComponent<Animator>().SetBool("push",true);
        _tuningScript._wheelcolor.GetComponent<Animator>().SetBool("push", false);
        _tuningScript._detailcolor.GetComponent<Animator>().SetBool("push", false);
        
    }
    public void WheelSelect()
    {
        MainMenuManager.manage.svChecked.isCheckBody = false;
        MainMenuManager.manage.svChecked.isCheckWheels = true;
        MainMenuManager.manage.svChecked.isCheckDetail = false;
        ColorActive.SetActive(false);
        ColorActive1.SetActive(true);
        ColorActive2.SetActive(false);
        _tuningScript._bodycolor.GetComponent<Animator>().SetBool("push", false);
        _tuningScript._wheelcolor.GetComponent<Animator>().SetBool("push", true);
        _tuningScript._detailcolor.GetComponent<Animator>().SetBool("push", false);

    }
    public void DetailSelect()
    {
        MainMenuManager.manage.svChecked.isCheckBody = false;
        MainMenuManager.manage.svChecked.isCheckWheels = false;
        MainMenuManager.manage.svChecked.isCheckDetail = true;
        ColorActive.SetActive(false);
        ColorActive1.SetActive(false);
        ColorActive2.SetActive(true);
        _tuningScript._bodycolor.GetComponent<Animator>().SetBool("push", false);
        _tuningScript._wheelcolor.GetComponent<Animator>().SetBool("push", false);
        _tuningScript._detailcolor.GetComponent<Animator>().SetBool("push", true);

    }
}
