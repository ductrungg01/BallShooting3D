using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;

    private void Start()
    {
        anim.SetInteger("state", 1);
    }

    void Update()
    {
        GameObject player = FindObjectOfType<Player>().gameObject;

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
