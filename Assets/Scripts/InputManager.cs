using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInput playerInput;
    private PlayerInput.WalkingActions walking;
    private PlayerMotor motor;
    private PlayerLook look;
    [SerializeField] Gun gun;

    Coroutine fireCoroutine;

    void Awake()
    {
        playerInput = new PlayerInput();
        walking = playerInput.Walking;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        walking.Jump.performed += ctx => motor.Jump();

        walking.Crouch.performed += ctx => motor.Crouch();
        walking.Sprint.performed += ctx => motor.Sprint();

        walking.Shoot.started += _ => StartFiring();
        walking.Shoot.canceled += _ => StopFiring();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(walking.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        look.ProcessLook(walking.Look.ReadValue<Vector2>());
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(gun.RapidFire());
    }

    void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }    
    }

    private void OnEnable()
    {
        walking.Enable();
    }
    private void OnDisable()
    {
        walking.Disable();
    }
}
