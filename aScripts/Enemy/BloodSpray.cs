using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpray : MonoBehaviour {

	public bool Spraying = true;
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
		if (Spraying) {
			transform.localPosition = Vector3.zero;
			if (cooldownLeft <= 0) {
				cooldownLeft = bleedCooldown;
				bleedLeft -= 1;
				float x = Random.Range (-0.5f, 2f);
				float y = Random.Range (-0.5f, 2f);
				float z = Random.Range (-0.5f, 2f);

				GameObject drop = Instantiate (BloodDrop, transform.position, Quaternion.identity);
				Rigidbody rig = drop.GetComponent<Rigidbody> ();
				if (rig != null) {
					drop.GetComponent<Rigidbody> ().AddForce (new Vector3 (Vector3.down.x + x, Vector3.down.y + y,
						Vector3.down.z + z) * sprayStrength, ForceMode.Impulse);
				}

			} else {
				cooldownLeft -= Time.deltaTime;
			}

			if (bleedLeft <= 0) {
				Destroy (this.gameObject);
			}
		}
	}
}
