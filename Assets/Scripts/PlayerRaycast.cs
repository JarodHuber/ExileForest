using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerRaycast : MonoBehaviour
{
    public float DistanceToSee;
    RaycastHit WhatIHit;

    void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, out WhatIHit, DistanceToSee, 9))
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        }
    }
}
