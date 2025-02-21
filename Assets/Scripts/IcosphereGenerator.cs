using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics; // Requires System.Numerics for Vector3, but it is available in UnityEngine too

/// <summary>
/// generate icosphere for pondering. This code was created by ChatGPT
/// </summary>
class IcosphereGenerator
{
    private struct Triangle
    {
        public int V1, V2, V3;
        public Triangle(int v1, int v2, int v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }
    }

    private List<Vector3> vertices;
    private List<Triangle> faces;
    private Dictionary<(int, int), int> midpointCache;

    public IcosphereGenerator()
    {
        vertices = new List<Vector3>();
        faces = new List<Triangle>();
        midpointCache = new Dictionary<(int, int), int>();
    }

    public (List<Vector3> Vertices, List<int[]> Faces) Generate(int subdivisions)
    {
        CreateIcosahedron();
        for (int i = 0; i < subdivisions; i++)
        {
            Subdivide();
        }

        // Convert faces to an array of integer indices
        List<int[]> faceIndices = faces.Select(f => new int[] { f.V1, f.V2, f.V3 }).ToList();
        return (vertices, faceIndices);
    }

    private void CreateIcosahedron()
    {
        vertices.Clear();
        faces.Clear();

        float t = (float)((1.0 + Math.Sqrt(5.0)) / 2.0); // Golden ratio

        // Create 12 vertices of an icosahedron
        vertices.AddRange(new Vector3[]
        {
            new(-1,  t,  0), new(1,  t,  0), new(-1, -t,  0), new(1, -t,  0),
            new(0, -1,  t), new(0,  1,  t), new(0, -1, -t), new(0,  1, -t),
            new(t,  0, -1), new(t,  0,  1), new(-t,  0, -1), new(-t,  0,  1)
        });

        // Normalize to unit sphere
        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] = Vector3.Normalize(vertices[i]);
        }

        // Define 20 triangular faces
        faces.AddRange(new Triangle[]
        {
            new(0, 11, 5), new(0, 5, 1), new(0, 1, 7), new(0, 7, 10), new(0, 10, 11),
            new(1, 5, 9), new(5, 11, 4), new(11, 10, 2), new(10, 7, 6), new(7, 1, 8),
            new(3, 9, 4), new(3, 4, 2), new(3, 2, 6), new(3, 6, 8), new(3, 8, 9),
            new(4, 9, 5), new(2, 4, 11), new(6, 2, 10), new(8, 6, 7), new(9, 8, 1)
        });
    }

    private void Subdivide()
    {
        var newFaces = new List<Triangle>();
        midpointCache.Clear();

        foreach (var tri in faces)
        {
            int a = GetMidpoint(tri.V1, tri.V2);
            int b = GetMidpoint(tri.V2, tri.V3);
            int c = GetMidpoint(tri.V3, tri.V1);

            newFaces.Add(new Triangle(tri.V1, a, c));
            newFaces.Add(new Triangle(tri.V2, b, a));
            newFaces.Add(new Triangle(tri.V3, c, b));
            newFaces.Add(new Triangle(a, b, c));
        }

        faces = newFaces;
    }

    private int GetMidpoint(int v1, int v2)
    {
        var edgeKey = (Math.Min(v1, v2), Math.Max(v1, v2));
        if (midpointCache.TryGetValue(edgeKey, out int midpointIndex))
        {
            return midpointIndex;
        }

        Vector3 midpoint = Vector3.Normalize((vertices[v1] + vertices[v2]) / 2f);
        midpointIndex = vertices.Count;
        vertices.Add(midpoint);
        midpointCache[edgeKey] = midpointIndex;
        return midpointIndex;
    }
}