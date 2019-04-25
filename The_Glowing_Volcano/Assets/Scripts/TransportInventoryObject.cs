using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportInventoryObject : InventoryObject
{
    [SerializeField]
    private GameObject objectToTransport;

    [SerializeField]
    private int x;

    [SerializeField]
    private int y;

    [SerializeField]
    private int z;

    private new Transform transform;

    protected override void Start()
    {
        base.Start();
        transform = objectToTransport.GetComponent<Transform>();
    }

    //void Update() // check every frame 
    //{
    //    if (PlayerInventory.InventoryObjects.Contains(this))
    //    {
    //        transform.position = new Vector3(x, y, z);
    //    }
    //}

    public override void InteractWith()
    {
        base.InteractWith();
        transform.position = new Vector3(x, y, z);
    }
}

