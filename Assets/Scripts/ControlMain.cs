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
    /// slider used to adjust angle
    /// </summary>
    public Transform BarrelTransform;
    /// <summary>
    /// Slider used to control power
    /// </summary>
    public Slider PowerSlider;
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
        Debug.Log(mainSlider.value);
        switch (mainSlider.value)
        {
            case WALK:
                {
                    if ((translocationalCanvas != null)&&(cannonCanvas!=null))  {
                        translocationalCanvas.gameObject.SetActive( true );
                        cannonCanvas.gameObject.SetActive( false );
                    }
                    break;
                }
            case LAUNCH:
                {
                    if ((translocationalCanvas != null) && (cannonCanvas != null))
                    {
                        translocationalCanvas.gameObject.SetActive(false);
                        cannonCanvas.gameObject.SetActive( true );
                    }
                    break;
                }
            default:
                break;
        }
    }


    public void ChangeAngle()  {
        if (AngleSlider == null || BarrelTransform == null) {
            Debug.LogError("Cannot change angle, complete init of ControlMain::ChangeAngle");
            return; 
        }
        float angleValue = AngleSlider.value;
        BarrelTransform.transform.rotation = Quaternion.Euler(0, 0, angleValue);
        
    }
    public void ChangePower()
    {
        if (PowerSlider == null || BarrelTransform == null)
        {
            Debug.LogError("Cannot change power, complete init of ControlMain::ChangePower");
            return;
        }
        BarrelTransform.GetComponentInParent<LaunchProjectileMy>().launchVelocity = PowerSlider.value;
    }
}
