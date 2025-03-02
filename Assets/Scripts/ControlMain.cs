using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ControlMain : MonoBehaviour
{
    /// <summary>
    /// slider used to toggle between walk mode and cannon mode
    /// </summary>
    public Slider mainSlider;
    /// <summary>
    /// Canvas that holds controls to move around
    /// </summary>
    public Canvas translocationalCanvas;
    /// <summary>
    /// Canvas that holds control controls for cannon
    /// </summary>
    public Canvas cannonCanvas;
    /// <summary>
    /// slider used to adjust angle
    /// </summary>
    public Slider AngleSlider;
    /// <summary>
    /// a point that rotates barrel
    /// </summary>
    public Transform BarrelTransform;
    /// <summary>
    /// Slider used to control power
    /// </summary>
    public Slider PowerSlider;
    /// <summary>
    /// Lets show and hide line used for targeting
    /// </summary>
    public GameObject LineTargeting;
    private const byte WALK = 0;
    private const byte LAUNCH = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Invoked when the value of the slider changes.
    /// Toggle mode : 0 to move around, 1 to setup launcher
    /// </summary>
    public void ModeToggle()
    {
        switch (mainSlider.value)
        {
            case WALK:
                {
                    if ((translocationalCanvas != null)&&(cannonCanvas!=null))  {
                        translocationalCanvas.gameObject.SetActive( true );
                        cannonCanvas.gameObject.SetActive( false );
                        if (LineTargeting!=null)  {
                            LineTargeting.gameObject.SetActive( false );
                        }
                    }
                    break;
                }
            case LAUNCH:
                {
                    if ((translocationalCanvas != null) && (cannonCanvas != null))
                    {
                        translocationalCanvas.gameObject.SetActive(false);
                        cannonCanvas.gameObject.SetActive( true );
                        if (LineTargeting != null)  {
                            LineTargeting.gameObject.SetActive(true);
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }
    /// <summary>
    /// re-draw trajectory. Oh my, it has so much geometry and physics, this is unbelievable
    /// </summary>
    private void reComputeTrajectory()
    {
        float rawLaunchPower = BarrelTransform.GetComponentInParent<LaunchProjectileMy>().launchVelocity;
        // it is hard to say where exception may happen, but it may happen
        float rawMass= BarrelTransform.GetComponentInParent<LaunchProjectileMy>().projectile.GetComponent<Rigidbody>().mass;
        Transform launchPosition = BarrelTransform.Find("LaunchOrigin");
        Vector3 localForce = new Vector3(rawLaunchPower, 0, 0);
        // ChatGPT helped me to apply rotation of launchPosition to vector force
        Vector3 worldForce = launchPosition.TransformDirection(localForce);
        (BarrelTransform.GetComponentInParent<DrawTrajectory>() as DrawTrajectory).UpdateTrajectory(worldForce, rawMass, launchPosition.position);
    }

    public void ChangeAngle()  {
        if (AngleSlider == null || BarrelTransform == null) {
            Debug.LogError("Cannot change angle, complete init of ControlMain::ChangeAngle");
            return; 
        }
        float angleValue = AngleSlider.value;
        BarrelTransform.transform.rotation = Quaternion.Euler(0, 0, angleValue);
        reComputeTrajectory();
    }
    public void ChangePower()
    {
        if (PowerSlider == null || BarrelTransform == null)
        {
            Debug.LogError("Cannot change power, complete init of ControlMain::ChangePower");
            return;
        }
        BarrelTransform.GetComponentInParent<LaunchProjectileMy>().launchVelocity = PowerSlider.value;
        reComputeTrajectory();
    }
}
