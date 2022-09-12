using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
public class PlaneMeshGenerator : MonoBehaviour
{
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;

    private Mesh _mesh;
    private MeshFilter _filter;
 
    private Vector3[] _vertices;

    private void Start()
    {
        _filter = GetComponent<MeshFilter>();
        _mesh = new Mesh();
        _filter.mesh = _mesh;
        CreateMesh();
    }

    private void CreateMesh()
    {
        _vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                _vertices[i] = new Vector3(x, y);
            }
        }

        int[] triangles = new int[xSize * ySize * 6];

        for (int y = 0 , ti = 0 , vi = 0; y < ySize; y++ , vi++)
        {
            for (int x = 0; x < xSize ; x++ , ti += 6 , vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = triangles[ti + 4] = xSize + vi + 1;
                triangles[ti + 2] = triangles[ti + 3] = vi + 1;
                triangles[ti + 5] = xSize + vi + 2; 

            }
        }
        
        _mesh.vertices = _vertices;
        _mesh.triangles = triangles;

        _mesh.RecalculateNormals();
    }

    private void OnDrawGizmosSelected()
    {
        if (_vertices == null)
            return;

        Gizmos.color = Color.red;
        
        for (int i = 0; i < _vertices.Length; i++)
        {
            Gizmos.DrawSphere(_vertices[i] + transform.position , 0.1f);
        }
        
    }
}