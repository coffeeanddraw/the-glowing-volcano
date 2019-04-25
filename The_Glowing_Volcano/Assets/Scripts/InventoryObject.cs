using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : InteractiveObject
{
    [Tooltip("Name of the object for inventory menu")]
    [SerializeField]
    private string objectName = nameof(InventoryObject);

    [Tooltip("Inventory item description")]
    [TextArea(3, 8)]
    [SerializeField]
    private string description;

    [Tooltip("Display icon for inventory item")]
    [SerializeField]
    private Sprite icon;

    [Tooltip("Use as transport object")]
    [SerializeField]
    private bool transportItem;

    [SerializeField]
    private GameObject objectToTransport;

    [Tooltip("Transport location x")]
    [SerializeField]
    private int x;

    [Tooltip("Transport location x")]
    [SerializeField]
    private int y;

    [Tooltip("Transport location x")]
    [SerializeField]
    private int z;

    public Sprite Icon => icon;
    public string ObjectName => objectName;
    public string Description => description;

    private new Renderer renderer;
    private new Collider collider;
    private Light lighting;
    private int childCount;
    private Transform passengerTransform;

    protected virtual void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        lighting = GetComponent<Light>();
        childCount = transform.childCount;
        passengerTransform = objectToTransport.GetComponent<Transform>();
    }

    public InventoryObject()
    {
        displayText = $"Take {objectName}";
    }

    /// <summary>
    /// Interacting with inventory object:
    /// 1. Add the inventory object to the PlayerInventory list
    /// 2. Remove the object from the game world/scene
    /// </summary>
    public override void InteractWith()
    {
        base.InteractWith(); // Play sound effect 

        PlayerInventory.InventoryObjects.Add(this);
        InventoryMenu.Instance.AddItemToMenu(this);

        //renderer.enabled = false;
        collider.enabled = false;
        //lighting.enabled = false;
        if (renderer)
        {
            renderer.enabled = false;
        }
        if (lighting)
        {
            lighting.enabled = false;
        }

        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child != null)
            child.SetActive(false);
        }

        if (transportItem)
        {
            base.InteractWith();
            passengerTransform.position = new Vector3(x, y, z);
        }

        Debug.Log($"Inventory menu game object name {InventoryMenu.Instance.name}");
    }
}
