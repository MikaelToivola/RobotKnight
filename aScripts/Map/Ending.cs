using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {
	bool end = true;
	public GameObject BloodDrop;
	public int bleedAmount = 1;
	int bleedLeft;
	public float bleedCooldown = 1f;
	float cooldownLeft;
	public float sprayStrength = 2f;


	void Start () {
		bleedLeft = bleedAmount;
		cooldownLeft = 0;
	}

	// Update is called once per frame
	void Update () {
		if (end) {
			//transform.localPosition = Vector3.zero;
			if (cooldownLeft <= 0) {
				cooldownLeft = bleedCooldown;
				bleedLeft -= 1;
				Vector3 pos = new Vector3 (transform.position.x, transform.position.y + 7.5f, transform.position.z);
				Instantiate (BloodDrop, transform.position, Quaternion.identity);

			} else {
				cooldownLeft -= Time.deltaTime;
			}
			if (bleedLeft <= 0) {
				SceneManager.LoadScene ("level0");
			}
				
		}
	}
	void OnTriggerEnter(){
		end = true;
	}
}
