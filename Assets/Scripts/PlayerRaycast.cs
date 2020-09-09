using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    public float DistanceToSee;
    public LayerMask layerMask;
    RaycastHit WhatIHit;

    bool locked = false;
    bool locked2 = false;

    void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * DistanceToSee, Color.magenta);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out WhatIHit, DistanceToSee, layerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("HIT!");
                switch (WhatIHit.transform.tag)
                {
                    case "Note":
                        switch (WhatIHit.transform.name)
                        {
                            case "1":
                                GameObject.FindGameObjectWithTag("ShowMap").GetComponent<ShowMap>().RevealMapPiece(1);
                                break;
                            case "2":
                                GameObject.FindGameObjectWithTag("ShowMap").GetComponent<ShowMap>().RevealMapPiece(2);
                                break;
                            case "3":
                                GameObject.FindGameObjectWithTag("ShowMap").GetComponent<ShowMap>().RevealMapPiece(3);
                                break;
                            case "4":
                                GameObject.FindGameObjectWithTag("ShowMap").GetComponent<ShowMap>().RevealMapPiece(4);
                                break;
                            default:
                                break;
                        }
                        if (!locked)
                        {
                            Invoke("Popup", 3);
                            locked = true;
                        }

                        MonsterManager.IncreaseAggression();
                        Destroy(WhatIHit.transform.gameObject);
                        break;
                    case "Key":
                        ValueHolder.HasKey = true;
                        GameObject.FindGameObjectWithTag("TextPopup").GetComponent<TextPopUp>().ShowText(6);
                        MonsterManager.IncreaseAggression(true);
                        Destroy(WhatIHit.transform.gameObject);
                        break;
                    case "Gate":
                        if (ValueHolder.HasKey)
                        {
                            Destroy(WhatIHit.transform.gameObject);
                            break;
                        }

                        if (!locked2)
                        {
                            MonsterManager.IncreaseAggression();
                            locked2 = true;
                        }

                        GameObject.FindGameObjectWithTag("TextPopup").GetComponent<TextPopUp>().ShowText(2);
                        break;
                }
            }
        }
    }

    void Popup()
    {
        GameObject.FindGameObjectWithTag("TextPopup").GetComponent<TextPopUp>().ShowText(3);
        Invoke("Popup2", 4.1f);
    }
    void Popup2()
    {
        GameObject.FindGameObjectWithTag("TextPopup").GetComponent<TextPopUp>().ShowText(4);
    }
}
