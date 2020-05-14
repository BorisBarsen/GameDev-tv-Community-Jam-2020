using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [Range(1f, 20f)]
    [SerializeField] float gridSize = 10f;
    // Update is called once per frame
    void Update()
    {
        Vector3 snapPos;
        print(transform.position.x);
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);
    }
}
