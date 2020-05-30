using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyBase : MonoBehaviour {

    [SerializeField] int lifes = 1;
    [SerializeField] SceneLoader sceneLoader; //TODO maybe call scene manager directly
    [SerializeField] ParticleSystem gameOverParticles;
    [SerializeField] Text lifesText;
    [SerializeField] Text gameOverPrompt;
    [SerializeField] EnemySpawner enemySpawner;

    private ParticleSystem seasonalParticles;

    // State
    public bool gameOver = false;
    bool continuesAvailable;

    public void SetSeasonalParticles(ParticleSystem particles)
    {
        seasonalParticles.Stop();
        seasonalParticles = particles;
        seasonalParticles.Play();
    }

    private void Start()
    {
        gameOverPrompt.enabled = false;
        lifesText.text = lifes.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        lifes--;
        lifesText.text = lifes.ToString();

        if (lifes <= 0 && !gameOver)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        if (seasonalParticles)
        {
            seasonalParticles.Stop();
        }
        gameOverParticles.Play();

        // Provide game over prompt and option to continue or restart this wave
        EnemySpawner.startAtWave = 2;
        Invoke("LoadGameOverScene", gameOverParticles.main.duration);        
    }

    private void LoadGameOverScene()
    {
        int continues = SceneLoader.continues;
        string gameOverMessage = "The base got destroyed!";

        if (continues > 0)
        {
            continuesAvailable = true;

            gameOverMessage += "\n\nYou have " + continues + " continues left." +
                "\n\n Press C to continue." +
                "\n Press R to start a new game.";
            gameOverPrompt.text = gameOverMessage;                           
            gameOverPrompt.enabled = true;

        }
        else
        {
            continuesAvailable = false;

            gameOverMessage += "\n You are out of continues!" +
                "\n\n Press R to reset to a new game.";
            gameOverPrompt.text = gameOverMessage;
            gameOverPrompt.enabled = true;
        }

        gameOver = true;
    }

    private void Update()
    {
        if(gameOver && continuesAvailable)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
               sceneLoader.Continue(enemySpawner.currentWave.id);
            }
        }
    }

}
