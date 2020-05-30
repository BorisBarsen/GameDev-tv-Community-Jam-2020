using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    //[SerializeField] EnemyMovement enemyPrefab;

    [SerializeField] Text endOfWavePrompt;

    //[SerializeField] int[] enmiesPerWave; //TODO hide
    //[SerializeField] [Range(0.1f, 120f)] float[] secondsBetweenSpawnsPerWave;

    [SerializeField] EnemyMovement enemyLevel1Prefab;
    [SerializeField] EnemyMovement enemyLevel2Prefab;
    [SerializeField] EnemyMovement enemyLevel3Prefab;



    Queue<Queue<Vector2>> waves = new Queue<Queue<Vector2>>();
    Queue <Vector2> currentWave = new Queue<Vector2>(); //TODO hide
    Vector2 currentEnemy; //TODO hide

    int waveCounter = 0;



    //State//TODO hide
    public bool stopped;

    private int enemiesLeft;

	// Use this for initialization
	void Start () {
        InitializeWaves();
        StartNextWave();

        StartCoroutine(RunSpawner());
    }

    void InitializeWaves()
    {
        var wave1 = new Queue<Vector2>();
        var wave2 = new Queue<Vector2>();
        waves = new Queue<Queue<Vector2>>(new[]
            {
                //// Wave 1
                //new Queue<Vector2>(new[]
                //{
                //    new Vector2(1, 1f),
                //    new Vector2(1, 1f),
                //    new Vector2(1, 1f)
                //}),

                //Wave 2
                new Queue<Vector2>(new[]
                {
                    new Vector2(1, 0.5f),
                    new Vector2(1, 0.5f),
                    new Vector2(1, 0.5f),
                    new Vector2(1, 1.5f),
                    new Vector2(1,  0.5f),
                    new Vector2(1,  0.5f),
                    new Vector2(2,  0f)
                }),
                //Wave 3
                new Queue<Vector2>(new[]
                {
                    new Vector2(1, 0.5f),
                    new Vector2(2, 0.5f),
                    new Vector2(1, 2.5f),
                    new Vector2(2, 1.5f),
                    new Vector2(1,  0.5f),
                    new Vector2(1,  10.5f),
                    new Vector2(2, 1.5f),
                    new Vector2(2, 0f),
                }),
            });
    }
    //Queue<Vector2Int>

    private void Update()
    {
        if(stopped)
        {
            if (Input.GetKeyDown("space"))
            {
                stopped = false;
                endOfWavePrompt.enabled = false;
            }
        }
    }

    private void StartNextWave()
    {

        endOfWavePrompt.enabled = true;
        stopped = true;
        currentWave = waves.Dequeue(); 
    }

    IEnumerator RunSpawner()
    {
        while (true)
        {
            //foreach (Vector2Int enemy in currentWave)
            //{


            ////}
            //print(waves.Count);
            //print("a " + currentWave.Count);

            if (!stopped)
            { 
                if (currentWave.Count > 0)
                {
                    currentEnemy = currentWave.Dequeue();
                    print(currentEnemy);
                    SpawnEnemy((int)currentEnemy[0]);
                    yield return new WaitForSeconds(currentEnemy.y);

                }
                else
                {
                    StartNextWave();
                    yield return new WaitForSeconds(2f);


                }
                //else
                //{
                //    stopped = true;
                //    StartNextWave();
                //}
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void SpawnEnemy(int enemyLevel)
    {

        EnemyMovement enemyPrefab;

        switch (enemyLevel)
        {
            case 1:
                enemyPrefab = enemyLevel1Prefab;
                break;

            case 2:
                enemyPrefab = enemyLevel2Prefab;
                break;

            case 3:
                enemyPrefab = enemyLevel3Prefab;
                break;
            default:
                enemyPrefab = enemyLevel1Prefab;
                break;
        }

        GameObject enemyClone = Instantiate(enemyPrefab.gameObject, transform.position, Quaternion.identity) as GameObject;
        enemyClone.transform.parent = gameObject.transform;
    }
}
