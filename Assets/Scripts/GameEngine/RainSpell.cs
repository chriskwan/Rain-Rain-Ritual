﻿using System;
using UnityEngine;
using System.Collections.Generic;

public class RainSpell : Spell
{
	private GameObject cloud = null;
	private GameObject rain = null;

	public RainSpell (string name, int numTicksToWin, int maxTicksForSpell) 
		: base(name, numTicksToWin, maxTicksForSpell) {
	}

	protected override List<Element> ElementList {
		get {
			var elements = new List<Element> () {
				//new Element (ElementType.Fire, 0, 5, 5),
				//new Element (ElementType.Earth, 0, 5, 5),

				//cwkTODO put back water and wind settings
				//			new Element (ElementType.Water, 25, 32, 1.0f),
				//			new Element (ElementType.Wind, 25, 32, 0.5f)

				//cwkTODO easier settings for testing
				new Element (ElementType.Water, 5, 10, 1.0f),
				new Element (ElementType.Wind, 5, 10, 0.5f)
			};
			return elements;
		}
	}

	protected override AudioSource WinSound {
		get { return audioDict.GetSound ("thunderclap"); }
	}

	protected override AudioSource LoseSound {
		get { return audioDict.GetSound ("cloudfailure"); }
	}

	protected override void CenterObjectInitialize () {
		this.cloud = GameObject.Find ("Cloud");
		//cwkTODO ask Christina why cloud is warping initially
		if (this.cloud != null) {
			this.cloud.GetComponent<CloudBehavior> ().reset ();
			this.cloud.GetComponent<CloudBehavior> ().growResult (0);
		}
	}

	protected override void CenterObjectUpdate () {
		if (this.cloud != null) {
			this.cloud.GetComponent<CloudBehavior> ().growResult (((float)numTicksInRange / (float)numTicksToWin) / 2f);
		}
	}

	protected override void CenterObjectWin () {
		if (this.cloud != null) {
			this.cloud.GetComponent<CloudBehavior> ().winResult ();
		} else {
			Debug.Log ("cloud is null in win");
		}
	}

	protected override void CenterObjectLose () {
		if (this.cloud != null) {
			this.cloud.GetComponent<CloudBehavior> ().loseResult ();
		} else {
			Debug.Log ("cloud is null is lose");
		}
	}

	protected override void WinAnimationInitialize () {
		this.rain = GameObject.Find("VFX_Rain");
	}

	protected override void ShowWinAnimation (bool show) {
		if (rain != null) {
			foreach (Transform t in rain.transform) {
				t.GetComponent<MeshRenderer> ().enabled = show;
			}
		}
	}
}
