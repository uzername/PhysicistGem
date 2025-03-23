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
        string ballMaterialName = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.name;
        if (collision.gameObject.name.StartsWith("SegmentOfSphere"))
        {
            string segmentMaterialName = collision.gameObject.transform.GetComponent<MeshRenderer>().material.name;
            if (segmentMaterialName.Equals(ballMaterialName,StringComparison.InvariantCultureIgnoreCase))
                Destroy(collision.gameObject);
        }
    }
}
