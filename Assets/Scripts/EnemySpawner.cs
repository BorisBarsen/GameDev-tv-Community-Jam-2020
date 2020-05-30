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
    [SerializeField] FriendlyBase friendlyBase;

    [SerializeField] int startAtWaveSetting = 1;

    //State//TODO hide
    public bool stopped;
    public static int startAtWave = 1;
    public Wave currentWave; //TODO hide

    Queue<Wave> waves = new Queue<Wave>();
    Vector2 currentEnemy;

    int waveCounter = 0;

    public struct Wave
    {
        public int id;
        public Queue<Vector2> enemies;
        public Vector4 towers;
        public string text;

        public Wave(int i, Queue<Vector2> e, Vector4 t, string s)
        {
            id = i;
            enemies = e;
            towers = t;
            text = s;
        }
    }



	// Use this for initialization
	void Start () {
        if(startAtWaveSetting != 1)
        {
            startAtWave = startAtWaveSetting;
        }

        InitializeWaves();
        StartNextWave();

        StartCoroutine(RunSpawner());
    }

    void InitializeWaves()
    {
        waves = new Queue<Wave>(new[] 
        {
            new Wave(
                    1,

                    new Queue<Vector2>(new[]
                        {
                            new Vector2(1, 1.5f),
                            new Vector2(1, 1.5f),
                            new Vector2(1, 1.5f),
                        }),

                    new Vector4(1, 0, 0 ,0),

                    "Welcome to Skull equals True!"
                ),

            new Wave(
                    2,
                    
                    new Queue<Vector2>(new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1f),
                        new Vector2(1, 1f),
                        new Vector2(1, 1f)
                    }),

                    new Vector4(1, 0, 0 ,0),

                    "End of wave 1!"
                ),

            new Wave(
                    3,
                
                    new Queue<Vector2>(new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 3.5f),
                        new Vector2(2, 1f),
                    }),

                    new Vector4(1, 1, 0 ,0),

                    "End of wave 2!\n\n 1 + Frost Tower\n\n Frost towers lower enemy temperature and slows them down."
                )
        });

        for (int i = 0; i < startAtWave - 1; i++)
        {
            waves.Dequeue();
        }

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
        
        if (waves.Count > 0)
        {
            currentWave = waves.Dequeue();

            endOfWavePrompt.text = currentWave.text + "\n\n Press 'SPACE' to start next wave.";

            endOfWavePrompt.enabled = true;

            towerFactory.SetTowerLimits(currentWave.towers);
        }
        else
        {
            //TODO add win screen, credits;
        }
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
                    SpawnEnemy((int)currentEnemy[0]);
                    yield return new WaitForSeconds(currentEnemy.y);

                }
                else
                {
                    if (!friendlyBase.gameOver && gameObject.transform.childCount == 1) StartNextWave();

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
