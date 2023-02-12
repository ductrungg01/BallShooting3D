using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vehicle : MonoBehaviour
{
    public VehicleDirection _direction;
    public float speed = 0.2f;
    private Vector3 velocity;
    private Rigidbody rb;
    
    private void Start()
    {
        velocity = _direction switch
        {
            VehicleDirection.Up => new Vector3(0, 0, speed),
            VehicleDirection.Down => new Vector3(0, 0, -speed),
            VehicleDirection.Left => new Vector3(-speed, 0, 0),
            VehicleDirection.Right => new Vector3(speed, 0, 0),
            _ => velocity
        };

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = velocity;
    }
}
