using System.Collections.Generic;
using UnityEngine;
// https://www.youtube.com/watch?v=13KrnisMf14&t=57s
// http://hyperphysics.phy-astr.gsu.edu/hbase/traj.html#tra2
public class DrawTrajectory : MonoBehaviour
{

    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    [Range(3, 30)]
    private int _lineSegmentCount = 20;
    private List<Vector3> _linePoints = new List<Vector3>();

    #region Singleton
    public static DrawTrajectory Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public void UpdateTrajectory(Vector3 forceVector, float rigidBodyMass, Vector3 startingPoint)
    {
        // https://discussions.unity.com/t/calculating-velocity-from-addforce-and-mass/495144/3
        // initial speed
        Vector3 velocity = (forceVector / rigidBodyMass) * Time.fixedDeltaTime;
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;
         float stepTime = FlightDuration / _lineSegmentCount;
        _linePoints.Clear();
        //Debug.Log("=== DRAW AIM LINE ===");
        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; //change in time
            Vector3 MovementVector = new Vector3(
                x: velocity.x * stepTimePassed,
                y: velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                z: velocity.z * stepTimePassed
            );
            //Debug.Log(-MovementVector + startingPoint);
            _linePoints.Add(item: -MovementVector + startingPoint);
        }
        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
