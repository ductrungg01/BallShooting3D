using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BoostManager : MonoBehaviour
{
    public GameObject _boostPrefab;

    public List<int> _boostValue = new List<int>()
    {
        5, 10, 15, 20
    };

    [HideInInspector] public List<GameObject> _boostList = new List<GameObject>();

    public static BoostManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int getRandomBoostValue()
    {
        Random rnd = new Random();

        return _boostValue[rnd.Next(_boostValue.Count)];
    }
}
