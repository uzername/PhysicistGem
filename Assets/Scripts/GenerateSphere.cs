using GeometryOfSpheres;
using System.Buffers.Text;
using UnityEngine;

public class GenerateSphere : MonoBehaviour
{
    private IcosphereGenerator sphericalGenerator; 
    public int subdivisionsNumber = 3;
    public int radiusSphere = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphericalGenerator = new IcosphereGenerator();
        GeneratePrism();

    }
    /// <summary>
    /// generate sphere using Radius and Subdivisions
    /// </summary>
    private void makeSphere()
    {
        
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
