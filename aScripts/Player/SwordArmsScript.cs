using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordArmsScript : MonoBehaviour {


	void OnTriggerEnter(Collider other){
		Debug.Log("Arms hit " + other.name);

		if (other.name == "TargetBox") {
			transform.root.GetComponent<CombatScript> ().isAttacking = false;
			Debug.Log ("Swing Stopped");
		}
	}
}
