using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TextPopUp : MonoBehaviour
{
    public string[] textToShow;
    public TextMeshProUGUI text;
    Color textColor;
    Timer fadeInTimer = new Timer(.5f);
    Timer holdTimer = new Timer(3);
    Timer fadeOutTimer = new Timer(.5f);
    private void Awake()
    {
        textColor = text.color;
        textColor.a = 0;
        text.color = textColor;
    }

    public void ShowText(int textIndex)
    {
        CancelInvoke("TextFade");
        while (textIndex >= textToShow.Length)
        {
            textIndex -= textToShow.Length;
        }
        if(textIndex < 0)
            textIndex = 0;

        fadeInTimer.Reset();
        holdTimer.Reset();
        fadeOutTimer.Reset();
        text.text = textToShow[textIndex];
        InvokeRepeating("TextFade", 0, 0.02f);
    }

    void TextFade()
    {
        if (!fadeInTimer.Check(false))
        {
            textColor.a = Mathf.Lerp(0, 1, fadeInTimer.PercentComplete);
            text.color = textColor;
            return;
        }

        if (!holdTimer.Check(false))
        {
            textColor.a = 1;
            text.color = textColor;
            return;
        }

        if (!fadeOutTimer.Check(false))
        {
            textColor.a = Mathf.Lerp(1, 0, fadeOutTimer.PercentComplete);
            text.color = textColor;
            return;
        }

        textColor.a = 0;
        text.color = textColor;
        CancelInvoke("TextFade");
    }
}
