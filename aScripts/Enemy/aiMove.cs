using System.Collections;
using UnityEngine;

public class aiMove : MonoBehaviour {

    
    public float moveSpeed = 3f; //move speed
    public float rotationSpeed = 3f; //speed of turning

    Vector3 direction;
    GameObject go;

    public float sensorRange = 100.0F;
    float sensorCooldownRemaining = 0f;
    public float sensorCooldown = 0.5F;

    float meleeCoolDown = 0.5F;
    public float meleeCoolDownRemaining = 0f;

    public float damage = 10.0F;
    public float meleeRange = 10f;

    bool playerInSight = false;
    bool grounded = false;
    float distanceToGround = 0f;

	Animator anim;

    public GameObject debrisPrefab;
    
	void Start(){
		anim = GetComponentInChildren<Animator> ();

	}

    void Update(){
        //groundCheck();

        var player = GameObject.FindWithTag("Player").transform; //target the player

        if (sensorCooldownRemaining <= 0){
            Sensor();
        }

		if (playerInSight == true){
			
			if (anim == null) {
				Debug.Log ("NO ANIMATOR!");
			}
			anim.SetInteger ("speed", 1);
	        //rotate to look at the player
	        transform.rotation = Quaternion.Slerp(transform.rotation,
	        	Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);

	        //move towards the player
	        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        meleeCoolDownRemaining -= Time.deltaTime;

        if (playerInSight == true && meleeCoolDownRemaining <= 0)
        {
            melee();
            meleeCoolDownRemaining = meleeCoolDown;
        }
    }

    void Sensor()
    {
        
        var player = GameObject.FindWithTag("Player").transform; //target the player ... again

        Vector3 direction = (player.transform.position - transform.position).normalized;

        Ray ray = new Ray(transform.position, direction);
       
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, sensorRange))
        {
            //Debug.DrawLine(ray.origin, hit.point);

            Vector3 hitPoint = hit.point;
            GameObject go = hit.collider.gameObject;
            if (go.tag == "Player" ) // wall check!!!
            {
                playerInSight = true;
            }
        }
         
        
    }

    void melee()
    {
        //Ray meleeRay = new Ray(transform.position, direction);
        var player = GameObject.FindWithTag("Player").transform; //target the player ... again

        Vector3 direction = (player.transform.position - transform.position).normalized;

        Ray ray = new Ray(transform.position, direction);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, meleeRange))
        {
            Vector3 hitPoint = hitInfo.point;
            GameObject go = hitInfo.collider.gameObject;
            Debug.Log("meleeHit Object: " + go.name);
            Debug.Log("origin " + transform.position + (transform.forward / 2));
            Debug.Log("meleeHit Point: " + hitPoint);

            Debug.DrawLine(ray.origin, hitInfo.point);


            HasHealth h = go.GetComponent<HasHealth>();

			if (go.tag == "Player") {
				Debug.Log ("DAMAGE " + damage);
				anim.SetTrigger ("melee");
				h.ReceiveDamage (damage);
            

				if (debrisPrefab != null) {
					Instantiate (debrisPrefab, hitPoint, Quaternion.identity);
				}
			}
        }
    }

    void groundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0F))
        {
            distanceToGround = hit.distance;

        }
        if (distanceToGround <= 0.1f)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }


}
