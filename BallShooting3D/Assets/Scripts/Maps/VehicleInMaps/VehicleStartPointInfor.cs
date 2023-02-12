using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class VehicleStartPointInfor : MonoBehaviour
{
    [HideInInspector] public Vector3 position;
    [HideInInspector] public Quaternion rotation;

    public VehicleDirection direction;

    private void Awake()
    {
        position = this.gameObject.transform.position;
        rotation = direction switch
        {
            VehicleDirection.Up => Quaternion.Euler(0, -90, 0),
            VehicleDirection.Down => Quaternion.Euler(0, 90, 0),
            VehicleDirection.Left => Quaternion.Euler(0, 180, 0),
            VehicleDirection.Right => Quaternion.Euler(0, 0, 0),
            _ => rotation
        };
    }
}
