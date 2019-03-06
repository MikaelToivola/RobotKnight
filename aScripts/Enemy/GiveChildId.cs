using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveChildId : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.gameObject.name = transform.root.gameObject.name  + "_" + this.gameObject.name;
		Debug.Log ("Renamed " + this.gameObject.name);
	}
	

}
