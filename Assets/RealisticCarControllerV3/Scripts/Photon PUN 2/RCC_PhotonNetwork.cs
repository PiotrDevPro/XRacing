//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2019 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using Photon;
using Photon.Pun;
using UnityEngine.SceneManagement;

/// <summary>
/// Streaming player input, or receiving data from server. And then feeds the RCC.
/// </summary>
[RequireComponent(typeof(PhotonView))]
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Network/Photon/RCC Photon Network")]
public class RCC_PhotonNetwork : Photon.Pun.MonoBehaviourPunCallbacks, IPunObservable {

	public static RCC_PhotonNetwork photonNetwork;
	public bool isMine = false;

	// Main RCC, Rigidbody, and Main RCC Camera of the scene. 
	private RCC_CarControllerV3 carController;
	private RCC_WheelCollider[] wheelColliders;
	private Rigidbody rigid;
	private GameObject _car_mirrors;

	// Vehicle position and rotation. Will send these to server.
	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;

	// Used for projected (interpolated) position.
	private Vector3 currentVelocity;
	private float updateTime = 0;

	// All inputs for RCC. We will send these values if we own this vehicle. If this vehicle is owned by other player, receives all these inputs from server.
	private float gasInput = 0f;
	private float brakeInput = 0f;
	private float steerInput = 0f;
	private float handbrakeInput = 0f;
	private float boostInput = 0f;
	private float clutchInput = 0f;
	private float idleInput = 0f;
	private int gear = 0;
	private int direction = 1;
	private bool changingGear = false;
	private bool semiAutomaticGear = false;
	//private float fuelInput = 1f;
	private bool engineRunning = false;

	private float[] cambers;
	private bool applyEngineTorqueToExtraRearWheelColliders;
	private RCC_CarControllerV3.WheelType _wheelTypeChoise;
	private float biasedWheelTorque;
	private bool canGoReverseNow;
	private float engineTorque;
	private float brakeTorque;
	private float minEngineRPM;
	private float maxEngineRPM;
	private float engineInertia;
	private bool useRevLimiter;
	private bool useExhaustFlame;
	//private bool useClutchMarginAtFirstGear;
	private float highspeedsteerAngle;
	private float highspeedsteerAngleAtspeed;
	private float antiRollFrontHorizontal;
	private float antiRollRearHorizontal;
	private float antiRollVertical;
	private float maxspeed;
	//private float engineHeat;
	//private float engineHeatMultiplier;
	private int totalGears;
	private float gearShiftingDelay;
	//private float gearShiftingThreshold;
	//private float clutchInertia;
	//private bool NGear;
	private float launched;
	private bool ABS;
	private float ABSThreshold;
	private bool TCS;
	private float TCSThreshold;
	private bool ESP;
	private float ESPThreshold;
	private bool steeringHelper;
	private float steerHelperLinearVelStrength;
	private float steerHelperAngularVelStrength;
	private bool tractionHelper;
	private float tractionHelperStrength;
	private bool applyCounterSteering;
	private bool useNOS;
	private bool useTurbo;

	// Lights.
	private bool lowBeamHeadLightsOn = false;
	private bool highBeamHeadLightsOn = false;

	// For Indicators.
	private RCC_CarControllerV3.IndicatorsOn indicatorsOn;

	// Customizations
	private float frontleftwheelSusp;
	private float frontRighttwheelSusp;
	private float RearleftwheelSusp;
	private float RearRightwheelSusp;

    private void Awake()
    {
		photonNetwork = this;

	}

    void Start()
	{
		if (SceneManager.GetActiveScene().name != "battle_online")
			return;
		// Getting RCC, Rigidbody. 
		carController = GetComponent<RCC_CarControllerV3>();
		wheelColliders = GetComponentsInChildren<RCC_WheelCollider>();
		cambers = new float[wheelColliders.Length];
		rigid = GetComponent<Rigidbody>();

		if (!gameObject.GetComponent<PhotonView>().ObservedComponents.Contains(this))
			gameObject.GetComponent<PhotonView>().ObservedComponents.Add(this);

		gameObject.GetComponent<PhotonView>().Synchronization = ViewSynchronization.Unreliable;

		GetValues();

		// If we are the owner of this vehicle, disable external controller and enable controller of the vehicle. Do opposite if we don't own this.
		if (photonView.IsMine)
		{

			carController.AIController = false;
			carController.canControl = true;

		}
		else
		{

			carController.AIController = true;
			carController.canControl = false;

		}

		// Setting name of the gameobject with Photon View ID.
		gameObject.name = gameObject.name + photonView.ViewID;

		//		PhotonNetwork.SendRate = 60;
		//		PhotonNetwork.SerializationRate = 60;

	}

	void GetValues()
	{

		correctPlayerPos = transform.position;
		correctPlayerRot = transform.rotation;

		gasInput = carController.gasInput;
		brakeInput = carController.brakeInput;
		steerInput = carController.steerInput;
		handbrakeInput = carController.handbrakeInput;
		boostInput = carController.boostInput;
		clutchInput = carController.clutchInput;
		idleInput = carController.idleInput;
		gear = carController.currentGear;
		direction = carController.direction;
		changingGear = carController.changingGear;
		semiAutomaticGear = carController.semiAutomaticGear;

		//fuelInput = carController.fuelInput;
		engineRunning = carController.engineRunning;
		lowBeamHeadLightsOn = carController.lowBeamHeadLightsOn;
		highBeamHeadLightsOn = carController.highBeamHeadLightsOn;
		indicatorsOn = carController.indicatorsOn;

		// Customization state
		frontleftwheelSusp = carController.FrontLeftWheelCollider.wheelCollider.suspensionDistance;
		frontRighttwheelSusp = carController.FrontRightWheelCollider.wheelCollider.suspensionDistance;
		RearleftwheelSusp = carController.RearLeftWheelCollider.wheelCollider.suspensionDistance;
		RearRightwheelSusp = carController.RearRightWheelCollider.wheelCollider.suspensionDistance;

		for (int i = 0; i < wheelColliders.Length; i++)
		{

			cambers[i] = wheelColliders[i].camber;

		}

		applyEngineTorqueToExtraRearWheelColliders = carController.applyEngineTorqueToExtraRearWheelColliders;
		_wheelTypeChoise = carController._wheelTypeChoise;
		biasedWheelTorque = carController.biasedWheelTorque;
		canGoReverseNow = carController.canGoReverseNow;
		engineTorque = carController.engineTorque;
		brakeTorque = carController.brakeTorque;
		minEngineRPM = carController.minEngineRPM;
		maxEngineRPM = carController.maxEngineRPM;
		engineInertia = carController.engineInertia;
		useRevLimiter = carController.useRevLimiter;
		useExhaustFlame = carController.useExhaustFlame;
		//useClutchMarginAtFirstGear = carController.useClutchMarginAtFirstGear;
		highspeedsteerAngle = carController.highspeedsteerAngle;
		highspeedsteerAngleAtspeed = carController.highspeedsteerAngleAtspeed;
		antiRollFrontHorizontal = carController.antiRollFrontHorizontal;
		antiRollRearHorizontal = carController.antiRollRearHorizontal;
		antiRollVertical = carController.antiRollVertical;
		maxspeed = carController.maxspeed;
		//engineHeat = carController.engineHeat;
		//engineHeatMultiplier = carController.engineHeatRate;
		totalGears = carController.totalGears;
		gearShiftingDelay = carController.gearShiftingDelay;
		//gearShiftingThreshold = carController.gearShiftingThreshold;
		//clutchInertia = carController.clutchInertia;
		//NGear = carController.NGear;
		launched = carController.launched;
		ABS = carController.ABS;
		TCS = carController.TCS;
		ESP = carController.ESP;
		TCSThreshold = carController.TCSThreshold;
		ESPThreshold = carController.ESPThreshold;
		ABSThreshold = carController.ABSThreshold;
		steeringHelper = carController.steeringHelper;
		steerHelperLinearVelStrength = carController.steerHelperLinearVelStrength;
		steerHelperAngularVelStrength = carController.steerHelperAngularVelStrength;
		tractionHelper = carController.tractionHelper;
		tractionHelperStrength = carController.tractionHelperStrength;
		applyCounterSteering = carController.applyCounterSteering;
		useNOS = carController.useNOS;
		useTurbo = carController.useTurbo;

	}

	void FixedUpdate()
	{

		if (!carController)
			return;

		isMine = photonView.IsMine;

		// If we are the owner of this vehicle, disable external controller and enable controller of the vehicle. Do opposite if we don't own this.
		carController.AIController = !isMine;
		carController.canControl = isMine;

		// If we are not owner of this vehicle, receive all inputs from server.
		if (!isMine)
		{

			Vector3 projectedPosition = this.correctPlayerPos + currentVelocity * (Time.time - updateTime);

			if (Vector3.Distance(transform.position, correctPlayerPos) < 15f)
			{

				transform.position = Vector3.Lerp(transform.position, projectedPosition, Time.deltaTime * 5f);
				transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5f);

			}
			else
			{

				transform.position = correctPlayerPos;
				transform.rotation = correctPlayerRot;

			}

			carController.gasInput = gasInput;
			carController.brakeInput = brakeInput;
			carController.steerInput = steerInput;
			carController.handbrakeInput = handbrakeInput;
			carController.boostInput = boostInput;
			carController.clutchInput = clutchInput;
			carController.idleInput = idleInput;
			carController.currentGear = gear;
			carController.direction = direction;
			carController.changingGear = changingGear;
			carController.semiAutomaticGear = semiAutomaticGear;
			carController.engineRunning = engineRunning;
			carController.lowBeamHeadLightsOn = lowBeamHeadLightsOn;
			carController.highBeamHeadLightsOn = highBeamHeadLightsOn;
			carController.indicatorsOn = indicatorsOn;
			// Customizations
			carController.FrontLeftWheelCollider.wheelCollider.suspensionDistance = frontleftwheelSusp;
			carController.FrontRightWheelCollider.wheelCollider.suspensionDistance = frontRighttwheelSusp;
			carController.RearLeftWheelCollider.wheelCollider.suspensionDistance = RearleftwheelSusp;
			carController.RearRightWheelCollider.wheelCollider.suspensionDistance = RearRightwheelSusp;

			for (int i = 0; i < wheelColliders.Length; i++)
			{

				wheelColliders[i].camber = cambers[i];

			}

			carController.applyEngineTorqueToExtraRearWheelColliders = applyEngineTorqueToExtraRearWheelColliders;
			carController._wheelTypeChoise = _wheelTypeChoise;
			carController.biasedWheelTorque = biasedWheelTorque;
			carController.canGoReverseNow = canGoReverseNow;
			carController.engineTorque = engineTorque;
			carController.brakeTorque = brakeTorque;
			carController.minEngineRPM = minEngineRPM;
			carController.maxEngineRPM = maxEngineRPM;
			carController.engineInertia = engineInertia;
			carController.useRevLimiter = useRevLimiter;
			carController.useExhaustFlame = useExhaustFlame;
			carController.highspeedsteerAngle = highspeedsteerAngle;
			carController.highspeedsteerAngleAtspeed = highspeedsteerAngleAtspeed;
			carController.antiRollFrontHorizontal = antiRollFrontHorizontal;
			carController.antiRollRearHorizontal = antiRollRearHorizontal;
			carController.antiRollVertical = antiRollVertical;
			carController.maxspeed = maxspeed;
			carController.totalGears = totalGears;
			carController.gearShiftingDelay = gearShiftingDelay;
			carController.launched = launched;
			carController.ABS = ABS;
			carController.ABSThreshold = ABSThreshold;
			carController.TCS = TCS;
			carController.TCSThreshold = TCSThreshold;
			carController.ESP = ESP;
			carController.ESPThreshold = ESPThreshold;
			carController.steeringHelper = steeringHelper;
			carController.steerHelperLinearVelStrength = steerHelperLinearVelStrength;
			carController.steerHelperAngularVelStrength = steerHelperAngularVelStrength;
			carController.tractionHelper = tractionHelper;
			carController.tractionHelperStrength = tractionHelperStrength;
			carController.applyCounterSteering = applyCounterSteering;
			carController.useNOS = useNOS;
			carController.useTurbo = useTurbo;



			//carController.fuelInput = fuelInput;
			//carController.gearShiftingThreshold = gearShiftingThreshold;
			//carController.clutchInertia = clutchInertia;
			//carController.NGear = NGear;
			//carController.engineHeat = engineHeat;
			//carController.engineHeatRate = engineHeatMultiplier;
			//carController.useClutchMarginAtFirstGear = useClutchMarginAtFirstGear;

		}

	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{

		if (!carController)
			return;

		// Sending all inputs, position, rotation, and velocity to the server.
		if (stream.IsWriting)
		{

			//We own this player: send the others our data
			stream.SendNext(carController.gasInput);
			stream.SendNext(carController.brakeInput);
			stream.SendNext(carController.steerInput);
			stream.SendNext(carController.handbrakeInput);
			stream.SendNext(carController.boostInput);
			stream.SendNext(carController.clutchInput);
			stream.SendNext(carController.idleInput);
			stream.SendNext(carController.currentGear);
			stream.SendNext(carController.direction);
			stream.SendNext(carController.changingGear);
			stream.SendNext(carController.semiAutomaticGear);
			//stream.SendNext(carController.fuelInput);
			stream.SendNext(carController.engineRunning);
			stream.SendNext(carController.lowBeamHeadLightsOn);
			stream.SendNext(carController.highBeamHeadLightsOn);
			stream.SendNext(carController.indicatorsOn);
			//Customizations
			stream.SendNext(carController.FrontLeftWheelCollider.wheelCollider.suspensionDistance);
			stream.SendNext(carController.FrontRightWheelCollider.wheelCollider.suspensionDistance);
			stream.SendNext(carController.RearLeftWheelCollider.wheelCollider.suspensionDistance);
			stream.SendNext(carController.RearRightWheelCollider.wheelCollider.suspensionDistance);

			for (int i = 0; i < wheelColliders.Length; i++)
			{

				stream.SendNext(wheelColliders[i].camber);

			}

			stream.SendNext(carController.applyEngineTorqueToExtraRearWheelColliders);
			stream.SendNext(carController._wheelTypeChoise);
			stream.SendNext(carController.biasedWheelTorque);
			stream.SendNext(carController.canGoReverseNow);
			stream.SendNext(carController.engineTorque);
			stream.SendNext(carController.brakeTorque);
			stream.SendNext(carController.minEngineRPM);
			stream.SendNext(carController.maxEngineRPM);
			stream.SendNext(carController.engineInertia);
			stream.SendNext(carController.useRevLimiter);
			stream.SendNext(carController.useExhaustFlame);
			//stream.SendNext(carController.useClutchMarginAtFirstGear);
			stream.SendNext(carController.highspeedsteerAngle);
			stream.SendNext(carController.highspeedsteerAngleAtspeed);
			stream.SendNext(carController.antiRollFrontHorizontal);
			stream.SendNext(carController.antiRollRearHorizontal);
			stream.SendNext(carController.antiRollVertical);
			stream.SendNext(carController.maxspeed);
			//stream.SendNext(carController.engineHeat);
			//stream.SendNext(carController.engineHeatRate);
			stream.SendNext(carController.totalGears);
			stream.SendNext(carController.gearShiftingDelay);
			//stream.SendNext(carController.gearShiftingThreshold);
			//stream.SendNext(carController.clutchInertia);
			///stream.SendNext(carController.NGear);
			stream.SendNext(carController.launched);
			stream.SendNext(carController.ABS);
			stream.SendNext(carController.ABSThreshold);
			stream.SendNext(carController.TCS);
			stream.SendNext(carController.TCSThreshold);
			stream.SendNext(carController.ESP);
			stream.SendNext(carController.ESPThreshold);
			stream.SendNext(carController.steeringHelper);
			stream.SendNext(carController.steerHelperLinearVelStrength);
			stream.SendNext(carController.steerHelperAngularVelStrength);
			stream.SendNext(carController.tractionHelper);
			stream.SendNext(carController.tractionHelperStrength);
			stream.SendNext(carController.applyCounterSteering);
			stream.SendNext(carController.useNOS);
			stream.SendNext(carController.useTurbo);

			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(rigid.velocity);

		}
		else
		{

			// Network player, receiving all inputs, position, rotation, and velocity from server. 
			gasInput = (float)stream.ReceiveNext();
			brakeInput = (float)stream.ReceiveNext();
			steerInput = (float)stream.ReceiveNext();
			handbrakeInput = (float)stream.ReceiveNext();
			boostInput = (float)stream.ReceiveNext();
			clutchInput = (float)stream.ReceiveNext();
			idleInput = (float)stream.ReceiveNext();
			gear = (int)stream.ReceiveNext();
			direction = (int)stream.ReceiveNext();
			changingGear = (bool)stream.ReceiveNext();
			semiAutomaticGear = (bool)stream.ReceiveNext();

			//fuelInput = (float)stream.ReceiveNext();
			engineRunning = (bool)stream.ReceiveNext();
			lowBeamHeadLightsOn = (bool)stream.ReceiveNext();
			highBeamHeadLightsOn = (bool)stream.ReceiveNext();
			indicatorsOn = (RCC_CarControllerV3.IndicatorsOn)stream.ReceiveNext();

			///Customizations
			frontleftwheelSusp = (float)stream.ReceiveNext();
			RearleftwheelSusp = (float)stream.ReceiveNext();
			frontRighttwheelSusp = (float)stream.ReceiveNext();
			RearRightwheelSusp = (float)stream.ReceiveNext();

			for (int i = 0; i < wheelColliders.Length; i++)
			{

				cambers[i] = (float)stream.ReceiveNext();

			}

			applyEngineTorqueToExtraRearWheelColliders = (bool)stream.ReceiveNext();
			_wheelTypeChoise = (RCC_CarControllerV3.WheelType)stream.ReceiveNext();
			biasedWheelTorque = (float)stream.ReceiveNext();
			canGoReverseNow = (bool)stream.ReceiveNext();
			engineTorque = (float)stream.ReceiveNext();
			brakeTorque = (float)stream.ReceiveNext();
			minEngineRPM = (float)stream.ReceiveNext();
			maxEngineRPM = (float)stream.ReceiveNext();
			engineInertia = (float)stream.ReceiveNext();
			useRevLimiter = (bool)stream.ReceiveNext();
			useExhaustFlame = (bool)stream.ReceiveNext();
			//useClutchMarginAtFirstGear = (bool)stream.ReceiveNext();
			highspeedsteerAngle = (float)stream.ReceiveNext();
			highspeedsteerAngleAtspeed = (float)stream.ReceiveNext();
			antiRollFrontHorizontal = (float)stream.ReceiveNext();
			antiRollRearHorizontal = (float)stream.ReceiveNext();
			antiRollVertical = (float)stream.ReceiveNext();
			maxspeed = (float)stream.ReceiveNext();
			//engineHeat = (float)stream.ReceiveNext();
			//engineHeatMultiplier = (float)stream.ReceiveNext();
			totalGears = (int)stream.ReceiveNext();
			gearShiftingDelay = (float)stream.ReceiveNext();
			//gearShiftingThreshold = (float)stream.ReceiveNext();
			//clutchInertia = (float)stream.ReceiveNext();
			//NGear = (bool)stream.ReceiveNext();
			launched = (float)stream.ReceiveNext();
			ABS = (bool)stream.ReceiveNext();
			ABSThreshold = (float)stream.ReceiveNext();
			TCS = (bool)stream.ReceiveNext();
			TCSThreshold = (float)stream.ReceiveNext();
			ESP = (bool)stream.ReceiveNext();
			ESPThreshold = (float)stream.ReceiveNext();
			steeringHelper = (bool)stream.ReceiveNext();
			steerHelperLinearVelStrength = (float)stream.ReceiveNext();
			steerHelperAngularVelStrength = (float)stream.ReceiveNext();
			tractionHelper = (bool)stream.ReceiveNext();
			tractionHelperStrength = (float)stream.ReceiveNext();
			applyCounterSteering = (bool)stream.ReceiveNext();
			useNOS = (bool)stream.ReceiveNext();
			useTurbo = (bool)stream.ReceiveNext();

			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
			currentVelocity = (Vector3)stream.ReceiveNext();

			updateTime = Time.time;

		}

	}

}
