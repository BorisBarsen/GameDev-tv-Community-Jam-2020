using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] Text endOfWavePrompt;

    [SerializeField] EnemyMovement enemyLevel1Prefab;
    [SerializeField] EnemyMovement enemyLevel2Prefab;
    [SerializeField] EnemyMovement enemyLevel3Prefab;
    [SerializeField] TowerFactory towerFactory;
    [SerializeField] FriendlyBase friendlyBase;

    [SerializeField] int startAtWaveSetting = 1;

    //State 
    public bool stopped; //TODO hide
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

                    "Welcome to Skulls = True!"
                ),

            new Wave(
                    2,
                    
                    new Queue<Vector2>(new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 0.5f),
                        new Vector2(1, 0.5f),
                        new Vector2(1, 1f),
                        new Vector2(1, 1f)
                    }),

                    new Vector4(1, 0, 0 ,0),

                    "End of wave 1\n\n"
                ),

            new Wave(
                    3,
                
                    new Queue<Vector2>(new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(1, 4.5f),
                        new Vector2(2, 1f),
                    }),

                    new Vector4(1, 1, 0 ,0),

                    "End of wave 2\n\n +1 Frost Tower\n\n Frost towers lower the enemies temperature slowing them down."
                ),

             new Wave(
                    4,

                    new Queue<Vector2>(new[]
                    {
                        new Vector2(1, 1.5f),
                        new Vector2(1, 1.5f),
                        new Vector2(2, 12f),
                        new Vector2(2, 1f),
                    }),

                    new Vector4(1, 1, 0 ,0),

                    "End of wave 3\n\n The seasons affect the ambient temperature, try to use this to your advantage!"
                ),

              new Wave(
                    5,

                    new Queue<Vector2>(new[]
                    {
                        new Vector2(1, 2f),
                        new Vector2(1, 2f),
                        new Vector2(1, 6f),
                        new Vector2(2, 6f),
                        new Vector2(2, 1f),
                    }),

                    new Vector4(1, 1, 1 ,0),

                    "End of wave 4\n\n 1+ Water Tower\n\n Water towers apply the WET status effect for a short period of time.\n WET enmies cooling down twice as fast."
                ),

              new Wave(
                    6,

                    new Queue<Vector2>(new[]
                    {
                        new Vector2(1, 0.25f),
                        new Vector2(1, 0.25f),
                        new Vector2(1, 0.25f),
                        new Vector2(1, 3.25f),
                        new Vector2(2, 1f),
                        new Vector2(1, 0.25f),
                        new Vector2(1, 0.25f),
                        new Vector2(1, 0.25f),
                    }),

                    new Vector4(2, 1, 1 ,0),

                    "End of wave 5\n\n 1+ Fire Tower\n\n"
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
                    print(GameObject.FindGameObjectsWithTag("Enemy").Length);
                    if (!friendlyBase.gameOver && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) StartNextWave();

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
