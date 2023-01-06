using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    Vector3[] _mcPositions =
    {
        new Vector3(),
        new Vector3(0.23f, 0f, -2.66f),                 // level1
        new Vector3(0.08f, 0f, 1.21f),                  // level2
        new Vector3(-4f, 0f, -2.51f),                   // level3                
        new Vector3(-3.49f, 0f, 4.61f),                 // level4
        new Vector3(0f, 0f, 1f),                        // level5
        new Vector3(0.04f, 0f, -2.55f),                 // level6
        new Vector3(3.17f, 0f, -1.65f),                 // level7
    };

    // Start is called before the first frame update
    void Start()
    {
        SetInitializePositionByLevel(LevelManager.Instance.levelIsPlayingRightNow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitializePositionByLevel(int level)
    {
        this.transform.position = _mcPositions[level];
    }

    public static explicit operator GameObject(MainCharacter v)
    {
        throw new NotImplementedException();
    }
}
