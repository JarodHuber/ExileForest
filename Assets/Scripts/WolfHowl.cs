using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WolfHowl : MonoBehaviour
{
    Timer timer = new Timer(36);
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (timer.Check() && Random.Range(0.0f,1.0f) < .6f)
        {
            audioSource.Play();
        }
    }
}
