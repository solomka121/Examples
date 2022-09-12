using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAn : MonoBehaviour
{
    [SerializeField] private MeshFilter _mesh;
    void Start()
    {
        Debug.Log(_mesh.mesh.vertices.Length);   
    }
}
