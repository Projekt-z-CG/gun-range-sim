using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * Script which handles Input from player on parkour map
 */
public class InputManager1 : MonoBehaviour
{
    // Object to store player input reference
    private PlayerInput playerInput;
    // Object to store player input walking actions reference
    private PlayerInput.WalkingActions walking;
    // Object to store player motor reference
    private PlayerMotor1 motor;
    // Object to store player look reference
    private PlayerLook1 look;

    // Used to set up variables, and read input calls
    void Awake()
    {
        playerInput = new PlayerInput();
        walking = playerInput.Walking;
        motor = GetComponent<PlayerMotor1>();
        look = GetComponent<PlayerLook1>();
        walking.Jump.performed += ctx => motor.Jump();

        walking.Crouch.performed += ctx => motor.Crouch();
        walking.Sprint.performed += ctx => motor.Sprint();
    }

    // FixedUpdate called mulitple times per frame, reads and calls function to process move input
    void FixedUpdate()
    {
        motor.ProcessMove(walking.Movement.ReadValue<Vector2>());
    }
    // Called after update, process mouse input
    void LateUpdate()
    {
        look.ProcessLook(walking.Look.ReadValue<Vector2>());
    }

    // Enables walking object
    private void OnEnable()
    {
        walking.Enable();
    }

    // Disables walking object
    private void OnDisable()
    {
        walking.Disable();
    }
}
