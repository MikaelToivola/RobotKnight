using UnityEngine;
using System.Collections;

public class selfDestruct : MonoBehaviour {

	public float timer = 1f;
	float timerLeft;


	// Use this for initialization
	void Start () {
		timerLeft = timer;
	}
	
	// Update is called once per frame
	void Update () {
		timerLeft -= Time.deltaTime;

		if (timerLeft <= 0) {
			Destroy (this.gameObject);
		}
	
	}
}
