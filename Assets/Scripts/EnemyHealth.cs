using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] int health = 100;
    [SerializeField] int damagePerHit = 5;

    [SerializeField] GameObject deathEvent;

    bool dying = false;

    private void OnParticleCollision(GameObject other)
    {
        takeDamage();

        if (health <= 0)
        {
            if (!dying) KillEnemy();            
        }
    }

    private void takeDamage()
    {
        health -= damagePerHit;
    }

    public void KillEnemy()
    {
        dying = true;

        GameObject deathEventClone = Instantiate(deathEvent, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem particleSystem = deathEventClone.GetComponent<ParticleSystem>();
        AudioSource audioSource = deathEventClone.GetComponent<AudioSource>();

        
        particleSystem.Play();
        if (audioSource != null) audioSource.Play();

        Destroy(deathEventClone, Mathf.Max(audioSource.clip.length, particleSystem.main.duration) + 0.1f);
        Destroy(gameObject);
    }
}
