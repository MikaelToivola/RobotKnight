using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HasHealth : MonoBehaviour {

    public float hitPoints = 100.0F;
    //float startingHealth = 100.0f;
    //public Transform spawnPoint;
    //public GameObject Player;
	public GameObject corpse;
	bool isAlive = true;
	public GameObject bloodPrefab;
	public Image healtfBar;


    public void ReceiveDamage (float damage) {
        hitPoints -= damage;

		if(hitPoints <= 0 && isAlive){
			isAlive = false;
            Die();
        }
	}
	void Start(){
		if (transform.tag == "Player") {
			healtfBar.fillAmount = 1f;

		}
	}
	void Update(){
		if (transform.tag == "Player") {
			
			healtfBar.fillAmount = (hitPoints / 100f);
		}
	}
	
	void Die() {
		if (tag == "Player") {
			Respawn ();
		} else if (tag == "Gun") {
			Debug.Log ("LaserGun DESTROYED");
			transform.root.GetComponent<RagdollGun> ().GunDestroyed = true;

		} 
		if (corpse != null) {
			if (transform.tag == "Enemy") {
				Transform rootBone = transform.root.FindChild ("RagdollMan").FindChild ("metarig").FindChild ("hips");
				Instantiate (corpse, rootBone.position, transform.rotation);
			} else {
				Instantiate (corpse, transform.position, transform.rotation);
			}
		}
		Destroy(gameObject);
        
    }
    void Respawn()
    {
		//Application.LoadLevel (Application.loadedLevel);
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
        /*Player.transform.position = spawnPoint.position;
        hitPoints = startingHealth;*/
    }
	/*public void CheckHealth()
	{
		return 	hitPoints;
	}	*/
}
