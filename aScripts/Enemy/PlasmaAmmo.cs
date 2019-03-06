using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaAmmo : MonoBehaviour {


	public float damage = 5f;
	GameObject hitObject;
	Rigidbody rig;
	public GameObject hitEffect;
	public float speed = 0.1f;
	public Vector3 direction;

	bool hasBounced = false;

	void Start () {
		rig = GetComponent<Rigidbody> ();

	}
	void Update(){
		rig.AddForce (direction * speed, ForceMode.Impulse);
		//transform.Translate (direction * speed);
	}

	void OnTriggerEnter( Collider other){
		GameObject hitObject = other.gameObject;

		if (hitObject.tag == "Player" || hitObject.tag == "Enemy") {
			HasHealth healthSkript = hitObject.GetComponent<HasHealth> ();
			healthSkript.ReceiveDamage (damage);

			Debug.Log (transform.gameObject.name + "dealt " + damage + " damage to "+ hitObject.name);
			BlowUp ();

		}if (hitObject.tag == "EnemyBodypart") {
			HasHealth healthSkript = hitObject.transform.root.GetComponent<HasHealth> ();
			healthSkript.ReceiveDamage (damage);

			Debug.Log (transform.gameObject.name + "dealt " + damage + " damage to " + hitObject.transform.root.gameObject.name);
			BlowUp ();

		} else if (hitObject.tag == "Sword" && hasBounced == false) {
			hasBounced = true;
			Debug.Log ("Bounse of sword");
			GameObject player = hitObject.transform.root.gameObject;

			float randX = Random.Range (-2f, 2f);
			float randY = Random.Range (-2f, 2f);
			float randZ = Random.Range (-2f, 2f);
			direction = new Vector3 (direction.x + randX, direction.y + randY, direction.z + randZ);
			direction = direction * -1f;
			rig.AddForce (direction * 2f, ForceMode.Impulse);

		} else if (hitObject.tag == "Trigger") {
			return;
		}else{
			BlowUp ();
		}
	}
	void BlowUp(){
		if (hitEffect != null) {
			Instantiate (hitEffect, transform.position, Quaternion.identity);
		}
		Destroy (this.gameObject);
	}
}
