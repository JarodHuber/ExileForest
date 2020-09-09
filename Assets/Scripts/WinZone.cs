using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ValueHolder.GameWin = true;
            GameObject.FindGameObjectWithTag("SceneChanger").GetComponent<SceneChanger>().LoadScene(2);
        }
    }
}
