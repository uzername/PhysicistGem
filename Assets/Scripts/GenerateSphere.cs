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
    /// <summary>
    /// how thick is the segment of hollow-out sphere. Should be larger than 1
    /// </summary>
    public float radiusDeltaPercent = 1.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphericalGenerator = new SharpIcosphereGenerator();
        var somethingReturned = sphericalGenerator.Generate(subdivisionsNumber);
        //MakeSphereSharp(somethingReturned.Item1, somethingReturned.Item2);
        MakeSphereSharpSegmented(somethingReturned.Item1, somethingReturned.Item2);
    }
    private void MoveTriangle(ref Vector3 A, ref Vector3 B, ref Vector3 C, Vector3 P, float d)
    {
        // Vector directed from P to A
        Vector3 direction = (A - P).normalized;

        // Move vertices by distance d
        A += direction * d;
        B += direction * d;
        C += direction * d;
    }
    /// <summary>
    /// generate multiple game objects aligned as a sphere
    /// </summary>
    /// <param name="obj">parent game object where to put it</param>
    /// <param name="VerticesGenerated"></param>
    /// <param name="IndicesGenerated"></param>
    private void MakeSphereSharpSegmented(List<Vector3> VerticesGenerated, List<int> IndicesGenerated)
    {
        int i = 0; int j = 0;
        while (i< VerticesGenerated.Count)  {
            GameObject obj = new GameObject($"SegmentOfSphere{i}");
            //obj.transform.parent = gameObject.transform;
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero; // Align with parent

            MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();

            Vector3 baseA = VerticesGenerated[i]* radiusSphere;
            Vector3 baseB = VerticesGenerated[i+1]* radiusSphere;
            Vector3 baseC = VerticesGenerated[i + 2] * radiusSphere;
            Vector3 topA = new Vector3 ( baseA.x, baseA.y, baseA.z) * radiusDeltaPercent;
            Vector3 topB = new Vector3( baseB.x, baseB.y, baseB.z) * radiusDeltaPercent;
            Vector3 topC = new Vector3(baseC.x, baseC.y, baseC.z) * radiusDeltaPercent;
            //if (j % 3 == 0)
            {
                MoveTriangle(ref baseA, ref baseB, ref baseC, new Vector3(0, 0, 0), 1.0f);
                MoveTriangle(ref topA, ref topB, ref topC, new Vector3(0, 0, 0), 1.0f);
            }
            Mesh mesh = new Mesh();
            mesh.vertices = new Vector3[]
            {
       // Base
        baseA, baseB, baseC,
        // Top
        topA, topB, topC,
        // Side faces (duplicated for sharp edges)
        baseA, baseB, topB, topA, // Side 1
        baseB, baseC, topC, topB, // Side 2
        baseC, baseA, topA, topC  // Side 3
            };

            mesh.triangles = new int[]
            {
        // Base triangle
        2, 1, 0,
        // Top triangle
        4, 5, 3,
        // Side faces (each face has its own unique vertices)
        6, 7, 8,  6, 8, 9,  // Side 1
        10, 11, 12,  10, 12, 13,  // Side 2
        14, 15, 16,  14, 16, 17   // Side 3
            };

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshFilter.mesh = mesh;
            Material newMat = Resources.Load("Materials/MeshMaterial", typeof(Material)) as Material;
            meshRenderer.material = newMat;
            j++;
            i += 3;
        }
    }
    /// <summary>
    /// adds solid ico-sphere to scene, non-smoothed
    /// </summary>
    /// <param name="VerticesGenerated"></param>
    /// <param name="IndicesGenerated"></param>
    private void MakeSphereSharp(List<Vector3> VerticesGenerated, List<int> IndicesGenerated)
    {
        Mesh mesh = new Mesh();
        List<Vector3> VerticesCopy = VerticesGenerated;
        for (int i=0; i< VerticesCopy.Count; i++)
        {
            VerticesCopy[i]*=radiusSphere;
        }
        mesh.vertices = VerticesCopy.ToArray();
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
