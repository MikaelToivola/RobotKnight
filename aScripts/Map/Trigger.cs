using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

	//public GameObject[] targets;
	public List<GameObject> targets = new List<GameObject> ();

	bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player" || other.tag == "Dragon") {
			if(triggered == false){
			triggered = true;

			for(int i = 0; i < targets.Count ; i++){
				
				if (targets [i].tag == "Spawner") {
					targets [i].GetComponent<EnemySpawner> ().SpawnEnemy ();
					Debug.Log ("Triggered " + targets [i].name);
				} else if (targets [i].tag == "Dragon") {
					targets [i].GetComponent<RocketEngine> ().move = true;
					Debug.Log ("Triggered " + targets [i].name);

				}else if (targets [i].tag == "Enemy") {
					targets [i].GetComponent<RagdollGun> ().playerSeen = true;
					Debug.Log ("Triggered " + targets [i].name);

				} else if(targets [i].name == "CorpseSpawn"){
						targets [i].GetComponent<BloodSpray> ().Spraying = true;

				}else{
					Debug.LogError ("Failed to trigger!");
				}
			}
			}
		}
		
	}
}
