using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanToggle : InteractiveObject
{
    [Tooltip("The key to the snowman's heart")]
    [SerializeField]
    private InventoryObject key;

    [Tooltip("The GameObject to toggle")]
    [SerializeField]
    private GameObject objectToToggle;

    [Tooltip("The GameObject to toggle 2")]
    [SerializeField]
    private GameObject objectToToggle2;

    [Tooltip("Lonely snowman display text")]
    [SerializeField]
    private string lonelySnowmanDisplayText = "I am lonely...";

    [Tooltip("Snowman dialogue 1")]
    [SerializeField]
    private string snowmanDialogue1 = "";

    [Tooltip("Snowman dialogue 2")]
    [SerializeField]
    private string snowmanDialogue2 = "";

    [Tooltip("Lonely snowman sound effect")]
    [SerializeField]
    private AudioClip lockedAudio;

    [Tooltip("Snowman sound effect")]
    [SerializeField]
    private AudioClip unlockedAudio;

    public override string DisplayText => isLocked ? lonelySnowmanDisplayText : base.DisplayText;

    private bool HasKey => PlayerInventory.InventoryObjects.Contains(key);

    private int interaction;
    private bool isLocked;

    public SnowmanToggle()
    {
        displayText = nameof(SnowmanToggle);
    }
    protected override void Awake()
    {
        base.Awake();
        isLocked = true;
        interaction = 0;
    }

    public override void InteractWith()
    {
        if (isLocked)
        {
            if (!HasKey) 
            {
                audioSource.clip = lockedAudio;
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
            else 
            {
                audioSource.clip = unlockedAudio;
                interaction = 1;
                UnlockSnowman(); 
                
            }
            base.InteractWith(); // play sound effect
        }
        else
        {
            if (interaction == 1)
            {
                displayText = snowmanDialogue1;
                interaction = 2;
            }
            else if (interaction == 2)
            {
                displayText = snowmanDialogue2;
                objectToToggle2.SetActive(!objectToToggle2.activeSelf);
            }
            base.InteractWith(); // play sound effect
        }
    }

    private void UnlockSnowman()
    {
        isLocked = false;
    }
}
