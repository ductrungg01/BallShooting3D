using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public List<GameObject> _vehiclePrefabs = new List<GameObject>();

    [HideInInspector] public List<GameObject> _vehicleList = new List<GameObject>();

    public static VehicleManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
