using UnityEngine;
using System.Collections;

public class SensorScript : MonoBehaviour {

	public float sensorRange = 100.0F;
	bool playerInSight = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Sensor ();
	
	}
	void Sensor()
	{

		var player = GameObject.FindWithTag("Player").transform; //target the player ... again

		Vector3 direction = (player.transform.position - transform.position).normalized;

		Ray ray = new Ray(transform.position, direction);

		RaycastHit hit;


		if (Physics.Raycast(ray, out hit, sensorRange))
		{
			Debug.DrawLine(ray.origin, hit.point);

			Vector3 hitPoint = hit.point;
			GameObject go = hit.collider.gameObject;
			if (go.tag == "Player" ) // wall check!!!
			{
				playerInSight = true;
				//Debug.Log ("playerInSight");
			}
		}
	}
}