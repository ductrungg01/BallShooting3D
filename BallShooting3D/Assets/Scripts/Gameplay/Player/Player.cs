using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            // Idle
            anim.SetInteger("state", 0);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // Walk
            anim.SetInteger("state", 1);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            // Dead
            anim.SetInteger("state", 2);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            // Attack
            anim.SetInteger("state", 3);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            // Aim
            anim.SetInteger("state", 4);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            // Shoot
            anim.SetInteger("state", 5);
        }
    }

    public async UniTask Dead()
    {
        anim.SetInteger("state", 2);
        
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        
        Destroy(this.gameObject);
    }
}
