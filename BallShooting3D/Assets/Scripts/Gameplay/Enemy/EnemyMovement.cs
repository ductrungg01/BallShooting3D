using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;

    void Update()
    {
        GameObject player = FindObjectOfType<MainCharacter>().gameObject;

        if (player)
        {
            Vector3 pos = player.transform.position;
            agent.SetDestination(pos);
        }
        else
        {
            agent.SetDestination(this.gameObject.transform.position);
            anim.SetInteger("state", 0);
        }
        
    }
}
