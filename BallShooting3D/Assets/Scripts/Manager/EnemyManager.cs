using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> _enemyPrefabs = new List<GameObject>();

    [HideInInspector] public List<GameObject> _enemyList = new List<GameObject>();

    public static EnemyManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
