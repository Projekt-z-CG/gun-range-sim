using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script which handles reception of damage and destruction of the objects
 */
public class Damageable : MonoBehaviour
{   
    // Variable storing object health points
    [SerializeField] float maxHealth = 100f;
    // Object to store the effect of bullet hit
    [SerializeField] GameObject hitEffect;

    //Variable to store current object health
    float currentHealth;

    // Used to set up variables
    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Creates effect on hit, and removes health of the object
    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            DestroyElem();
        }
    }

    // Destroys object
    void DestroyElem()
    {
        Destroy(gameObject);
    }
}
