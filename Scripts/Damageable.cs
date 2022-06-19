using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] GameObject hitEffect;
    float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector3 hitPos, Vector3 hitNormal)
    {
        Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            DestroyElem();
        }
    }

    void DestroyElem()
    {
        print(name + "was destroyed");
        Destroy(gameObject);
    }
}
