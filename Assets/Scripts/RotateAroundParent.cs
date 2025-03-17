using UnityEngine;

public class RotateAroundParent : MonoBehaviour
{
    public Transform parent; // Assign the parent in the inspector
    public float speed = 20f; // Rotation speed in degrees per second

    void Update()
    {
        if (parent != null)
        {
            // Rotate around the parent's position on the Y-axis
            transform.RotateAround(parent.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
