using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollGun : MonoBehaviour {

	public GameObject projectile;
	//public float projectileSpeed = 5f;
	GameObject gun;
	Transform barrel;
	GameObject player;
	public bool playerSeen;

	public float cooldown = 5f;
	float cooldownleft;

	public float rotationSpeed = 2f;

	public bool GunDestroyed = false;
	GameObject hips;

	void Start () {

		player = GameObject.Find ("Player");
		//playerSeen = false;
		cooldownleft = Random.Range(1f, cooldown);
	}
	
	void Update () {
		if (gun == null && GunDestroyed == false) {
			gun = GameObject.Find(this.gameObject.name + "_Lazergun");
			barrel = gun.transform.FindChild ("GunBarrel");
		}
		if (GunDestroyed) {
			gun = null;
			barrel = null;
		}
		if (playerSeen && barrel != null && GunDestroyed == false) {
			Vector3 direction = player.transform.position - transform.position;
			direction = new Vector3 (direction.x, direction.y, direction.z);

			hips = transform.FindChild ("RagdollMan").FindChild("metarig").FindChild("hips").gameObject;
			Rigidbody rig = hips.GetComponent<Rigidbody> ();

			float angleDiff = Vector3.Angle (hips.transform.forward, direction);

			Vector3 cross = Vector3.Cross (hips.transform.forward, direction);

			rig.AddTorque (cross * angleDiff * rotationSpeed);
			//gun.transform.rotation = Quaternion.Slerp (transform.rotation,
			//	Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);

			if (cooldownleft <= 0) {
				cooldownleft = cooldown;


				Debug.Log (transform.name + " Fired");
				GameObject bullet = Instantiate (projectile, barrel.position, Quaternion.identity);

				float randX = Random.Range (-0.4f, 0.4f);
				float randY = Random.Range (-0.2f, 0.5f);
				float randZ = Random.Range (-0.4f, 0.4f);

				Vector3 adjustedPlayerPos = new Vector3 (player.transform.position.x + randX, 
					player.transform.position.y + randY, player.transform.position.z + randZ);

				Vector3 aim = (adjustedPlayerPos - barrel.transform.position);
				bullet.GetComponent<PlasmaAmmo> ().direction = aim;
				//bullet.GetComponent<Rigidbody> ().AddRelativeForce (aim * projectileSpeed,ForceMode.Impulse);

			} else if (cooldownleft > 0) {
				cooldownleft -= Time.deltaTime;
			}
		}
	}
}
