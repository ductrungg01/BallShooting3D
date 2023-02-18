using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    void Update()
    {
        Vector3 pos = FindObjectOfType<MainCharacter>().transform.position;
        agent.SetDestination(pos);
    }
}
