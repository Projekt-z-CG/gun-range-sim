using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{

    public Transform playerTransform;
    float timer = 0.0f;

    public AiStateId GetId()
    {
        return AiStateId.chasePlayer;
    }
    public void Enter(AiAgent agent)
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public void Update(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqDistance = (playerTransform.position - agent.navMeshAgent.destination).magnitude;
            if (sqDistance > agent.config.maxTime * agent.config.maxDistance)
            {
                agent.navMeshAgent.destination = playerTransform.position;
            }
            timer = agent.config.maxTime;
        }
    }
    public void Exit(AiAgent agent)
    {

    }

    

}
