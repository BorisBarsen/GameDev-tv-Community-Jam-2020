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
    [SerializeField] TowerFactory towerFactory;


    public struct Wave
    {
        public Queue<Vector2> enemies;
        public Vector4 towers;
        public string text;

        public Wave(Queue<Vector2> e, Vector4 t, string s)
        {
            enemies = e;
            towers = t;
            text = s;
        }
    }

    Queue<Wave> waves = new Queue<Wave>();
    Wave currentWave; //TODO hide
    Vector2 currentEnemy; //TODO hide

    int waveCounter = 0;

    //State//TODO hide
    public bool stopped;

	// Use this for initialization
	void Start () {
        InitializeWaves();
        StartNextWave();

        StartCoroutine(RunSpawner());
    }

    void InitializeWaves()
    {
        waves = new Queue<Wave>(new[] 
        {
            // Wave 1
            new Wave(new Queue<Vector2>
                (new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                    }),

                    new Vector4(1, 0, 0 ,0),

                    "Welcome to Skull equals True!"
                ),
            
            // Wave 2
            new Wave(new Queue<Vector2>
                (new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1f),
                        new Vector2(1, 1f),
                        new Vector2(1, 1f)
                    }),

                    new Vector4(1, 0, 0 ,0),

                    "End of wave 2!"
                ),

                        // Wave 3
            new Wave(new Queue<Vector2>
                (new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 3.5f),
                        new Vector2(2, 1f),
                        new Vector2(1, 0.5f),
                        new Vector2(1, 1f)
                    }),

                    new Vector4(1, 1, 0 ,0),

                    "End of wave 3\n 1+ Frost Tower\n\n Frost towers lower enemy temperature and slows them down."
                )
        });

    }

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
        stopped = true;

        currentWave = waves.Dequeue();

        endOfWavePrompt.text = currentWave.text + "\n\n Press 'SPACE' to start next wave.";

        endOfWavePrompt.enabled = true;

        towerFactory.SetTowerLimits(currentWave.towers);
    }

    IEnumerator RunSpawner()
    {
        while (true)
        {
            if (!stopped)
            { 
                if (currentWave.enemies.Count > 0)
                {
                    currentEnemy = currentWave.enemies.Dequeue();
                    print(currentEnemy);
                    SpawnEnemy((int)currentEnemy[0]);
                    yield return new WaitForSeconds(currentEnemy.y);

                }
                else
                {
                    StartNextWave();
                    yield return new WaitForSeconds(2f);
                }
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
