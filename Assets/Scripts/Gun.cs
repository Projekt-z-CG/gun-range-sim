using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Script handling gun behavior and mous input
 */
public class Gun : MonoBehaviour
{
    // Object to store camera transformation
    Transform camLoc;
    // Object to store input manager instance
    InputManager inputManager;
    // Object to store time variable used to time the full auto shooting
    WaitForSeconds rapidFireWait;
    // Object to store time variable used to time the gun reload
    WaitForSeconds reloadWait;

    // Text object to store ammunition UI representaiton
    public Text ammoDisplay;
    // Text object to store gun fire rate 
    public Text fireRateDisplay;

    // Object to store weapon visual muzzle flash effects
    public ParticleSystem muzzleFlash;

    // Object to store shoot sound audio source
    public AudioSource shootSound;
    // Object to store reload sound audio source
    public AudioSource reloadSound;
    // Object to store aim sound audio source
    public AudioSource aimSound;
    // Object to store fire rate toggle audio source
    public AudioSource fireRateToggleSound;

    // Variable to store default aim position
    public Vector3 normalAimPosition;
    // Variable to store ADS position
    public Vector3 adsAimPosition;
    // Variable to store temporary values of aim position
    public Vector3 target;
    // Variable to store current rotation values
    public Vector2 _currentRotation;

    // Variable to store aim smoothing 
    public float aimSmoothing = 10f;
    // Variable to store
    public bool adsed = false;

    // Variable to store information if gun is automatic
    [SerializeField] bool canFullAuto;
    // Variable to store information if gun is in single or full auto mode
    [SerializeField] bool rapidFire = false;
    // Variable to store range value
    [SerializeField] float range = 50f;
    // Variable to store damage value
    public float damage = 10f;
    // Variable to store fire rate value
    [SerializeField] float fireRate = 5f;
    // Variable to store max ammuniton value 
    [SerializeField] int maxAmmunition = 150;
    // Variable to store magazine size value
    [SerializeField] int magSize = 30;
    // Variable to store current value of ammunition
    [SerializeField] int currentAmmunition; 
    // Variable to store reload time value
    [SerializeField] float reloadTime;
    // Variable to store weapon sway coefficient 
    [SerializeField] float weaponSwayAmount;
    // Variable to store mouse sensitivity value
    [SerializeField] float mouseSensitivity;
    // Variable to store overall max ammunition value
    private int maxAmmoVal;
    // Variable to store information if recoil should be random
    public bool randomizeRecoil = true;
    // Variable to store information about random recoil constraints
    public Vector2 randomRecoilConstraints;

    // Start is called before the first frame update and sets Input Manager to the one owned by Player, and other gun specific variables.
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

    // Called in the update of input manager, processes input of the mouse and applies it to the camera and player rotation including recoil if applied
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

    // Setting some data before enabling script
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

    // Called every frame, used to get updates on recoil and ammuntion
    void Update()
    {
        DetermineAim();
        UpdateAmmo();
    }

    // Launched when gun shoot, creates raycasthits and depending on the collider, launches necessary collision scripts
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

            var targetHitBox = hit.collider.GetComponent<Target>();
            if (targetHitBox != null )
            {
                targetHitBox.OnRayCastHit(hit.point, hit.normal);
            }
        }
        currentAmmunition--;
        ammoDisplay.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
    }

    // Launched by the courotine, treats the full auto shooting depending on the fire speed 
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

    // Launched when gun is reloaded
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

    // Checks to assure if gun is able to shoot
    bool CanShoot()
    {
        bool enoughAmmunition = currentAmmunition > 0;
        return enoughAmmunition;
    }

    // Changes the fire rate of the gun
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

    // Used to perfom ADS
    public void ADS()
    {
        adsed = !adsed;
        aimSound.Play();
    }

    // Used to change the transformation of the camera depending if the gun is in ADS mode or in default aim position
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

    // Determines recoil and gun posisition
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

    // Adds ammunition when value of score is passed
    private void UpdateAmmo()
    {
        if(inputManager.score > inputManager.prevScore && (inputManager.score - inputManager.prevScore)>=1000)
        {
            maxAmmunition = maxAmmoVal;
            inputManager.prevScore = inputManager.score;
        }
    }
}
