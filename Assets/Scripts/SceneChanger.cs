using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Image fadeImg;
    public float fadeDur = 1.0f;
    public bool fadeIn = false, fadeOut = false;

    public Color fadeColor;

    int sceneToChangeTo;
    Timer timer;

    private void Start()
    {
        timer = new Timer(fadeDur);
        if (fadeIn)
        {
            fadeColor.a = 1;
            fadeImg.enabled = true;
        }
        else
        {
            fadeColor.a = 0;
            fadeImg.enabled = false;
        }
        fadeImg.color = fadeColor;
    }

    void Update()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        else if (fadeOut)
        {
            FadeOut();
        }
        else
        {
            fadeColor.a = 0;
            timer.Reset();
        }


        fadeImg.color = fadeColor;
    }

    void FadeIn()
    {
        if (!timer.Check())
        {
            fadeColor.a = Mathf.Lerp(1, 0, timer.PercentComplete);
            return;
        }

        fadeColor.a = 0;
        fadeIn = false;
    }

    void FadeOut()
    {
        if (!timer.Check())
        {
            fadeColor.a = Mathf.Lerp(0, 1, timer.PercentComplete);
            return;
        }

        fadeColor.a = 1;
        fadeOut = false;

        if (sceneToChangeTo == -1)
        {
            Application.Quit();
            return;
        }

        SceneManager.LoadScene(sceneToChangeTo);
    }

    public void LoadScene(int SceneToChangeTo)
    {
        sceneToChangeTo = SceneToChangeTo;
        fadeIn = false;
        fadeOut = true;
        fadeImg.enabled = true;
    }
}
