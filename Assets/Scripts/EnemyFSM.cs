using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyMoveFSM;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public EnemyState currentState;
    public Transform target;
    private NavMeshAgent agent;
    private float journeyFraction;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
        currentState = EnemyState.Moving;
    }

    void Update()
    {
        journeyFraction = 1f - (agent.remainingDistance / agent.pathEndPosition.magnitude);

        if (journeyFraction >= 0.33f && currentState == EnemyState.Moving)
        {
            int randomAction = Random.Range(0, 2);
            if (randomAction == 0)
                StartCoroutine(Jump());
            else
                StartCoroutine(SpeedUp());
        }
    }

    IEnumerator Jump()
    {
        currentState = EnemyState.Jumping;
        Debug.Log("Enemy Jumps!");
        Vector3 jumpPos = transform.position + new Vector3(0, 2, 0);
        transform.position = jumpPos;
        yield return new WaitForSeconds(0.5f);
        transform.position -= new Vector3(0, 2, 0);
        currentState = EnemyState.Moving;
    }

    IEnumerator SpeedUp()
    {
        currentState = EnemyState.SpeedUp;
        Debug.Log("Enemy Speeds Up!");
        agent.speed *= 2;
        yield return new WaitForSeconds(2f);
        agent.speed /= 2;
        currentState = EnemyState.Moving;
    }
}
