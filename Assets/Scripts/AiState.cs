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
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
