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
        if (collision.gameObject.name.StartsWith("SegmentOfSphere"))
        {
            Destroy(collision.gameObject);
        }
    }
}
