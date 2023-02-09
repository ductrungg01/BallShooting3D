using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class MapsGenerate : MonoBehaviour
{
    public List<GameObject> cityPrefabs = new List<GameObject>();
    public List<Transform> spawnPos = new List<Transform>();


    private void Awake()
    {
        GenerateMaps();
    }

    void GenerateMaps()
    {
        int numOfCityGroup = cityPrefabs.Count;
        bool[] isUsed = new bool[numOfCityGroup];

        for (int i = 0; i < numOfCityGroup; i++)
        {
            System.Random rand = new System.Random();
            int id = 0;
            do
            {
                id = rand.Next(numOfCityGroup);
            } while (isUsed[id] == false);

            Instantiate(cityPrefabs[i], spawnPos[id].position, Quaternion.identity);
        }
    }
}
