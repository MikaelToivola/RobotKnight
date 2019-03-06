using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCleared : MonoBehaviour {

	public List<GameObject> enemies = new List<GameObject> ();

	public GameObject gate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enemies[1] == null){
			Destroy (gate);
		}
		
	}
}
