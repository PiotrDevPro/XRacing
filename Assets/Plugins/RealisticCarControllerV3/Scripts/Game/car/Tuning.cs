using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tuning : MonoBehaviour
{
    public GameObject CustomizeEnginePanel;
    public GameObject ColorPickerPanel;
    public GameObject ColorActive, ColorActive1, ColorActive2;
    public GameObject CarConfig;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _engine;
    [SerializeField] private GameObject _color;
    public GameObject _bodycolor;
    public GameObject _wheelcolor;
    public GameObject _detailcolor;
    public Image color1;
    public Image color2;
    public Image color3;
    public Image color4;
    public Image color5;
    public Image color6;
    public Image color7;
    public Image color8;
    public Image color9;
    void Start()
    {
        CustomizeEnginePanel.SetActive(false);
        CarConfig.SetActive(false);
        //color1.color = MainMenuManager.manage.carSetting[PlayerPrefs.GetInt("CurrentCar")].Colors[0];
        //print(MainMenuManager.manage.carSetting[PlayerPrefs.GetInt("CurrentCar")].Colors[0]);
    }

    public void OnClick()
    {
        CustomizeEnginePanel.SetActive(true);
        ColorPickerPanel.SetActive(false);
        CarConfig.SetActive(false);
        //button animation
        Amplitude.Instance.logEvent("Tuning->Engine");
        _body.GetComponent<Animator>().SetBool("push", false);
        _engine.GetComponent<Animator>().SetBool("push", true);
        _color.GetComponent<Animator>().SetBool("push", false);

    }

    public void onClickCarConfig()
    {
        CarConfig.SetActive(true);
        ColorPickerPanel.SetActive(false);
        CustomizeEnginePanel.SetActive(false);
        Amplitude.Instance.logEvent("Tuning->CarBody");
        //button animation
        _body.GetComponent<Animator>().SetBool("push", true);
        _engine.GetComponent<Animator>().SetBool("push", false);
        _color.GetComponent<Animator>().SetBool("push", false);

    }

    public void onClickColor()
    {
        CustomizeEnginePanel.SetActive(false);
        ColorPickerPanel.SetActive(true);
        MainMenuManager.manage.svChecked.isCheckBody = true;
        ColorActive.SetActive(true);
        ColorActive1.SetActive(false);
        ColorActive2.SetActive(false);
        CarConfig.SetActive(false);
        Amplitude.Instance.logEvent("Tuning->Color");
        //button animation
        _body.GetComponent<Animator>().SetBool("push", false);
        _engine.GetComponent<Animator>().SetBool("push", false);
        _color.GetComponent<Animator>().SetBool("push", true);
        _bodycolor.GetComponent<Animator>().SetBool("push", true);

    }

    public void OnClickEscape()
    {
        CustomizeEnginePanel.SetActive(false);
        ColorPickerPanel.SetActive(false);
        CarConfig.SetActive(false);
      //  ColorPickerActive.SetActive(false);
    }



}
