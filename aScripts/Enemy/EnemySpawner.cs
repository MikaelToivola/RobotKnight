using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	public void SpawnEnemy(){
		
		GameObject spawned = Instantiate (enemyPrefab, transform.position, Quaternion.identity);
		spawned.transform.rotation = transform.rotation;
	}
}
