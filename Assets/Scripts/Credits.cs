using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Credits : MonoBehaviour {

    [SerializeField] Text text;

	// Use this for initialization
	void Start () {
        text.text = "Created by: Barsen\n\n" +
            "Design ideas and emotional support: Teodor Stoilov\n\n" +
            "Music:\n\"Hand Balance Redux\" Kevin MacLeod (incompetech.com)" +
            "Licensed under Creative Commons: By Attribution 4.0 License\n" +
            "http://creativecommons.org/licenses/by/4.0/" +
            "\n\nCreated during the 'GameDev.tv Community Jam' in 2020 by adapting the 'Realm Rush' project of the 'Complete C# Unity Developer 3D course'." +
            "\n\n Press 'SPACE' to get back to continue";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
	}
}
