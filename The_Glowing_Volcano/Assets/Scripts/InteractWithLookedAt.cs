﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects when the player presses the interact button while looking at an IInteractive 
/// and then calls that IInteractive's Interactive method
/// </summary>

public class InteractWithLookedAt : MonoBehaviour
{
    [SerializeField]
    private DetectLookedAtInteractive detectLookedAtInteractive;

    // Update is called once per frame
    void Update() // check every frame 
    {
        if (Input.GetButtonDown("Interact") && detectLookedAtInteractive.LookedAtInteractive != null)
        {
            Debug.Log("Player pressed the interact button");
            detectLookedAtInteractive.LookedAtInteractive.InteractWith();
        }
    }
}