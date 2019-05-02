using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuItemToggle : MonoBehaviour
{
    [Tooltip("Image component used to show the associated icon")]
    [SerializeField]
    private Image iconImage;

    public static event Action<InventoryObject> InventoryMenuItemSelected;
    private InventoryObject associatedInventoryObject;

    public InventoryObject AssociatedInventoryObject
    {
        get { return associatedInventoryObject; }
        set
        {
            associatedInventoryObject = value;
            iconImage.sprite = associatedInventoryObject.Icon;
        }
    }

    /// <summary>
    /// This will be plugged into the toggle's "OnValueChanged" property in the editor 
    /// and called whenever the toggle is clicked.
    /// </summary>
    public void InventoryMenuItemWasToggled(bool isOn)
    {
        // Only run the code when the toggle has been selected, not when it has been deselected
        if (isOn)
            InventoryMenuItemSelected?.Invoke(AssociatedInventoryObject);
    }

    private void Awake()
    {
        Toggle toggle = GetComponent<Toggle>();
        ToggleGroup toggleGroup = GetComponentInParent<ToggleGroup>();
        toggle.group = toggleGroup; 
    }
}
