using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private NavMeshAgent agent;

    void Update()
    {
        if (LevelManager.Instance.isLoading) return;

        Vector3 pos = FindObjectOfType<MainCharacter>().transform.position;
        agent.SetDestination(pos);

        // TODO: I don't know why y = -0.4 always :)
        Vector3 nowPos = transform.position;
        this.transform.position = new Vector3(nowPos.x, 0, nowPos.z);
    }
}
