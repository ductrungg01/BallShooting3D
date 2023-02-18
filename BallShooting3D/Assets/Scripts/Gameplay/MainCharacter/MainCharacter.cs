using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            GetComponent<Animator>().SetInteger("state", 0);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            GetComponent<Animator>().SetInteger("state", 1);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            GetComponent<Animator>().SetInteger("state", 2);
        }
    }

    public async UniTask Dead()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetInteger("state", 2);

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        
        Destroy(this.gameObject);
    }
}
