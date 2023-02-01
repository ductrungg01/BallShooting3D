using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public ObjectPooler bulletPooler;

    private void Awake()
    {
        Instance = this;
    }
}