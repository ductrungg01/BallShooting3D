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
        
    }

    public async UniTask Dead()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetInteger("state", 2);

        await UniTask.Delay(TimeSpan.FromSeconds(1));
        
        Destroy(this.gameObject);
    }
}
