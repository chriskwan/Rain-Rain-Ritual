﻿using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	private System.DateTime lastBeat;

	public double inputThreshold = .35;

	public Metronome metronome;

	public delegate void InputEvent(ElementType element);

	public event InputEvent ElementEvent;

	public GameObject firePrefab;
	public GameObject waterPrefab;
	public GameObject windPrefab;
	public GameObject earthPrefab;

	void Start () {
		metronome = GameObject.Find ("Metronome").GetComponent<Metronome> ();
		metronome.OnTick += Store;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire")) {
			Instantiate (firePrefab, gameObject.transform.position, gameObject.transform.rotation);
			StartCoroutine (GameObject.Find ("FireRipple").GetComponent<RippleEffect> ().Ripple ());
			if (VerifyBeat ()) {
				ElementEvent (ElementType.Fire);
			}
		} else if (Input.GetButtonDown ("Water")){ 
			Instantiate (waterPrefab, gameObject.transform.position, gameObject.transform.rotation);
			StartCoroutine (GameObject.Find ("WaterRipple").GetComponent<RippleEffect> ().Ripple ());

			if (VerifyBeat()){
				ElementEvent(ElementType.Water);
			}
		} else if (Input.GetButtonDown("Wind")) {
			Instantiate (windPrefab, gameObject.transform.position, gameObject.transform.rotation);
			 StartCoroutine (GameObject.Find ("WindRipple").GetComponent<RippleEffect> ().Ripple ());
			if (VerifyBeat()){
				ElementEvent (ElementType.Wind);
			}
		} else if (Input.GetButtonDown ("Earth")) {
			Instantiate (earthPrefab, gameObject.transform.position, gameObject.transform.rotation);
			StartCoroutine (GameObject.Find ("EarthRipple").GetComponent<RippleEffect> ().Ripple ());

			if (VerifyBeat ()) {
				ElementEvent (ElementType.Earth);
			}
		}
	}

	void Store () {
		lastBeat = System.DateTime.Now;
		//print (lastBeat);
	}

	bool VerifyBeat() {
		System.DateTime inputTime = System.DateTime.Now;
		double millisecondSpan = (inputTime - lastBeat).TotalMilliseconds;
		if (System.Math.Round(millisecondSpan - inputThreshold) % 2 == 0 || System.Math.Round(millisecondSpan + inputThreshold) % 2 == 0) {
			//print ("onBeat" + System.Math.Round((inputTime - lastBeat).TotalMilliseconds));
			return true;
		} else {
			print("offBeat");
			ElementEvent (ElementType.OffBeat);

			return false;
		}
	}

}