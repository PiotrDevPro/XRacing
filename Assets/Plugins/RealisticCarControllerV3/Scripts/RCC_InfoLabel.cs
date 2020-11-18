//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2019 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles RCC Canvas dashboard elements.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/RCC UI Info Displayer")]
[RequireComponent (typeof(Text))]
public class RCC_InfoLabel : MonoBehaviour {

	#region singleton
	private static RCC_InfoLabel instance;
	public static RCC_InfoLabel Instance{

		get{

			if (instance == null) {

				if (GameObject.FindObjectOfType<RCC_InfoLabel> ())
					instance = GameObject.FindObjectOfType<RCC_InfoLabel> ();

			}

			return instance;

		}

	}
	#endregion

	private GameObject text;
	private float timer = 1f;

	void Start () {
		text = GameObject.Find("Info");
		text.GetComponent<Text>();
		text.GetComponent<Text>().enabled = false;

	}

	void Update(){

		if (timer < 1f) {
			
			if (!text.GetComponent<Text>().enabled)
				text.GetComponent<Text>().enabled = true;
			
		} else {
			
			if (text.GetComponent<Text>().enabled)
				text.GetComponent<Text>().enabled = false;
			
		}

		timer += Time.deltaTime;

	}

	public void ShowInfo (string info) {

		if (!text)
			return;

		text.GetComponent<Text>().text = info;
		timer = 0f;

//		StartCoroutine (ShowInfoCo(info, time));
		
	}

	IEnumerator ShowInfoCo(string info, float time){

		text.GetComponent<Text>().enabled = true;
		text.GetComponent<Text>().text = info;
		yield return new WaitForSeconds (time);
		text.GetComponent<Text>().enabled = false;

	}

}
