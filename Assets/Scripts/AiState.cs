using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Enumerator of AI states
**/
public enum AiStateId
{
    chasePlayer,
    Death
}

public interface AiState
{
    //Get id for AI state
    AiStateId GetId();
    //Enter AI state
    void Enter(AiAgent agent);
    //Update AI state
    void Update(AiAgent agent);
    //Exit AI state
    void Exit(AiAgent agent);
}
