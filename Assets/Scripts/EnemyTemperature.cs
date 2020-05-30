using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemperature : MonoBehaviour {

    //enum

    [Range (-3, 3)]
    [SerializeField] float temperature = 0;
    [SerializeField] ParticleSystem particleSystem;
    SeasonalCycle seasonalCycle;

    [SerializeField] float hitChangeFactor;

    [Range(0, 1)]
    [SerializeField] float tempChangeFactor = 1f;
    [SerializeField] Vector2 temperatureRange = new Vector2(-3f, 3f);
    [SerializeField] Vector2 particleStartSpeedRange = new Vector2(3f, 15f);
    [SerializeField] Vector2 particleSimulationSpeedRange = new Vector2(0.1f, 1.3f);

    [SerializeField]
    float seasonalTemp = 0f; 

    ParticleSystem.MainModule particleSystemMain;
    float oldTemp = 0;

    float particleStartSpeed = 9f;
    float particleSimulationSpeed = 0.7f;

    float normalizedTemp;
    float tempLerpValue;

    // Status effects
    public bool wet;


    // Use this for initialization
    void Start () {
        seasonalCycle = FindObjectOfType<SeasonalCycle>();

        particleSystemMain = particleSystem.main;
        particleSystemMain.startSpeed = particleStartSpeed;
        particleSystemMain.simulationSpeed = particleSimulationSpeed;
        seasonalTemp = seasonalCycle.temperature;
        StartCoroutine(SeasonalTempChange());
    }

    IEnumerator SeasonalTempChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ChangeTemp(seasonalCycle.temperature);
        }
    }

    public void ChangeTemp(float amount)
    {
        if (amount < 0 && wet) amount *= 2;

        temperature += amount * tempChangeFactor;

        if (temperature < temperatureRange.x) temperature = temperatureRange.x;
        //if (temperature > temperatureRange.y) temperature = temperatureRange.y;//so it can explode after max reached

        if (temperature >= temperatureRange.y)
        {
            GetComponent<EnemyHealth>().KillEnemy();
        }
        else
        {
            normalizedTemp = (temperature - temperatureRange.x) / (temperatureRange.y - temperatureRange.x);
            GetComponent<EnemyMovement>().SetSpeedFactor(normalizedTemp);

            LerpParticleSettings();

            particleSystemMain.startSpeed = particleStartSpeed;
            particleSystemMain.simulationSpeed = particleSimulationSpeed;
        }

    }

    private void LerpParticleSettings()
    {
        particleStartSpeed = Mathf.Lerp(particleStartSpeedRange.x, particleStartSpeedRange.y, normalizedTemp);
        particleSimulationSpeed = Mathf.Lerp(particleSimulationSpeedRange.x, particleSimulationSpeedRange.y, normalizedTemp);
    }
}
