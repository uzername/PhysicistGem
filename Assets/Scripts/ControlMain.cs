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
}
