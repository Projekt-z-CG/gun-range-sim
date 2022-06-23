using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    InputManager inputManager;
    public float maxHealth;
    SkinnedMeshRenderer skinnedMeshRenderer;

    [HideInInspector]
    AiAgent agent;
    public float currentHealth;
    Ragdoll ragdoll;
    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

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


    public void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}