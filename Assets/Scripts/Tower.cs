using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {


    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem gun;

    [SerializeField] float gunRange = 30;

    // State
    GameObject targetEnemy;

    public void remove()
    {
        Destroy(gameObject);
    }


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update ()
    {
        ShootAtClosestEnemyInRange();
    }

    private void ShootAtClosestEnemyInRange()
    {
        targetEnemy = FindClosestEnemyInRange();

        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy.transform);
            gun.Play(); 
        }
        else
        {
            gun.Stop();
        }
    }

    private GameObject FindClosestEnemyInRange()
    {
        GameObject closestEnemyInRange = null;
        float distanceToClosestEnemy = float.MaxValue;
        float distanceToCurrentEnemy = float.MaxValue;
    
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) //Adding new objects with enemy tag throws "enumeration operation may not execute", us copy of list for quick hack 
        {
            distanceToCurrentEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToCurrentEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToCurrentEnemy;

                if (Vector3.Distance(transform.position, enemy.transform.position) <= gunRange)
                {
                    closestEnemyInRange = enemy;
                }
            }
        }
        
        return closestEnemyInRange;
    }
}
