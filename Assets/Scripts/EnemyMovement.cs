using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyMovement : MonoBehaviour {

    [SerializeField] GameObject agentEnemyPrefab;
    [SerializeField] int splitAmount = 2;

    [SerializeField] float baseSpeed = 20f;
    [Range(0f, 1f)]
    [SerializeField] float speedFactor = 1f;
    [SerializeField] float stunDuration;

    [SerializeField] ParticleSystem stunParticles;

    List<Waypoint> path;
    Vector3 target;

    bool dying = false;

    // Status effect 
    float stunTimer; // removed in the same frame it was recieved

    // Counter
    int waypointsPassed = 0;

    public void SetSpeedFactor(float factor)
    {
        if (speedFactor < 0 || speedFactor > 1) Debug.LogWarning("Speed factor set out of range!(" + factor + ")");

        speedFactor = factor;
    }

    public void Stun()
    {
        stunParticles.Play();
        stunTimer = stunDuration;
    }

    // Use this for initialization
    void Start () {
        stunTimer = 0;
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        path = pathfinder.GetPath();
        if (waypointsPassed < 0) waypointsPassed = 0; // Just for safety
        if (waypointsPassed < 0) waypointsPassed = 0; // Just for safety
        target = path[++waypointsPassed].transform.position;
    }

    public void Split(Vector3 masterPosition)
    {
        if (agentEnemyPrefab)
        {
            for (int i = 0; i < splitAmount; i++)
            {
                var enemyChild = Instantiate(agentEnemyPrefab, masterPosition + new Vector3(i*-10, 0f, i*10), Quaternion.identity);
                enemyChild.GetComponent<EnemyMovement>().InheritValues(target, waypointsPassed);
            }
        }
    }

    private void Update()
    {
        if (stunTimer == 0)
        {
            if (stunParticles) stunParticles.Stop();

            float step = baseSpeed * speedFactor * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                if (waypointsPassed == path.Count)
                {
                    GetComponent<EnemyHealth>().KillEnemy();
                }
                else
                {
                    target = path[waypointsPassed++].transform.position;
                }
            }
        }
        else //Stunned
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer < 0) stunTimer = 0;
        }
    }

    private void InheritValues(Vector3 position, int afterWaypoint)
    {
        target = position;
        waypointsPassed = afterWaypoint - 2;
    }
}
