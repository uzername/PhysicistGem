using System;
using UnityEngine;

public class SphereOnHit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        string ballMaterialName = collision.collider.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name;
        if (collision.gameObject.name.StartsWith("SegmentOfSphere"))
        {
            string segmentMaterialName = collision.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name;
            Destroy(collision.gameObject);
        }
    }
}
