﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; //So you can use SceneManager


public class SceneLoader : MonoBehaviour
{
    public static int continues = 3;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r"))
        {
            Reset();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Reset()
    {
        EnemySpawner.startAtWave = 1;
        continues = 3;
        SceneManager.LoadScene(1);
    }

    public void Continue(int startAtLevel)
    {
        continues--;
        EnemySpawner.startAtWave = startAtLevel;
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
