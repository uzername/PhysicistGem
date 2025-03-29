using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenScript : MonoBehaviour
{
    public Canvas canvasAbout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartMainGameOnclick()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowInstructionsOnAbout()
    {
        canvasAbout.gameObject.SetActive( true );
    }
    public void HideInstructionsOnAbout()
    {
        canvasAbout.gameObject.SetActive( false );
    }
}
