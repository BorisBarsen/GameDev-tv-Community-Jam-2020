using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] GameObject deathEvent;
    EnemyTemperature temperature;

    bool dying = false;

    private void Start()
    {
        temperature = GetComponent<EnemyTemperature>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other)
        {
            temperature.ChangeTemp(other.GetComponentInParent<Tower>().GetTempChange()); //TODO have the rane that this script passes match the one used in the object
        }
    }


    public void KillEnemy()
    {
        dying = true;

        GetComponent<EnemyMovement>().Split(transform.position);

        GameObject deathEventClone = Instantiate(deathEvent, transform.position, Quaternion.identity) as GameObject;
        ParticleSystem particleSystem = deathEventClone.GetComponent<ParticleSystem>();
        AudioSource audioSource = deathEventClone.GetComponent<AudioSource>();

        particleSystem.Play();
        audioSource.Play();

        Destroy(deathEventClone, Mathf.Max(audioSource.clip.length, particleSystem.main.duration) + 0.1f);
        Destroy(gameObject);
    }
}
