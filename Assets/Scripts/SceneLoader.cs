using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; //So you can use SceneManager


public class SceneLoader : MonoBehaviour
{
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            Reset();
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
