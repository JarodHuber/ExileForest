using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroller : MonoBehaviour
{
    public float creditSpeed = 5;

    RectTransform rT;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        rT = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rT.Translate(Vector2.up * creditSpeed);
    }
}
