using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor1 : MonoBehaviour
{
    // Object to store character controller reference
    private CharacterController controller;
    // Variable to store player velocity value
    private Vector3 playerVelocity;
    // Variable to store information if player touches the ground
    private bool isGrounded;
    // Variable to store information of player speed
    public float speed = 5f;
    // Variable to store the value of gravity
    public float gravity = -14f;
    // Variable to store the jump height value
    public float jumpHeight = 1.5f;
    // Variable to store crouch timer
    public float crouchTimer = 2f;
    // Variable to store information if player is crouching
    public bool lerpCrouch;
    // Variable to store information if player is crouched
    public bool crouching;
    // Variable to store information if player is sprinting
    public bool sprinting;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame, processes the transition between crouch states
    void Update()
    {
        isGrounded = controller.isGrounded;
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if(crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if(p >1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    // Called in the update of input manager, processes move input
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Called in the awake of input manager, processes jump
    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -1.5f * gravity);
        }
    }

    // Called in the awake of input manager, processes crouch
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    // Called in the awake of input manager, processes sprint
    public void Sprint()
    {
        sprinting = !sprinting;
        if(sprinting)
        {
            speed = 8;
        }
        else
        {
            speed = 5;
        }    
    }
}

