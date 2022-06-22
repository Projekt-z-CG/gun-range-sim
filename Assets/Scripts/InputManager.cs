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
    WeaponSwitching weaponSwitch;
    [SerializeField] Gun gun1;
    [SerializeField] Gun gun2;
    [SerializeField] Gun gun3;
    [SerializeField] Gun gun4;
    Gun currentGun;

    public Quaternion rotationOne;
    public Quaternion rotationTwo;

    Coroutine fireCoroutine;

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
    }

    void Start()
    {
        currentGun = gun1;
    }

    void SwitchGun1()
    {
        currentGun = gun1;
        weaponSwitch.SetIndex(0);
    }

    void SwitchGun2()
    {
        currentGun = gun2;
        weaponSwitch.SetIndex(1);
    }

    void SwitchGun3()
    {
        currentGun = gun3;
        weaponSwitch.SetIndex(2);
    }

    void SwitchGun4()
    {
        currentGun = gun4;
        weaponSwitch.SetIndex(3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(walking.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        currentGun.ProcessLook(walking.Look.ReadValue<Vector2>());
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(currentGun.RapidFire());
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
