using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Season : MonoBehaviour {

    public Season next;

    [Tooltip("Temperature change per second")]
    public float temperature = 0f;

    [SerializeField] List<Texture> textures;
    [SerializeField] ParticleSystem friendlyTowerParticles;

    public ParticleSystem GetFriendlyBaseParticles()
    {
        return friendlyTowerParticles;
    }

    public List<Texture> GetTextures()
    {
        return textures;
    }
}
