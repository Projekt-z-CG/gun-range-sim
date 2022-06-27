using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* State machine for enemies AI
**/
public class AiStateMachine
{
    // States that can be used
    public AiState[] states;
    // AI agent
    public AiAgent agent;
    // ID of the current state
    public AiStateId currentState;
    
    // Create state machine
    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        states = new AiState[numStates];
    }
    // Register the state of the enemy
    public void RegistarState(AiState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }
    // Get state of the enemy
    public AiState GetState(AiStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }
    //Update
    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }
    // Change state of the enmy
    public void ChangeState(AiStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
