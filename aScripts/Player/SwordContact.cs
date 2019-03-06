using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordContact : MonoBehaviour {

	public float damage = 10f;
	public float force = 15f;
	CombatScript combat;
	// Use this for initialization
	void Start () {
		combat = transform.root.GetComponent<CombatScript> ();
	}

	void OnTriggerEnter(Collider other){
		GameObject go = other.gameObject;
		if (go.name != "Player") {
			Debug.Log ("Sword hit " + go.name);
		}
		if (combat.isAttacking) {
			if (go.tag == "Enemy" || go.tag == "Gun" || go.tag == "Breakable") {
				Debug.Log ("Contact on enemy");
				HasHealth health = go.GetComponent<HasHealth> ();
				if (health != null) {
					health.ReceiveDamage (damage);
				}
			} else if (go.tag == "EnemyBodypart") {
				go.GetComponent<Rigidbody> ().AddForce (combat.swingDirection * force, ForceMode.Impulse);

				WeakSpotScript spot = go.GetComponent<WeakSpotScript> ();
				if (spot != null) {
					spot.PassDamage (damage);
				}
			}
		}
	}
}
