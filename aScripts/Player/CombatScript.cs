using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatScript : MonoBehaviour {

	public float mouseSensX = 0.15f;
	public float mouseSensY = 0.15f;

	public bool lockedOn = false;
	public float lockOnRange = 10f;

	public GameObject target;
	public GameObject arms;
	GameObject targetBox;

	public float ArmLength = 1.5f;
	//public float ArmRayRange = 5f;
	//Vector3 ArmRayHitPoint;

	Vector3 direction;
	Vector3 bladeDirection;
	Vector3 prevMousePos;
	public Vector3 swingDirection;

	public float armMoveSpeed = 0.2f;
	public float swingSpeed = 0.2f;
	public float SwordRotateSpeed = 1.0f;

	public bool isAttacking = false;
	bool isAiming = false;
	public float attackCooldown = 5f;
	float attackTimer;

	Animator anim;
	fpsMove moveSkript;
	bool animStarted = false;

	//public int layerMask = 1 << 8;
	public LayerMask lockOnlayerMask;
	public LayerMask armMovelayerMask;

	public Image lockOnImage;
	FloatingTag floatingSkript;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		anim = GameObject.Find ("swordWithAnim").GetComponent<Animator>();
		moveSkript = GetComponent<fpsMove> ();
		targetBox = transform.FindChild ("TargetBox").gameObject;
		floatingSkript = lockOnImage.GetComponent<FloatingTag> ();

	}
	// Update is called once per frame
	void Update () {
		//LockOn
		if (target == null) {
			lockedOn = false;
			moveSkript.lockMouse = false;
			floatingSkript.target = null;

		}

		if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.LeftShift)) {
			if (lockedOn == false) {
				LockOn ();
			} else {
				lockedOn = false;
				target = null;
				moveSkript.lockMouse = false;
			}
		}

		//Move Arms
		if (isAttacking) {
			Swing ();
		} else if (lockedOn && isAttacking == false) {
			MoveArms ();
		}else {
			arms.transform.localPosition = new Vector3 (0, 0, ArmLength);
		}
		if (isAiming == false && isAttacking == false) {
			targetBox.transform.localPosition = arms.transform.localPosition;
		}

		//ATTACKING
		if (Input.GetKeyDown (KeyCode.Mouse0) && isAttacking == false && lockedOn) {
			
			prevMousePos = Input.mousePosition;
			isAiming = true;
			Time.timeScale = 0.2f;
		} /*else if (Input.GetKeyDown (KeyCode.Mouse0) && isAttacking == false && lockedOn == false) {
			animStarted = true;
			isAttacking = true;
			anim.SetTrigger ("swing");
		}*/

		if (Input.GetMouseButtonUp (0)) {
			attackTimer = attackCooldown;

			isAiming = false;
			isAttacking = true;
			Time.timeScale = 1.0f;

		}

		//TIMER
		if (isAttacking) {
			if (attackTimer > 0) {

				attackTimer -= Time.deltaTime;
			} else {

				isAttacking = false;
				animStarted = false;

			}
		}
	}

	void LockOn(){
		int layerMask = 1 << 8;

		layerMask = ~layerMask;
		RaycastHit hitInfo;
		Ray lockOnRay = new Ray (Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward);

		if (Physics.Raycast (lockOnRay, out hitInfo, lockOnRange, lockOnlayerMask)) {
			Vector3 hitPoint = hitInfo.point;
			GameObject go = hitInfo.collider.gameObject;
			//Debug.Log ("Hit Object: " + go.name);
			//Debug.Log("Hit Point: " + hitPoint);

			Debug.DrawLine (lockOnRay.origin, hitPoint);

			if (go.tag == "Enemy") {
				target = go;
				lockedOn = true;
				moveSkript.lockMouse = true;
				floatingSkript.target = target.transform;

				Debug.Log ("Locked on to: " + target.name);
			} else if (go.tag == "EnemyBodypart") {
				target = go.transform.root.gameObject;

				target = target.transform.FindChild ("RagdollMan").FindChild("metarig").FindChild("hips").gameObject;
				floatingSkript.target = target.transform;

				lockedOn = true;
				Debug.Log ("Locked on to: " + target.name);
				moveSkript.lockMouse = true;
			}
		}
	}

	void MoveArms(){
			
			GameObject moveObject = arms;
			if (isAiming) {
				moveObject = targetBox;
			}

			float moveX = moveObject.transform.localPosition.x;
			moveX -= -Input.GetAxis ("Mouse X") * mouseSensX;
			moveX = Mathf.Clamp (moveX, -Screen.width * 0.0007f, Screen.width * 0.0007f);

			float moveY = moveObject.transform.localPosition.y;
			moveY -= -Input.GetAxis ("Mouse Y") * mouseSensY;
			moveY = Mathf.Clamp (moveY, -Screen.height * 0.0001f, Screen.height * 0.0015f);

			Vector3 previousPos = moveObject.transform.localPosition;
			Vector3 nextPos = new Vector3 (moveX, moveY, ArmLength);

			Vector3 armDirection = nextPos - previousPos;

			moveObject.transform.Translate (armDirection * armMoveSpeed);
	}
	void RotateArms(){
		/*if (isAttacking == false) {
			bladeDirection = GameObject.Find ("SwordFaceObj").transform.position - arms.transform.position;
			//bladeDirection = targetBox.transform.localPosition - arms.transform.localPosition;

			arms.transform.rotation = Quaternion.Slerp (arms.transform.rotation,
				Quaternion.LookRotation (bladeDirection), SwordRotateSpeed * Time.deltaTime);
		} else {
			arms.transform.rotation = Quaternion.Euler (target.transform.position - arms.transform.position);
		}*/

	}

	void Swing(){

		if (lockedOn) {
			
			swingDirection = targetBox.transform.localPosition - arms.transform.localPosition;

			arms.transform.Translate (swingDirection.normalized * swingSpeed);

			if (animStarted == false) {
				animStarted = true;

				if (Mathf.Abs(swingDirection.y) > 1.5f * Mathf.Abs(swingDirection.x) ) {
					anim.SetTrigger ("swing");
				}else if (swingDirection.x > 0) { // (Input.mousePosition.x < Screen.width / 3) {
					anim.SetTrigger ("swing_L");
				} else if (swingDirection.x < 0) { // (Input.mousePosition.x > 2 * Screen.width / 3) {
					anim.SetTrigger ("swing_R");
				} 
			}

		} else if(lockedOn == false && animStarted == false){
			animStarted = true;
			anim.SetTrigger ("swing");
		}

	}
	

}
		/*
		RaycastHit MouseHitInfo;
		Ray MouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (MouseRay, out MouseHitInfo, ArmRayRange, armMovelayerMask)) {//fire at the target cube
			
			ArmRayHitPoint = MouseHitInfo.point;
			Debug.DrawLine (MouseRay.origin, ArmRayHitPoint);
		}
		//move
		arms.transform.position = ArmRayHitPoint;//optional method=> 

		//direction = ArmRayHitPoint - arms.transform.position;

		//arms.transform.Translate (direction * armMoveSpeed);
		*/
		/*if (Input.GetKeyDown (KeyCode.Mouse1) && isAttacking == false) {//Stab
		anim.SetTrigger ("stab");
		attackTimer = attackCooldown;
		isAttacking = true;
		}*/