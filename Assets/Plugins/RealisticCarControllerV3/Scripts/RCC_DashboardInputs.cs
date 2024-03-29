﻿//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Dashboard Inputs")]
public class RCC_DashboardInputs : MonoBehaviour {

	public static RCC_DashboardInputs manage;
	public RCC_CarControllerV3 currentCarController;
	private PhotonView photonView;
	public GameObject RPMNeedle;
	public GameObject KMHNeedle;
	public GameObject turboGauge;
	public GameObject NOSGauge;
	public GameObject BoostNeedle;
	public GameObject NoSNeedle;
	public GameObject infoPanel;
	public GameObject infoPanelAboutPeople;
	[SerializeField] GameObject reminderPanel;

	private float RPMNeedleRotation = 0f;
	private float KMHNeedleRotation = 0f;
	private float BoostNeedleRotation = 0f;
	private float NoSNeedleRotation = 0f;

	public float RPM;
	public float KMH;
	internal int direction = 1;
	internal float Gear;
	internal bool NGear = false;

	internal bool ABS = false;
	internal bool ESP = false;
	internal bool Park = false;
	internal bool Headlights = false;
	internal RCC_CarControllerV3.IndicatorsOn indicators;

    private void Awake()
    {
		manage = this;

	}

    void Update(){

		if(RCC_Settings.Instance.uiType == RCC_Settings.UIType.None){
			gameObject.SetActive(false);
			enabled = false;
			return;
		}

		GetValues();
	}
	

	void GetValues(){


		if (!RCC_SceneManager.Instance.activePlayerVehicle)
			return;

		if(!RCC_SceneManager.Instance.activePlayerVehicle.canControl || RCC_SceneManager.Instance.activePlayerVehicle.AIController)
		{
			return;
		}

		if(NOSGauge){
			if(RCC_SceneManager.Instance.activePlayerVehicle.useNOS){
				if(!NOSGauge.activeSelf)
					NOSGauge.SetActive(true);
			}else{
				if(NOSGauge.activeSelf)
					NOSGauge.SetActive(false);
			}
		}

		if(turboGauge){
			if(RCC_SceneManager.Instance.activePlayerVehicle.useTurbo){
				if(!turboGauge.activeSelf)
					turboGauge.SetActive(true);
			}else{
				if(turboGauge.activeSelf)
					turboGauge.SetActive(false);
			}
		}
		
		RPM = RCC_SceneManager.Instance.activePlayerVehicle.engineRPM;
		KMH = RCC_SceneManager.Instance.activePlayerVehicle.speed;
		direction = RCC_SceneManager.Instance.activePlayerVehicle.direction;
		Gear = RCC_SceneManager.Instance.activePlayerVehicle.currentGear;

		NGear = RCC_SceneManager.Instance.activePlayerVehicle.changingGear;
		
		ABS = RCC_SceneManager.Instance.activePlayerVehicle.ABSAct;
		ESP = RCC_SceneManager.Instance.activePlayerVehicle.ESPAct;
		Park = RCC_SceneManager.Instance.activePlayerVehicle.handbrakeInput > .1f ? true : false;
		Headlights = RCC_SceneManager.Instance.activePlayerVehicle.lowBeamHeadLightsOn || RCC_SceneManager.Instance.activePlayerVehicle.highBeamHeadLightsOn;
		indicators = RCC_SceneManager.Instance.activePlayerVehicle.indicatorsOn;

		if(RPMNeedle){
			RPMNeedleRotation = (RCC_SceneManager.Instance.activePlayerVehicle.engineRPM / 50f);
			RPMNeedle.transform.eulerAngles = new Vector3(RPMNeedle.transform.eulerAngles.x ,RPMNeedle.transform.eulerAngles.y, -RPMNeedleRotation);
		}
		if(KMHNeedle){
			if(RCC_Settings.Instance.units == RCC_Settings.Units.KMH)
				KMHNeedleRotation = (RCC_SceneManager.Instance.activePlayerVehicle.speed);
			else
				KMHNeedleRotation = (RCC_SceneManager.Instance.activePlayerVehicle.speed * 0.62f);
			KMHNeedle.transform.eulerAngles = new Vector3(KMHNeedle.transform.eulerAngles.x ,KMHNeedle.transform.eulerAngles.y, -KMHNeedleRotation);
		}
		if(BoostNeedle){
			BoostNeedleRotation = (RCC_SceneManager.Instance.activePlayerVehicle.turboBoost / 30f) * 270f;
			BoostNeedle.transform.eulerAngles = new Vector3(BoostNeedle.transform.eulerAngles.x ,BoostNeedle.transform.eulerAngles.y, -BoostNeedleRotation);
		}
		if(NoSNeedle){
			NoSNeedleRotation = (RCC_SceneManager.Instance.activePlayerVehicle.NoS / 100f) * 270f;
			NoSNeedle.transform.eulerAngles = new Vector3(NoSNeedle.transform.eulerAngles.x ,NoSNeedle.transform.eulerAngles.y, -NoSNeedleRotation);
		}
			
	}

	public void ResetCarForCoinzSinglePlayer()
    {
		if (RCC_EnterExitCar.manage.isPlayerIn)
		{
			RCC_CarControllerV3.manage.ResetCarForCoinCity();
			DamagePartsHood.manage.wheels1.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels2.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels1col.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels2col.gameObject.SetActive(true);
			DamagePartsHood.manage.blow.SetActive(false);
			DamagePartsHood.manage.point = 50;
			DPTrunk.manage.wheels1.gameObject.SetActive(true);
			DPTrunk.manage.wheels2.gameObject.SetActive(true);
			DPTrunk.manage.wheels1col.gameObject.SetActive(true);
			DPTrunk.manage.wheels2col.gameObject.SetActive(true);
			DPTrunk.manage.Blow.SetActive(false);
			DPTrunk.manage.point = 50;
			if (PlayerPrefs.GetInt("CurrentCar") == 10)
			{
				DPTrunkTwo.manage.wheels1.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels2.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels1col.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels2col.gameObject.SetActive(true);
				DPTrunkTwo.manage.point = 50;
			}
			infoPanel.SetActive(true);
			if (Application.systemLanguage != SystemLanguage.Russian)
			{
				infoPanel.GetComponentInChildren<Text>().text = "Repaired";
			}
			else
			{
				infoPanel.GetComponentInChildren<Text>().text = "Восстановлен";
			}
			GameObject checkpointSound = GameObject.Find("checkpointSnd");
			checkpointSound.GetComponent<AudioSource>().Play();
			Pause.manage.PausePanel.SetActive(false);
			Pause.manage.PausePanelSingle.SetActive(false);
			Time.timeScale = 1f;
			AudioListener.pause = false;
			Amplitude.Instance.logEvent("ResetCarForCoinzCitySingle");
			Invoke("latency", 0.9f);
		}
	}

	public void ResetCarForCoinzArena()
	{
		if (RCC_EnterExitCar.manage.isPlayerIn)
		{
			RCC_CarControllerV3.manage.ResetCarArena();
			DamagePartsHood.manage.wheels1.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels2.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels1col.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels2col.gameObject.SetActive(true);
			DamagePartsHood.manage.blow.SetActive(false);
			DamagePartsHood.manage.point = 50;
			DPTrunk.manage.wheels1.gameObject.SetActive(true);
			DPTrunk.manage.wheels2.gameObject.SetActive(true);
			DPTrunk.manage.wheels1col.gameObject.SetActive(true);
			DPTrunk.manage.wheels2col.gameObject.SetActive(true);
			DPTrunk.manage.Blow.SetActive(false);
			DPTrunk.manage.point = 50;
			if (PlayerPrefs.GetInt("CurrentCar") == 10)
			{
				DPTrunkTwo.manage.wheels1.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels2.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels1col.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels2col.gameObject.SetActive(true);
				DPTrunkTwo.manage.point = 50;
			}
			infoPanel.SetActive(true);
			if (Application.systemLanguage != SystemLanguage.Russian)
			{
				infoPanel.GetComponentInChildren<Text>().text = "Repaired";
			}
			else
			{
				infoPanel.GetComponentInChildren<Text>().text = "Восстановлен";
			}
			GameObject checkpointSound = GameObject.Find("checkpointSnd");
			checkpointSound.GetComponent<AudioSource>().Play();
			Pause.manage.PausePanel.SetActive(false);
			Pause.manage.PausePanelSingle.SetActive(false);
			Time.timeScale = 1f;
			AudioListener.pause = false;
			Amplitude.Instance.logEvent("ResetCarForCoinzCitySingle");
			Invoke("latency", 0.9f);
		}
	}


	public void ResetCarForCoinz()
    {
		//if (RCC_EnterExitCar.manage.isPlayerIn)
        //{
			RCC_CarControllerV3.manage.ResetCarForCoin();
			DamagePartsHood.manage.wheels1.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels2.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels1col.gameObject.SetActive(true);
			DamagePartsHood.manage.wheels2col.gameObject.SetActive(true);
			DamagePartsHood.manage.blow.SetActive(false);
			DamagePartsHood.manage.point = 50;
			DPTrunk.manage.wheels1.gameObject.SetActive(true);
			DPTrunk.manage.wheels2.gameObject.SetActive(true);
			DPTrunk.manage.wheels1col.gameObject.SetActive(true);
			DPTrunk.manage.wheels2col.gameObject.SetActive(true);
			DPTrunk.manage.Blow.SetActive(false);
			DPTrunk.manage.point = 50;
			if (PlayerPrefs.GetInt("CurrentCar") == 10)
			{
				DPTrunkTwo.manage.wheels1.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels2.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels1col.gameObject.SetActive(true);
				DPTrunkTwo.manage.wheels2col.gameObject.SetActive(true);
				DPTrunkTwo.manage.point = 50;
			}
			infoPanel.SetActive(true);
			if (Application.systemLanguage != SystemLanguage.Russian)
			{
				infoPanel.GetComponentInChildren<Text>().text = "Repaired";
            }
            else
            {
				infoPanel.GetComponentInChildren<Text>().text = "Восстановлен";
			}
			GameObject checkpointSound = GameObject.Find("checkpointSnd");
			checkpointSound.GetComponent<AudioSource>().Play();
			Pause.manage.PausePanel.SetActive(false);
			Time.timeScale = 1f;
			AudioListener.pause = false;
			Amplitude.Instance.logEvent("ResetCarForCoinz");
			Invoke("latency", 0.9f);
		//}
		
	}

    private void latency()
    {
		infoPanel.SetActive(false);
	}

	public void PeoplePanel()
    {
		Invoke("latencyPeoplePanel",0.5f);
    }

	private void latencyPeoplePanel()
    {
		infoPanelAboutPeople.SetActive(false);
    }
}



