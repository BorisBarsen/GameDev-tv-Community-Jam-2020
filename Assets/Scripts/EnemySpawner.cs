using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] EnemyMovement enemyPrefab;

    [SerializeField] Text endOfWavePrompt;

    [SerializeField] int[] enmiesPerWave; //TODO hide
    [SerializeField] [Range(0.1f, 120f)] float[] secondsBetweenSpawnsPerWave;

    [SerializeField] int currentWave; //TODO hide



    //State//TODO hide
    public bool stopped;

    private int enemiesLeft;

	// Use this for initialization
	void Start () {
        stopped = false;
        currentWave = -1;
        StartNextWave();
        endOfWavePrompt.enabled = false;

        StartCoroutine(RunSpawner());

    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            stopped = false;
            endOfWavePrompt.enabled = false;
        }
    }

    private void StartNextWave()
    {
        //stopped = true;
        // Do things between waves
        //      Show text about succesfull wave
        //      Wait for specific input to start next wave 
        //      Increase seasonal cycle speed
        endOfWavePrompt.enabled = true;

        currentWave++;
        enemiesLeft = enmiesPerWave[currentWave];



    }

    IEnumerator RunSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenSpawnsPerWave[currentWave]);

            if (!stopped)
            { 
                if (enemiesLeft > 1)
                {
                    enemiesLeft--;
                    SpawnEnemy();
                }
                else
                {
                    if (currentWave + 1 < enmiesPerWave.Length)
                    {
                        stopped = true;
                        StartNextWave();
                    }
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyClone = Instantiate(enemyPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        enemyClone.transform.parent = gameObject.transform;
    }
}
