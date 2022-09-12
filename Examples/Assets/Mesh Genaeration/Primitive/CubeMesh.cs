using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : MonoBehaviour
{
    public float cubeLength;
    public float cubeWidth;
    public float cubeHeight;
    public MeshFilter meshFilter;
    private Mesh _mesh;

    private void Awake()
    {
        _mesh = new Mesh();
        _mesh.vertices = GetVertices();
        _mesh.triangles = GetTriangles();
        _mesh.normals = GetNormals();
        meshFilter.mesh = _mesh;
    }

    Vector3[] GetVertices()
    {
        var vertice0 = new Vector3(-cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
        var vertice1 = new Vector3(cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
        var vertice2 = new Vector3(cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
        var vertice3 = new Vector3(-cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
        var vertice4 = new Vector3(-cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
        var vertice5 = new Vector3(cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
        var vertice6 = new Vector3(cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
        var vertice7 = new Vector3(-cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
        var vertices = new Vector3[]
        {
// Bottom Polygon
            vertice0, vertice1, vertice2, vertice3,
// Vector3.left Polygon
            vertice7, vertice4, vertice0, vertice3,
// Vector3.forward Polygon
            vertice4, vertice5, vertice1, vertice0,
// Vector3.back Polygon
            vertice6, vertice7, vertice3, vertice2,
// Vector3.right Polygon
            vertice5, vertice6, vertice2, vertice1,
// Top Polygon
            vertice7, vertice6, vertice5, vertice4
        };

        return vertices;
    }

    private Vector3[] GetNormals()
    {

        var normals = new Vector3[]
        {
            // Bottom Side ReRnder
            Vector3.down, Vector3.down, Vector3.down, Vector3.down,

            // Vector3.left Side Render
            Vector3.left, Vector3.left, Vector3.left, Vector3.left,

            // Vector3.forward Side Render
            Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward,

            // Vector3.back Side Render
            Vector3.back, Vector3.back, Vector3.back, Vector3.back,

            // RIGTH Side Render
            Vector3.right, Vector3.right, Vector3.right, Vector3.right,

            // UP Side Render
            Vector3.up, Vector3.up, Vector3.up, Vector3.up
        };

        return normals;
    }

    private int[] GetTriangles()
    {
        var triangles = new int[]
        {
// Cube Bottom Side Triangles
            3, 1, 0,
            3, 2, 1,
// Cube Vector3.left Side Triangles
            3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
            3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
// Cube Vector3.forward Side Triangles
            3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
            3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
// Cube Vector3.back Side Triangles
            3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
            3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
// Cube Rigth Side Triangles
            3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
            3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
// Cube Top Side Triangles
            3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
            3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
        };
        return triangles;
    }
}