using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] float secondsBetweenSpawns = 2f;
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
            GameObject enemyClone = Instantiate(enemyPrefab.gameObject, transform.position, transform.rotation) as GameObject;
            print("Spawning!");
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
}
