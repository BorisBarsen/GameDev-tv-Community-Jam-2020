using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Season : MonoBehaviour {

    public Season next;

    [SerializeField] List<Texture> textures;

    public List<Texture> GetTextures()
    {
        return textures;
    }
}
