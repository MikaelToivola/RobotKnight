using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class fpsMove : MonoBehaviour {

	public float moveSpeed = 5.0F;
	public float jumpSpeed = 5.0F;
	public float characteGravity = 9.81f;

	public float mouseSensY = 5f;
	public float mouseSensX = 8f;

	float verticalRotation = 0;
	float verticalVelocity = 0;
	public float upDownRange = 60;
	float rotleftright;

	public bool lockMouse;
	private GameObject target;
	public float locOnRotSpeed = 20f;

	CharacterController characterController;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		lockMouse = false;
	}
	
	void Update () {
		//cc needs to be in Update()
		CharacterController characterController = GetComponent<CharacterController>(); 

		//mouse
		if (lockMouse == false) {
			rotleftright = Input.GetAxis ("Mouse X") * mouseSensX;
			transform.Rotate (0, rotleftright, 0);

			verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensY;
			verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
			Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		} else if(lockMouse){
			target = GetComponent<CombatScript> ().target;
			Vector3 direction = target.transform.position - transform.position;
			direction = new Vector3 (direction.x, 0, direction.z);

			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(direction), locOnRotSpeed * Time.deltaTime);

			/*verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensY;
			verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);*/
			Camera.main.transform.localRotation = Quaternion.Euler (0, 0, 0);//(verticalRotation, 0, 0);

		}


		//movement
		float forwardSpeed = Input.GetAxis("Vertical") ;//* moveSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") ;//* moveSpeed;

		//gravity
		verticalVelocity = verticalVelocity -characteGravity * Time.deltaTime;// Physics.gravity.y

		if (characterController.isGrounded && Input.GetButton ("Jump") ) {
			verticalVelocity = jumpSpeed;
		}
		Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed).normalized * moveSpeed;

		speed = new Vector3(speed.x, verticalVelocity, speed.z);

		speed = transform.rotation * speed;


		characterController.Move(speed * Time.deltaTime);

	
	}
}
