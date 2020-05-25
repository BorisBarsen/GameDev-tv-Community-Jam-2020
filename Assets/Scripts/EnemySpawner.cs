using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] [Range(0.1f, 120f)] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            GameObject enemyClone = Instantiate(enemyPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
            enemyClone.transform.parent = gameObject.transform;
            print("Spawning!");
        }
    }
}
