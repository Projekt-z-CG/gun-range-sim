using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/**
* Script for managing state machines for enemy AI
**/
public class AiAgent : MonoBehaviour
{
    //state machine of the AI agent
    public AiStateMachine stateMachine;
    //state in which the AI agent is in
    public AiStateId intialState;
    //configuration of the AI agent
    public AiAgentConfig config;
    //nav mesh for AI agent
    public NavMeshAgent navMeshAgent;
    //ragdoll for AI agent
    public Ragdoll ragdoll;
    //skinned mesh render for AI agent
    public SkinnedMeshRenderer skinnedMeshRenderer;
   
    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegistarState(new AiChasePlayerState());
        stateMachine.RegistarState(new AiDeathState());
        stateMachine.ChangeState(intialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
