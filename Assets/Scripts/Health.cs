using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   
    public delegate void EnemyKilled();
    
    public static event EnemyKilled OnEnemyKilled;
    //Input manager
    InputManager inputManager;
    // Maximum health of an enemy
    public float maxHealth;
    // Skinned render mesh
    SkinnedMeshRenderer skinnedMeshRenderer;

    [HideInInspector]
    AiAgent agent;
    // Current health of an enemy
    public float currentHealth;
    // Ragdoll for the enemy
    Ragdoll ragdoll;
    // Blink ntensity of enemy when beeing hit
    public float blinkIntensity;
    // Blink duration of enemy when beeing hit
    public float blinkDuration;
    float blinkTimer;
    
    // Find input manager
    void Awake()
    {
        inputManager = GameObject.Find("Player Switch").GetComponent<InputManager>();
        Debug.Log(inputManager.score);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AiAgent>();
        //ragdoll = GetComponent<Ragdoll>();
        currentHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        var rigiBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigiBody in rigiBodies)
        {
            Hitbox hitBox = rigiBody.gameObject.AddComponent<Hitbox>();
            hitBox.health = this;
        }
    }

    // Enemy take damage from player
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }

        blinkTimer = blinkDuration;
    }
    // Enemy die
    public void Die()
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        agent.stateMachine.ChangeState(AiStateId.Death);

        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
            Destroy(gameObject, 3);
            inputManager.score += 50;
            Debug.Log(inputManager.score);
        }
    }

    // Enemy update
    public void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}
