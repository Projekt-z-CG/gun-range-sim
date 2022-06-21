using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId intialState;
    public AiAgentConfig config;
    public NavMeshAgent navMeshAgent;
    public Ragdoll ragdoll;
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
