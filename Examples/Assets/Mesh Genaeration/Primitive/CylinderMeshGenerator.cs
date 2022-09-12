using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
public class CylinderMeshGenerator : MonoBehaviour
{
    [SerializeField] private int ySize;
    [SerializeField] private int circlePoints;
    [SerializeField] private float radius;
    [SerializeField] private float sinLenght;
    [SerializeField] private int sinOffset;

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
    }

    private void SetVertices()
    {
        _vertices = new Vector3[ySize * circlePoints];

        int vert = 0;
        for (int y = 0; y < ySize; y++)
        {
            for (int circlePoint = 0; circlePoint < circlePoints; circlePoint++) // Front
            {
                float rads = 2 * Mathf.PI / circlePoints * circlePoint;
                _vertices[vert++] = new Vector3(
                    Mathf.Cos(rads) * radius + (float) Math.Sin(y / sinLenght) * sinOffset,
                    y,
                    Mathf.Sin(rads) * radius);
            }
        }

        _mesh.vertices = _vertices;
    }

    private void SetTriangles()
    {
        var cellCount = ySize * circlePoints;
        var triangles = new int[cellCount * 6];
        int vi = 0, ti = 0;

        for (int y = 0; y < ySize - 1; y++, vi++)
        {
            for (int i = 0; i < circlePoints - 1; i++, vi++)
            {
                ti = SetCell(triangles, ti, vi, vi + circlePoints, vi + 1, vi + circlePoints + 1);
            }

            ti = SetCell(triangles, ti, vi, vi + circlePoints, vi - circlePoints + 1, vi + 1);
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