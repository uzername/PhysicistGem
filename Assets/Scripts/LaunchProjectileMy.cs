using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaunchProjectileMy : MonoBehaviour
{
    [Range(1, 3)]
    public int GeneratorNumberSegments = 3;
    public GameObject projectile;
    public Transform launchPoint;
    public TextMeshProUGUI usedOrbsLbl;

    public bool launchOnClick = false;
    public float launchVelocity = 700f;
    // launch angle and initial direction of launch is defined by launchPoint Position and Rotation

    void Update()
    {
        // it won't be called
        if (Input.GetButtonDown("Fire1") && launchOnClick)
        {
            performLaunching();
        }
    }
    public void performLaunching()
    {
        if (launchPoint == null)
        {
            Debug.LogError("launch point not defined in performLaunching()");
            return;
        }
        Assets.Scripts.StaticConstants.UsedSpheres++;
        usedOrbsLbl.text = $"{Assets.Scripts.StaticConstants.UsedSpheres}";
        GameObject ball = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
        int chosenSegment = Random.Range(1, GeneratorNumberSegments + 1);
        Material newMat = Resources.Load($"Materials/MeshMaterial{chosenSegment}", typeof(Material)) as Material;
        ball.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = newMat;
        ball.gameObject.GetComponent<SphereOnHit>().rootOfSegments = GameObject.Find("PrismSample");
        ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(launchVelocity, 0, 0));
    }
}
