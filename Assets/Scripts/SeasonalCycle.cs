using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonalCycle : MonoBehaviour {

    [SerializeField] float timeBetweenSeasons = 5f;
    [SerializeField] GameObject friendlyBase;
    //[SerializeField] GameObject enemiesList;

    [SerializeField] Season winter; 
    [SerializeField] Season spring; 
    [SerializeField] Season summer; 
    [SerializeField] Season autumn;

    [SerializeField] Text seasonText;    

    Season season;

    float currentLerpTime;

    public float temperature = 0f;

    // Use this for initialization
    void Start () {
        season = autumn;
        StartNextSeason();
    }
	
	// Update is called once per frame
	void Update ()
    {
        LerpSeasons();
    }

    private void LerpSeasons()
    {
        currentLerpTime += Time.deltaTime;

        if (currentLerpTime > timeBetweenSeasons)
        {
            currentLerpTime = 0f;
            StartNextSeason();
        }

        float perc = currentLerpTime / timeBetweenSeasons;
        RenderSettings.skybox.SetFloat("_Blend", Mathf.Lerp(0f, 1f, perc));
    }

    private void StartNextSeason()
    {
        season.GetFriendlyBaseParticles().Stop();
        season = season.next;
        LoadNewSeason();
    }

    private void LoadNewSeason()
    {
        print("Loading " + season.name);
        temperature = season.temperature;
        SwitchTextures(season.GetTextures(), season.next.GetTextures());
        print(season);
        seasonText.text = season.name;
        var friendlyBaseParticles = season.GetFriendlyBaseParticles();
        friendlyBaseParticles.transform.position = friendlyBase.transform.position;
        friendlyBaseParticles.Play();
    }

    private void SwitchTextures(List<Texture> currentTextures, List<Texture> nextTextures)
    {
        RenderSettings.skybox.SetTexture("_FrontTex", currentTextures[2]);
        RenderSettings.skybox.SetTexture("_BackTex", currentTextures[0]);
        RenderSettings.skybox.SetTexture("_LeftTex", currentTextures[3]);
        RenderSettings.skybox.SetTexture("_RightTex", currentTextures[4]);
        RenderSettings.skybox.SetTexture("_UpTex", currentTextures[5]);
        RenderSettings.skybox.SetTexture("_DownTex", currentTextures[1]);

        RenderSettings.skybox.SetTexture("_FrontTex2", nextTextures[2]);
        RenderSettings.skybox.SetTexture("_BackTex2", nextTextures[0]);
        RenderSettings.skybox.SetTexture("_LeftTex2", nextTextures[3]);
        RenderSettings.skybox.SetTexture("_RightTex2", nextTextures[4]);
        RenderSettings.skybox.SetTexture("_UpTex2", nextTextures[5]);
        RenderSettings.skybox.SetTexture("_DownTex2", nextTextures[1]);
    }
}
