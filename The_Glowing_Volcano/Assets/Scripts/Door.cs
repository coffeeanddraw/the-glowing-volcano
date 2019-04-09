using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Tooltip("Lock the door")]
    [SerializeField]
    private bool isLocked;

    [Tooltip("Locked door display text")]
    [SerializeField]
    private string lockedDisplayText = "Locked";

    [Tooltip("Locked door sound effect")]
    [SerializeField]
    private AudioClip lockedAudio;

    [Tooltip("Unlocked door sound effect")]
    [SerializeField]
    private AudioClip unlockedAudio;

    public override string DisplayText =>  isLocked? lockedDisplayText : base.DisplayText;

    private Animator animator;
    private bool isOpen = false;
    private int shouldOpenAnimParameter = Animator.StringToHash(nameof(shouldOpenAnimParameter));

    /// <summary>
    /// Using a constructor here to intialize displayText in the editor
    /// </summary>
    public Door()
    {
        displayText = nameof(Door);
    }
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void InteractWith()
    {
        if(!isOpen)
        {
            if(!isLocked)
            {
                audioSource.clip = unlockedAudio;
                animator.SetBool(shouldOpenAnimParameter, true);
                displayText = string.Empty;
                isOpen = true;
            }
            else // if the door is locked 
            {
                audioSource.clip = lockedAudio;
            }
            base.InteractWith(); // play sound effect
        }
    }
}
