using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElement : MonoBehaviour {

    [Range (-3, 3)]
    [SerializeField] int temperature = 0;
    [SerializeField] ParticleSystem particleSystem;

    ParticleSystem.MainModule particleSystemMain;
    int oldTemp = 0;

	// Use this for initialization
	void Start () {
        particleSystemMain = particleSystem.main;
        particleSystemMain.startSpeed = 9f;
        particleSystemMain.simulationSpeed = 0.7f;
    }
	
	// Update is called once per frame
	void Update () {
        if (temperature < oldTemp && temperature >= -3)
        {
            oldTemp = temperature;
            TempDown();
        }
        else if (temperature > oldTemp && temperature <= 3) //TODO make magic number to limit
        {
            oldTemp = temperature;
            TempUp();
        }        
	}

    private void TempDown()
    {
        particleSystemMain.startSpeed = particleSystemMain.startSpeed.constant - 2;
        particleSystemMain.simulationSpeed -= 0.2f;
    }

    private void TempUp()
    {
        particleSystemMain.startSpeed = particleSystemMain.startSpeed.constant + 2;
        particleSystemMain.simulationSpeed += 0.2f;
    }
}
