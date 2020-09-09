using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMap : MonoBehaviour
{
    public Image mapPiece1;
    public Image mapPiece2;
    public Image mapPiece3;
    public Image mapPiece4;


    Timer showLength;
    Timer timerStall = new Timer(.2f);
    bool hideByTime = false;
    bool mapActive = false;

    public void RevealMapPiece(int mapPiece)
    {
        switch (mapPiece)
        {
            case 1:
                mapPiece1.enabled = true;
                break;
            case 2:
                mapPiece2.enabled = true;
                break;
            case 3:
                mapPiece3.enabled = true;
                break;
            case 4:
                mapPiece4.enabled = true;
                break;
        }

        ShowMapPieces(3);
    }

    void ShowMapPieces(float time = 0)
    {
        if(time != 0)
        {
            showLength = new Timer(time);
            hideByTime = true;
        }
        timerStall.Reset();

        mapPiece1.gameObject.SetActive(true);
        mapPiece2.gameObject.SetActive(true);
        mapPiece3.gameObject.SetActive(true);
        mapPiece4.gameObject.SetActive(true);

        mapActive = true;
    }
    void HideMapPieces()
    {
        hideByTime = false;
        timerStall.Reset();

        mapPiece1.gameObject.SetActive(false);
        mapPiece2.gameObject.SetActive(false);
        mapPiece3.gameObject.SetActive(false);
        mapPiece4.gameObject.SetActive(false);

        mapActive = false;
    }

    private void Update()
    {
        if (!mapActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowMapPieces();
            }
            return;
        }

        if (hideByTime)
        {
            if (showLength.Check() || Input.GetKeyDown(KeyCode.E))
            {
                HideMapPieces();
            }
            return;
        }

        if (!timerStall.Check(false))
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            HideMapPieces();
        }
    }
}
