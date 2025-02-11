using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectileMy : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchPoint;
    public float launchVelocity = 700f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (launchPoint == null)
            {
                Debug.Log("launch point not defined");
            }
            GameObject ball = Instantiate(projectile,
            launchPoint.position, launchPoint.rotation);

            ball.GetComponent<Rigidbody>().AddRelativeForce(new
            Vector3(launchVelocity,0,0));
        }
    }
}
