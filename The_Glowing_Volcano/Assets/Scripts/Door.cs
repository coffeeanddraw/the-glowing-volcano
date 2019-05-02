using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    [Tooltip("Assigning a key locks the door. if the key is in the player's inventory, the door will be unlocked")]
    [SerializeField]
    private InventoryObject key;

    [Tooltip("Make the key consumable")]
    [SerializeField]
    private bool consumeKey;

    [Tooltip("Locked door display text")]
    [SerializeField]
    private string lockedDisplayText = "Locked";

    [Tooltip("Locked door sound effect")]
    [SerializeField]
    private AudioClip lockedAudio;

    [Tooltip("Unlocked door sound effect")]
    [SerializeField]
    private AudioClip unlockedAudio;

    //public override string DisplayText =>  isLocked? lockedDisplayText : base.DisplayText;

    public override string DisplayText
    {
        get
        {
            string toReturn;

            if (isLocked)
                toReturn = HasKey ? $"Use {key.ObjectName}" : lockedDisplayText;
            else
                toReturn = base.DisplayText;

            return toReturn;
        }
    }

    private bool HasKey => PlayerInventory.InventoryObjects.Contains(key);

    private Animator animator;
    private bool isOpen = false;
    private bool isLocked;

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
        InitializeLocked();
    }

    private void InitializeLocked()
    {
        if (key != null)
            isLocked = true;
    }

    public override void InteractWith()
    {
        if(!isOpen)
        {
            if (isLocked && !HasKey) // Door is locked and the player does not have a key
            {
                audioSource.clip = lockedAudio;
            }
            else // Door is unlocked (originally unlocked or the player has a key)
            {
                audioSource.clip = unlockedAudio;
                animator.SetBool(shouldOpenAnimParameter, true);
                displayText = string.Empty;
                isOpen = true;
                UnlockDoor();
            }
            base.InteractWith(); // play sound effect
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        if (key != null && consumeKey)
            PlayerInventory.InventoryObjects.Remove(key);
    }
}
