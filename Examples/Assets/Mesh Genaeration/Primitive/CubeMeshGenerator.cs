using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
public class CubeMeshGenerator : MonoBehaviour
{
    [SerializeField] private bool _isCube;
    [SerializeField] private int xSize;
    [SerializeField] private int ySize;
    [SerializeField] private int zSize;

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
        SetVertices();
        SetTriangles();

        _mesh.RecalculateNormals();
        //SetNormals();
    }

    private void SetVertices()
    {
        int cornerCount = 8;
        int edgeCount = 0;
        int faceCount = 0;
        
        if (_isCube == false)
        {
            edgeCount = (xSize * ySize * zSize - 3) * 4;
            faceCount = (xSize - 1) * (ySize - 1) + (xSize - 1) * (zSize - 1) + (ySize - 1) * (zSize - 1);
            faceCount *= 2;
        }

        _vertices = new Vector3[cornerCount + edgeCount + faceCount];

        int vert = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++) // Front
            {
                _vertices[vert++] = new Vector3(x, y, 0);
            }

            for (int z = 1; z <= zSize; z++) // Right 
            {
                _vertices[vert++] = new Vector3(xSize, y, z);
            }

            for (int x = xSize - 1; x >= 0; x--) // Back
            {
                _vertices[vert++] = new Vector3(x, y, zSize);
            }

            for (int z = zSize - 1; z > 0; z--) // Left
            {
                _vertices[vert++] = new Vector3(0, y, z);
            }
        }

        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                _vertices[vert++] = new Vector3(x, ySize, z);
            }
        }

        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                _vertices[vert++] = new Vector3(x, 0, z);
            }
        }

        _mesh.vertices = _vertices;
    }

    private void SetTriangles()
    {
        var cellCount = xSize * zSize + ySize * zSize + xSize * zSize;
        cellCount *= 2;
        var triangles = new int[cellCount * 6];
        var loop = (xSize + zSize) * 2;
        int vi = 0, ti = 0;

        for (int y = 0; y < ySize; y++, vi++)
        {
            for (int i = 0; i < loop - 1; i++, vi++)
            {
                ti = SetCell(triangles, ti, vi, vi + loop, vi + 1, vi + loop + 1);
            }

            ti = SetCell(triangles, ti, vi, vi + loop, vi - loop + 1, vi + 1);
            
            if (_isCube)
            {
                ti = SetCell(triangles, ti, 0, 1, 3, 2);
                ti = SetCell(triangles, ti, 0 + loop, 3 + loop, 1 + loop, 2 + loop);
            }
        }

        _mesh.triangles = triangles;
    }

    private int SetCell(int[] triangles, int ti, int v00, int v01, int v10, int v11)
    {
        triangles[ti] = v00;
        triangles[ti + 1] = triangles[ti + 4] = v01;
        triangles[ti + 2] = triangles[ti + 3] = v10;
        triangles[ti + 5] = v11;
        return ti + 6;
    }

    private void OnDrawGizmosSelected()
    {
        if (_vertices == null)
            return;

        Gizmos.color = Color.red;

        for (int i = 0; i < _vertices.Length; i++)
        {
            Gizmos.DrawSphere(_vertices[i] + transform.position, 0.1f);
        }
    }
}