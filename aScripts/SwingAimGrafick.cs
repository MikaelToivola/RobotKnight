using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingAimGrafick : MonoBehaviour {

	LineRenderer line;
	GameObject arms;
	GameObject targetBox;

	public float StartWidth = 1f;
	public float EndWidth = 0.5f;


	void Start () {
		arms = GameObject.Find ("SwordArms");
		targetBox = GameObject.Find ("TargetBox");
		line = GetComponent<LineRenderer> ();
	}

	void Update () {

		if (arms != null && targetBox != null) {

			line.startWidth = StartWidth;
			line.endWidth = EndWidth;
			line.SetPosition (0, arms.transform.position);
			line.SetPosition (1, targetBox.transform.position);

			//line.transform.transform.position = arms.transform.position;

			//line.transform.localScale = Vector3.zero;

			/*
			if (GetComponent<Image> ().enabled == true) {
				Vector2 targetPos = Camera.main.WorldToScreenPoint (arms.transform.position);
				transform.position = targetPos;

				Vector3 direction = (targetBox.transform.position - targetBox.transform.position);

				float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}*/
		}
	}
}
