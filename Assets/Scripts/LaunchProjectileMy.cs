using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectileMy : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchPoint;

    public bool launchOnClick = false;
    public float launchVelocity = 700f;
    // launch angle and initial direction of launch is defined by launchPoint Position and Rotation

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && launchOnClick)
        {
            performLaunching();
        }
    }
    public void performLaunching()
    {
        if (launchPoint == null)
        {
            Debug.Log("launch point not defined");
        }
        GameObject ball = Instantiate(projectile,
        launchPoint.position, launchPoint.rotation);

        ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(launchVelocity, 0, 0));
    }
}
