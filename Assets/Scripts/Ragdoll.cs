using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigiBodies;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigiBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactivateRagdoll();
    }

    // Update is called once per frame

    public void DeactivateRagdoll() { 
        foreach(var rigiBody in rigiBodies)
        {
            rigiBody.isKinematic = true;
        }
        animator.enabled = true;
    } 

    public void ActivateRagdoll()
    {
        foreach(var rigiBody in rigiBodies)
        {
            rigiBody.isKinematic = false;
        }
        animator.enabled = false;
    }
   
}
