using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
* AI state for chasing the player
**/
public class AiChasePlayerState : AiState
{
    //position where the enemy will go
    public Transform playerTransform;
    float timer = 0.0f;
    /**
    * Get state ID
    **/
    public AiStateId GetId()
    {
        return AiStateId.chasePlayer;
    }
    /**
    * Enter ChasePlayerState
    **/
    public void Enter(AiAgent agent)
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    /**
    * Update the position where the enemy should go
    **/
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
    /**
    * Exit ChasePlayerState
    **/
    public void Exit(AiAgent agent)
    {

    }

    

}
