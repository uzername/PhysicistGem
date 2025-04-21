using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI TextSpheres;
    public TMPro.TextMeshProUGUI TextSegmentsHit;
    public TMPro.TextMeshProUGUI TextSegmentsLeft;
    public TMPro.TextMeshProUGUI TextQuote;
    public TMPro.TextMeshProUGUI TextQuoteAuthor;
    public Canvas StatsCanvas;
    public Canvas QuoteCanvas;
    private Tuple<String, String> pickedQuote;
    private bool statsCanvasStatus = true;
    private bool quoteCanvasStatus = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickedQuote = StaticRewards.GetRandomQuote();
        if (TextQuote != null)
            TextQuote.text = pickedQuote.Item1;
        if (TextQuoteAuthor != null) 
            TextQuoteAuthor.text = pickedQuote.Item2;

        if (TextSpheres != null) 
            TextSpheres.text = StaticConstants.UsedSpheres.ToString();
        if (TextSegmentsHit != null)
            TextSegmentsHit.text = StaticConstants.SegmentsHit.ToString();
        if (TextSegmentsLeft != null) 
            TextSegmentsLeft.text = StaticConstants.SegmentsLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleQuoteOnClick()
    {
        statsCanvasStatus = !statsCanvasStatus;
        quoteCanvasStatus = !quoteCanvasStatus;
        StatsCanvas.gameObject.SetActive(statsCanvasStatus);
        QuoteCanvas.gameObject.SetActive(quoteCanvasStatus);
    }
    public void StartMainGameOnclick()
    {
        StaticConstants.ResetConstants();
        SceneManager.LoadScene(1);
    }
}
