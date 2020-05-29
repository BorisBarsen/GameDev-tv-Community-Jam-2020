using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {


    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem gun;

    [SerializeField] float gunRange = 30;

    [Tooltip("In s")]
    [SerializeField] [Range(0f, 60f)] float cooldown = 30;

    [SerializeField] float cooldownTimer = 0;
    [SerializeField] float tempChange = 0f;

    public Waypoint baseWaypoint;

    private string element;

    public void SetElement(string name)
    {
        element = name;
    }

    public string GetElement()
    {
        return element;
    }

    // State
    GameObject targetEnemy;

    public void remove() //TODO never called?
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        DecreaseCooldown();

        var closestEnemyInRange = FindClosestEnemyInRange();
        if (cooldownTimer == 0 && closestEnemyInRange)
        {
            gun.Play();
            objectToPan.LookAt(closestEnemyInRange.transform);
            cooldownTimer = cooldown;
        }
        //else
        //{
        //    gun.Stop();
        //}
    }

    private void DecreaseCooldown()
    {
        if (cooldownTimer == 0) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0) cooldownTimer = 0;
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

    public float GetTempChange()
    {
        return tempChange;
    }

    private void OnMouseOver()
    {
        print("Hello");
        if (Input.GetMouseButtonDown(1))
        {
            transform.parent.GetComponent<TowerFactory>().RemoveTower(this);
            baseWaypoint.isPlaceable = true;
        }
    }
}
