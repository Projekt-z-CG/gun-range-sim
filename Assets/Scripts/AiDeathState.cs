using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiDeathState : AiState
{
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
