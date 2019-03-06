using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingTag : MonoBehaviour {
	
	public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (target != null) {
			GetComponent<Image> ().enabled = true;
				
			Vector2 targetPos = Camera.main.WorldToScreenPoint (target.position);
			transform.position = targetPos;
		} else {
			GetComponent<Image> ().enabled = false;
		}
	}

}
