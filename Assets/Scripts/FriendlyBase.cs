using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyBase : MonoBehaviour {

    [SerializeField] int lifes = 1;
    [SerializeField] SceneLoader sceneLoader; //TODO maybe call scene manager directly
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] Text lifesText;

    // State
    bool gameOver = false;

    private void Start()
    {
        lifesText.text = lifes.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        print("The base is beeing attacked!");
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
        particleSystem.Play();

        //Invoke("LoadGameOverScene", particleSystem.main.duration);        
    }

    private void LoadGameOverScene()
    {
        sceneLoader.GameOver();
    }

}
