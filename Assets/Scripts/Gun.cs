using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    //Camera
    Transform camLoc;
    InputManager inputManager;
    //Weapon delay times
    WaitForSeconds rapidFireWait;
    WaitForSeconds reloadWait;

    //HUD elements
    public Text ammoDisplay;
    public Text fireRateDisplay;

    //Weapon visual effects
    public ParticleSystem muzzleFlash;

    //Weapon sound effects
    public AudioSource shootSound;
    public AudioSource reloadSound;
    public AudioSource aimSound;
    public AudioSource fireRateToggleSound;

    //Aiming data
    public Vector3 normalAimPosition;
    public Vector3 adsAimPosition;
    public Vector3 target;
    public Vector2 _currentRotation;

    //Aiming 
    public float aimSmoothing = 10f;
    public bool adsed = false;

    //Gun specific data
    [SerializeField] bool canFullAuto;
    [SerializeField] bool rapidFire = false;
    [SerializeField] float range = 50f;
    public float damage = 10f;
    [SerializeField] float fireRate = 5f;
    [SerializeField] int maxAmmunition = 150;
    [SerializeField] int magSize = 30;
    [SerializeField] int currentAmmunition;
    [SerializeField] float reloadTime;
    [SerializeField] float weaponSwayAmount;
    [SerializeField] float mouseSensitivity;
    private int maxAmmoVal;
    //Weapon recoil variables
    public bool randomizeRecoil = true;
    public Vector2 randomRecoilConstraints;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        weaponSwayAmount = -0.5f;
        mouseSensitivity = 0.35f;
        randomizeRecoil = true;
        inputManager = GameObject.FindWithTag("Player").GetComponent<InputManager>();
        maxAmmoVal = maxAmmunition;
    }

    public void ProcessLook(Vector2 input)
    {
        Vector2 mouseAxis = new Vector2(input.x, input.y);
        mouseAxis *= mouseSensitivity;
        _currentRotation += mouseAxis;
        _currentRotation.y = Mathf.Clamp(_currentRotation.y, -80f, 80f);
        transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;
        transform.parent.parent.parent.localRotation = Quaternion.AngleAxis(_currentRotation.x, Vector3.up);
        transform.parent.parent.localRotation = Quaternion.AngleAxis(-_currentRotation.y, Vector3.right);
    }

    private void Awake()
    {
        camLoc = Camera.main.transform;
        rapidFireWait = new WaitForSeconds(1 / fireRate);
        reloadWait = new WaitForSeconds(reloadTime);
        currentAmmunition = magSize;
        fireRateDisplay.text = "Single Fire";
        ammoDisplay.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
        target = normalAimPosition;
    }

    void Update()
    {
        DetermineAim();
        UpdateAmmo();
    }

    public void Shoot()
    {
        DetermineRecoil();
        RaycastHit hit;
        muzzleFlash.Play();
        shootSound.Play();
        if (Physics.Raycast(camLoc.position, camLoc.forward, out hit, range))
        {
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeDamage(damage, hit.point, hit.normal);
            }

            var hitBox = hit.collider.GetComponent<Hitbox>();
            if (hitBox != null)
            {
                hitBox.OnRayCastHit(this);
            }

            var target = hit.collider.GetComponent<Target>();
            if (target != null )
            {
                target.OnRayCastHit(hit.point, hit.normal);
            }
        }
        currentAmmunition--;
        ammoDisplay.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
    }

    public IEnumerator RapidFire()
    {
        if (CanShoot())
        {
            Shoot();
            if (rapidFire)
            {
                while (CanShoot())
                {
                    yield return rapidFireWait;
                    Shoot();
                }
            }
        }
    }

    public void Reload()
    {
        if (maxAmmunition > magSize)
        {
            currentAmmunition = magSize;
            maxAmmunition -= magSize;
            reloadSound.Play();
        }
        else if (maxAmmunition > 0)
        {
            currentAmmunition = maxAmmunition;
            maxAmmunition = 0;
            reloadSound.Play();
        }
        ammoDisplay.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
    }

    bool CanShoot()
    {
        bool enoughAmmunition = currentAmmunition > 0;
        return enoughAmmunition;
    }

    public void ToggleFireRate()
    {
        if (canFullAuto)
        {
            if (rapidFire)
            {
                rapidFire = false;
                fireRateDisplay.text = "Single Fire";
            }
            else
            {
                rapidFire = true;
                fireRateDisplay.text = "Full auto";
            }
            fireRateToggleSound.Play();
        }
        else
        {
            rapidFire = false;
            fireRateDisplay.text = "Single Fire";
        }
    }

    public void ADS()
    {
        adsed = !adsed;
        aimSound.Play();
    }

    private void DetermineAim()
    {
        if (adsed)
        {
            target = adsAimPosition;
        }
        else
        {
            target = normalAimPosition;
        }
        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        transform.localPosition = desiredPosition;
    }

    private void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;
        if (randomizeRecoil)
        {
            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(0, randomRecoilConstraints.y);

            Vector2 recoil2d = new Vector2(xRecoil, yRecoil);
            _currentRotation += recoil2d;
        }
    }

    private void UpdateAmmo()
    {
        if(inputManager.score > inputManager.prevScore && (inputManager.score - inputManager.prevScore)>=1000)
        {
            maxAmmunition = maxAmmoVal;
            inputManager.prevScore = inputManager.score;
        }
    }
}
