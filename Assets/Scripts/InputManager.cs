using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/**
 * Script which handles Input from player
 */
public class InputManager : MonoBehaviour
{
    // Object to store player input reference
    private PlayerInput playerInput;
    // Object to store player input walking actions reference
    private PlayerInput.WalkingActions walking;
    // Object to store player motor reference
    private PlayerMotor motor;
    // Object to store weapon switch reference
    WeaponSwitching weaponSwitch;
    // Object to store aussault rifle m4 reference
    [SerializeField] Gun gun1;
    // Object to store pistol reference
    [SerializeField] Gun gun2;
    // Object to store assault rifle ak reference
    [SerializeField] Gun gun3;
    // Object to store sniper reference
    [SerializeField] Gun gun4;
    // Object to store current gun reference
    Gun currentGun;

    // Text object to store score
    public Text scoreText;

    // Variable to store score
    public int score = 0;
    // Variable to store previous score value
    public int prevScore = 0;

    // Vector2 to store rotation values
    public Vector2 rotationVal;
    // Courotine to manage full automatic shooting
    Coroutine fireCoroutine;

    // Used to set up variables, and read input calls
    void Awake()
    {
        playerInput = new PlayerInput();
        weaponSwitch = GameObject.Find("WeaponsHolder").GetComponent<WeaponSwitching>();
        walking = playerInput.Walking;
        motor = GetComponent<PlayerMotor>();
        walking.Jump.performed += ctx => motor.Jump();

        walking.Crouch.performed += ctx => motor.Crouch();
        walking.Sprint.performed += ctx => motor.Sprint();

        walking.Gun1.performed += _ => SwitchGun1();
        walking.Gun2.performed += _ => SwitchGun2();
        walking.Gun3.performed += _ => SwitchGun3();
        walking.Gun4.performed += _ => SwitchGun4();

        walking.Shoot.started += _ => StartFiring();
        walking.Shoot.canceled += _ => StopFiring();
        walking.Reload.performed += _ => currentGun.Reload();
        walking.ToggleFireRate.performed += _ => currentGun.ToggleFireRate();
        walking.ADS.performed += _ => currentGun.ADS();

        scoreText.text = "Score: " + score;
    }

    // Used to set the current gun
    void Start()
    {
        currentGun = gun1;
    }

    // Used to change the gun object and pass rotation value
    void SwitchGun1()
    {
        rotationVal = currentGun._currentRotation;
        currentGun = gun1;
        currentGun._currentRotation = rotationVal;
        weaponSwitch.SetIndex(0);
    }

    // Used to change the gun object and pass rotation value
    void SwitchGun2()
    {
        rotationVal = currentGun._currentRotation;
        currentGun = gun2;
        currentGun._currentRotation = rotationVal;
        weaponSwitch.SetIndex(1);
    }

    // Used to change the gun object and pass rotation value
    void SwitchGun3()
    {
        rotationVal = currentGun._currentRotation;
        currentGun = gun3;
        currentGun._currentRotation = rotationVal;
        weaponSwitch.SetIndex(2);
    }

    // Used to change the gun object and pass rotation value
    void SwitchGun4()
    {
        rotationVal = currentGun._currentRotation;
        currentGun = gun4;
        currentGun._currentRotation = rotationVal;
        weaponSwitch.SetIndex(3);
    }

    // FixedUpdate called mulitple times per frame, reads and calls function to process move input
    void FixedUpdate()
    {
        motor.ProcessMove(walking.Movement.ReadValue<Vector2>());
        scoreText.text = "Score: " + score;
    }

    // Called after update, process mouse input
    void LateUpdate()
    {
        currentGun.ProcessLook(walking.Look.ReadValue<Vector2>());
    }

    // Used to create coroutine to process full automatic shooting
    void StartFiring()
    {
        fireCoroutine = StartCoroutine(currentGun.RapidFire());
    }

    // Used to stop coroutine processing full automatic shooting
    void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
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
