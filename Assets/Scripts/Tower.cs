using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;

    ParticleSystem gun;

	// Use this for initialization
	void Start () {
        objectToPan = transform.Find("Tower_A_Top");
        targetEnemy = GameObject.Find("Enemy").transform;
        gun = objectToPan.gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        objectToPan.LookAt(targetEnemy);

        // Shoot
        if (Input.GetKeyDown("space"))
        {
            print("hello");
            gun.Play();
        }
	}
}
