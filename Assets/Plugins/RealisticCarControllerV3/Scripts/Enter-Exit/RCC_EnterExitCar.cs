//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using Photon.Pun;
using System.Collections;

public class RCC_EnterExitCar : MonoBehaviourPunCallbacks
{
	public static RCC_EnterExitCar manage;
	private RCC_CarControllerV3 carController;
	private GameObject carCamera;
	private GameObject player;
	private GameObject dashboard;
	private GameObject OutPos;
	public Transform getOutPosition;
	private Transform getOutPos;


	public bool isPlayerIn = false;
	private bool  opened = false;
	private float waitTime = 1f;
	private bool  temp = false;

	[Header("Camera")]
	public GameObject mainCam;

	
	void Awake (){
		manage = this;
		carController = GetComponent<RCC_CarControllerV3>();
		carCamera = GameObject.FindObjectOfType<RCC_Camera>().gameObject;
		if (!getOutPos)
        {
			GameObject geto = new GameObject("Get Out From Car");
			geto.transform.SetParent(transform);
			geto.transform.localPosition = new Vector3(-3f, 0f, 0f);
			geto.AddComponent<BoxCollider>().tag = "inOut";
			geto.GetComponent<BoxCollider>().isTrigger = true;
			getOutPos = geto.transform;

		}
			
		
		if (!getOutPosition){
			GameObject getOutPos = new GameObject("Get Out Position");
			getOutPos.transform.SetParent(transform);
			getOutPos.transform.localPosition = new Vector3(-3f, 0f, 0f);
			getOutPos.transform.localRotation = Quaternion.identity;
			getOutPosition = getOutPos.transform;

		}

	}

	void Start(){
		
		//if(dashboard)
		//	StartCoroutine("DisableDashboard");
		isPlayerIn = true;

	}

	IEnumerator DisableDashboard(){

		yield return new WaitForEndOfFrame();
		dashboard.SetActive(false);

	}
	
	void Update (){




		if ((RCC_Settings.Instance.controllerType == RCC_Settings.ControllerType.Keyboard && ControlFreak2.CF2Input.GetKeyDown(RCC_Settings.Instance.enterExitVehicleKB)) && opened && !temp){
			GetOut();
			opened = false;
			temp = false;
		}

        if (isPlayerIn)
        {
				carController.canControl = true;
			
		}

		else
			carController.canControl = false;

	}
	
	IEnumerator Act (GameObject fpsplayer){
		
		player = fpsplayer;

		if (!opened && !temp){
			GetIn();
			opened = true;
			temp = true;
			yield return new WaitForSeconds(waitTime);
			temp = false;
		}

	}
	
	void GetIn (){

		isPlayerIn = true;
		InsideOutsideCar.manage.fixThaCam();
		SickscoreGames.HUDNavigationSystem.HUDNavigationSystem.Instance.PlayerCamera = InsideOutsideCar.manage.mainCam.GetComponentInChildren<Camera>();
		InsideOutsideCar.manage.mainCam.localPosition = new Vector3(0, 0, 0);
		InsideOutsideCar.manage.mainCam.localEulerAngles = new Vector3(0, 0, 0);
		
		carCamera.SetActive(true);
		//
		InsideOutsideCar.manage.enter_panel.SetActive(false);
		InsideOutsideCar.manage.carInput.SetActive(true);
		InsideOutsideCar.manage._UI.SetActive(true);
		InsideOutsideCar.manage.Rcc_canvas.SetActive(true);
		InsideOutsideCar.manage.characterInput.SetActive(false);
		if (carCamera.GetComponent<RCC_Camera>()){
			carCamera.GetComponent<RCC_Camera>().cameraSwitchCount = 10;
			carCamera.GetComponent<RCC_Camera>().ChangeCamera();
		}
		Amplitude.Instance.logEvent("EnterToCar");
		carCamera.transform.GetComponent<RCC_Camera>().SetPlayerCar(gameObject);
		player.transform.SetParent(transform);
		player.transform.localPosition = Vector3.zero;
		player.transform.localRotation = Quaternion.identity;
		player.SetActive(false);
		GetComponent<RCC_CarControllerV3>().canControl = true;
		GetComponent<RCC_CarControllerV3>().GetComponentInChildren<BoxCollider>().tag = "Player";
		if (!GetComponent<RCC_CarControllerV3>().engineRunning)
				SendMessage ("StartEngine");
	}
	
	public void GetOut (){


		isPlayerIn = false;
		SickscoreGames.HUDNavigationSystem.HUDNavigationSystem.Instance.PlayerCamera = UnityStandardAssets.Characters.FirstPerson.FirstPersonController.manage.m_Camera;
		player.transform.SetParent(null);
		
		player.transform.position = getOutPosition.position;
		player.transform.rotation = getOutPosition.rotation;
		player.transform.rotation = Quaternion.Euler(0f, player.transform.eulerAngles.y, 0f);
		carCamera.SetActive(false);
		player.SetActive(true);
		GetComponent<RCC_CarControllerV3>().canControl = false;
		if(!RCC_Settings.Instance.keepEnginesAlive)
			GetComponent<RCC_CarControllerV3>().engineRunning = false;

	}
	
}