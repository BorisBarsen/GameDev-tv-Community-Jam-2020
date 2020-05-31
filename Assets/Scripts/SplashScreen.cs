using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    //state
    int screen = 0;

    [SerializeField] Text text;
    string startText;

	// Use this for initialization
	void Start () {
        startText = "Welcome to Hot Skulls" +
            "\n\nSave your base from the skull invasion by heating them up!" +
            "\n\nPress 'C' for CREDITS" +
            "\n Press 'SPACE' to START A NEW GAME";
        text.text = startText;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if(screen == 0)
            {
                screen++;
                text.text = "Controlls:" +
                    "\n\nLeft click - Place/Move towers" +
                    "\nRight click - Remove towers" +
                    "\nNumber Keys (1, 2, 3, 4) - Change tower type" +
                    "\n\nR - To RESET to a NEW GAME" +
                    "\nESC - To ESC for the MAIN MENU" +

                    "\n\n\n Press 'SPACE' to START THE BATTLE!";
            }
            else if (screen == 1)
            {
                SceneManager.LoadScene(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            screen = 0;
            text.text = startText;
        }

        if (Input.GetKeyDown(KeyCode.C) && screen == 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
