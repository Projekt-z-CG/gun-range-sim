using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* AI state for enemy to when it dies
**/
public class AiDeathState : AiState
{
    /**
    * Whem enemy dies activete ragdoll, change the speed of enemy to 0 so it doesn't move
    **/
    public void Enter(AiAgent agent)
    {
        agent.ragdoll.ActivateRagdoll();
        agent.skinnedMeshRenderer.updateWhenOffscreen = true;
        agent.navMeshAgent.speed = 0.0f;

    }

    public void Exit(AiAgent agent)
    {
        
    }

    public AiStateId GetId()
    {
        return AiStateId.Death;
    }

    public void Update(AiAgent agent)
    {
        
    }
}
