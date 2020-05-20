using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] int health = 100;
    [SerializeField] int damagePerHit = 5;

    private void OnParticleCollision(GameObject other)
    {
        takeDamage();

        if (health < 0)
        {
            DeathEvent();            
        }
    }

    private void takeDamage()
    {
        health -= damagePerHit;
    }

    private void DeathEvent()
    {
        GameObject deathEventClone = Instantiate(GameObject.Find("Death Event"), transform.position, transform.rotation) as GameObject;
        ParticleSystem particleSystem = deathEventClone.GetComponent<ParticleSystem>();
        AudioSource audioSource = deathEventClone.GetComponent<AudioSource>();

        particleSystem.Play();
        audioSource.Play();

        Destroy(deathEventClone, Mathf.Max(audioSource.clip.length, particleSystem.main.duration) + 0.1f);
        Destroy(gameObject);
    }
}
