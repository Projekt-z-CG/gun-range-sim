using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float maxHealth;
    SkinnedMeshRenderer skinnedMeshRenderer;

    [HideInInspector]
    public float currentHealth;
    Ragdoll ragdoll;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;
    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        var rigiBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigiBody in rigiBodies)
        {
            Hitbox hitBox = rigiBody.gameObject.AddComponent<Hitbox>();
            hitBox.health = this;
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }

        blinkTimer = blinkDuration;
    }

    public void Die()
    {
        ragdoll.ActivateRagdoll();
    }


    public void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}