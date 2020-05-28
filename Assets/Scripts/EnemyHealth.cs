using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] int health = 100;
    [SerializeField] int damagePerHit = 5;

    [SerializeField] GameObject deathEvent;

    EnemyElement element;

    bool dying = false;

    private void Start()
    {
        element = GetComponent<EnemyElement>();
    }

    private void OnParticleCollision(GameObject other)
    {
        //takeDamage();
        //ice 0.3
        //fire 0.05
        element.ChangeTemp(other.GetComponentInParent<Tower>().GetTempChange()); //TODO have the rane that this script passes match the one used in the object

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
        audioSource.Play();

        Destroy(deathEventClone, Mathf.Max(audioSource.clip.length, particleSystem.main.duration) + 0.1f);
        Destroy(gameObject);
    }
}
