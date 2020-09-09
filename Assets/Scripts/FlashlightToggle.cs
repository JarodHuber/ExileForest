using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class FlashlightToggle : MonoBehaviour
{
    public AudioClip turnOn;
    public AudioClip turnOff;
    public Timer timerOn;
    public Timer timerOff;

    Light flashlight;
    AudioSource audiosrc;

    void Start()
    {
        flashlight = GetComponent<Light>();
        audiosrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        System.Random rand = new System.Random();
        /*if (!timerOff.Check(false))
        {
            return;
        }
*/
        if (timerOn.Check(false))
        {
            flashlight.enabled = false;
            timerOff = new Timer(5);
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            if (flashlight.enabled)
            {
                audiosrc.clip = turnOn;
                timerOn = new Timer(rand.Next(10,20));
            }
            else
            {
                audiosrc.clip = turnOff;
                timerOn = new Timer(1000000);

            }
            audiosrc.Play();

        }
    }
}
