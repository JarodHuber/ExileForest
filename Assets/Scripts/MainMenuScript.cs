using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ValueHolder.GameOver = false;
        ValueHolder.GameWin = false;
        ValueHolder.HasKey = false;
    }
}
