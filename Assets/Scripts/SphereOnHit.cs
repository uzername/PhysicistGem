using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereOnHit : MonoBehaviour
{
    public bool UseColors;
    public GameObject rootOfSegments;
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
            StaticConstants.SegmentsHit++;
            StaticConstants.SegmentsLeft--;
            string segmentMaterialName = collision.gameObject.transform.GetComponent<MeshRenderer>().material.name;
            if ((segmentMaterialName.Equals(ballMaterialName, StringComparison.InvariantCultureIgnoreCase)) || (UseColors == false))
            {
                Destroy(collision.gameObject);
                //bool allSegmentsDestroyed = (rootOfSegments?.gameObject.transform.childCount == 0);
                bool allSegmentsDestroyed = (StaticConstants.SegmentsLeft == 0);
                if (allSegmentsDestroyed)  {
                    // load scene as victory
                    Debug.Log($"You destroyed all the segments using {Assets.Scripts.StaticConstants.UsedSpheres} spheres!");
                    SceneManager.LoadScene(2);
                }
            }
        }
    }
}
