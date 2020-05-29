using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyBase : MonoBehaviour {

    [SerializeField] int lifes = 1;
    [SerializeField] SceneLoader sceneLoader; //TODO maybe call scene manager directly
    [SerializeField] ParticleSystem gameOverParticles;
    [SerializeField] Text lifesText;

    private ParticleSystem seasonalParticles;

    // State
    bool gameOver = false;

    public void SetSeasonalParticles(ParticleSystem particles)
    {
        seasonalParticles.Stop();
        seasonalParticles = particles;
        seasonalParticles.Play();
    }

    private void Start()
    {
        lifesText.text = lifes.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        lifes--;
        lifesText.text = lifes.ToString();

        if (lifes <= 0 && !gameOver)
        {
            TriggerGameOver();
            gameOver = true;
        }
    }

    private void TriggerGameOver()
    {
        print("Game Over!");
        if (seasonalParticles)
        {
            seasonalParticles.Stop();
        }
        gameOverParticles.Play();

        //Invoke("LoadGameOverScene", particleSystem.main.duration);        
    }

    private void LoadGameOverScene()
    {
        sceneLoader.GameOver();
    }

}
