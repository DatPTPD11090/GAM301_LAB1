using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
   
    public Transform target;  // Điểm đích

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);  // Đặt điểm đến
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over! Quái đã đến đích.");
    }
}
