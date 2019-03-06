using UnityEngine;
using System.Collections;

public class WeakSpotScript : MonoBehaviour {


	public void PassDamage(float damage){

		HasHealth health = transform.root.GetComponent<HasHealth> ();
		health.ReceiveDamage (damage);

		GameObject bloodParticle = health.bloodPrefab;
		Instantiate (bloodParticle, this.transform);
		}
}