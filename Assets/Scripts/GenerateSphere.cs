using GeometryOfSpheres;
using NUnit.Framework.Internal;
using System.Buffers.Text;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GenerateSphere : MonoBehaviour
{
    private SharpIcosphereGenerator sphericalGenerator; 
    public int subdivisionsNumber = 1;
    public int radiusSphere = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphericalGenerator = new SharpIcosphereGenerator();
        var somethingReturned = sphericalGenerator.Generate(subdivisionsNumber);
        MakeSphereSharp(somethingReturned.Item1, somethingReturned.Item2);

    }
    private void MakeSphereSharp(List<Vector3> VerticesGenerated, List<int> IndicesGenerated)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = VerticesGenerated.ToArray();
        mesh.triangles = IndicesGenerated.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        
        Material newMat = Resources.Load("Materials/MeshMaterial", typeof(Material)) as Material;
        meshRenderer.material = newMat;
    }
    /// <summary>
    /// generate sphere and add it to scene, but this one if you use regular IcosphereGenerator. 
    /// Result we be smoothed out
    /// </summary>
    private void MakeSphere(List<Vector3> VerticesGenerated, List<int[]> FacesGenerated)
    {
        List<int> allTriangles = new List<int>();
        Mesh mesh = new Mesh();
        mesh.vertices = VerticesGenerated.ToArray();
        foreach (var itemSingleFace in FacesGenerated)  {
            for (int i = 0; i < itemSingleFace.Length; i++)   {
                allTriangles.Add(itemSingleFace[i]);
            }
        }
        mesh.triangles = allTriangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        //meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        Material newMat = Resources.Load("Materials/MeshMaterial", typeof(Material)) as Material;
        meshRenderer.material = newMat;
    }

    void GeneratePrism()
    {
        Vector3 baseA = new Vector3(-5, 3, -5);
        Vector3 baseB = new Vector3(5, 3, -5);
        Vector3 baseC = new Vector3(0, 3, 5);
        float height = 2f;

    Vector3 topA = baseA + Vector3.up * height;
        Vector3 topB = baseB + Vector3.up * height;
        Vector3 topC = baseC + Vector3.up * height;

        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            // Base
            baseA, baseB, baseC,
            // Top
            topA, topB, topC
        };

        mesh.triangles = new int[]
        {
// Base triangle (clockwise order)
        0, 1, 2,
        // Top triangle (clockwise order)
        3, 5, 4,
        // Side faces (fixing the winding order)
        0, 3, 4,  0, 4, 1,  // Side 1
        1, 4, 5,  1, 5, 2,  // Side 2
        2, 5, 3,  2, 3, 0   // Side 3
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        //meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        Material newMat = Resources.Load("Materials/MeshMaterial", typeof(Material)) as Material;
        meshRenderer.material = newMat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
