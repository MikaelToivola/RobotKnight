using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour {

	public GameObject projectile;
	//public float projectileSpeed = 5f;
	public GameObject gunR;
	public GameObject gunL;

	Transform barrelR;
	Transform barrelL;

	GameObject player;
	public bool playerSeen;

	public float Rcooldown = 5f;
	float Rcooldownleft;
	public float Lcooldown = 5f;
	float Lcooldownleft;

	public float rotationSpeed = 2f;

	public bool LGunDestroyed = false;
	public bool RGunDestroyed = false;

	GameObject hips;

	void Start () {

		player = GameObject.Find ("Player");
		//playerSeen = false;
		//cooldownleft = Random.Range(1f, cooldown);
	}

	void Update () {
			
		barrelR = gunR.transform.FindChild ("GunBarrel");
		barrelL = gunL.transform.FindChild ("GunBarrel");

		if (RGunDestroyed) {
			gunR = null;
			barrelR = null;
		}
		if (LGunDestroyed) {
			gunL = null;
			barrelL = null;
		}
		hips = transform.FindChild ("RagdollMan").FindChild("metarig").FindChild("hips").gameObject;
		Rigidbody rig = hips.GetComponent<Rigidbody> ();

		Vector3 direction = player.transform.position - transform.position;

		float angleDiff = Vector3.Angle (hips.transform.forward, direction);

		Vector3 cross = Vector3.Cross (hips.transform.forward, direction);

		rig.AddTorque (cross * angleDiff * rotationSpeed);

		if (playerSeen && barrelR != null && RGunDestroyed == false) {
			direction = player.transform.position - transform.position;
			direction = new Vector3 (direction.x, direction.y, direction.z);

			if (Rcooldownleft <= 0) {
				Rcooldownleft = Rcooldown;

				Debug.Log (transform.name + " Fired");
				GameObject bullet = Instantiate (projectile, barrelR.position, Quaternion.identity);

				float randX = Random.Range (-0.4f, 0.4f);
				float randY = Random.Range (-0.2f, 0.5f);
				float randZ = Random.Range (-0.4f, 0.4f);

				Vector3 adjustedPlayerPos = new Vector3 (player.transform.position.x + randX, 
					player.transform.position.y + randY, player.transform.position.z + randZ);

				Vector3 aim = (adjustedPlayerPos - barrelR.transform.position);
				bullet.GetComponent<PlasmaAmmo> ().direction = aim;

			} else if (Rcooldownleft > 0) {
				Rcooldownleft -= Time.deltaTime;
			}
		}
		if (playerSeen && barrelL != null && LGunDestroyed == false) {
			direction = player.transform.position - transform.position;
			direction = new Vector3 (direction.x, direction.y, direction.z);

			if (Lcooldownleft <= 0) {
				Lcooldownleft = Lcooldown;

				Debug.Log (transform.name + " Fired");
				GameObject bullet = Instantiate (projectile, barrelL.position, Quaternion.identity);

				float randX = Random.Range (-0.4f, 0.4f);
				float randY = Random.Range (-0.2f, 0.5f);
				float randZ = Random.Range (-0.4f, 0.4f);

				Vector3 adjustedPlayerPos = new Vector3 (player.transform.position.x + randX, 
					player.transform.position.y + randY, player.transform.position.z + randZ);

				Vector3 aim = (adjustedPlayerPos - barrelL.transform.position);
				bullet.GetComponent<PlasmaAmmo> ().direction = aim;

			} else if (Lcooldownleft > 0) {
				Lcooldownleft -= Time.deltaTime;
			}
		}
	}
}
