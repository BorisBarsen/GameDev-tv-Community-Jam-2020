using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] float gunRange = 30;

    Transform objectToPan;
    GameObject targetEnemy;

    ParticleSystem gun;

	// Use this for initialization
	void Start () {
        objectToPan = transform.Find("Tower_A_Top");
        gun = objectToPan.gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ShootAtClosestEnemyInRange();
    }

    private void ShootAtClosestEnemyInRange()
    {
        targetEnemy = FindClosestEnemyInRange();

        if (targetEnemy != null)
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
