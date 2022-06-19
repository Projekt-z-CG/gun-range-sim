using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    Transform cam;
    WaitForSeconds rapidFireWait;
    WaitForSeconds reloadWait;
    public Text ammoDisplay;
    public Text fireRateDisplay;
    public ParticleSystem muzzleFlash;

    [SerializeField] bool rapidFire = false;
    [SerializeField] float range = 50f;
    public float damage = 10f;
    [SerializeField] float fireRate = 5f;
    [SerializeField] int maxAmmunition = 150;
    [SerializeField] int magSize = 30;
    [SerializeField] int currentAmmunition;
    [SerializeField] float reloadTime;


    private void Awake()
    {
        cam = Camera.main.transform;
        rapidFireWait = new WaitForSeconds(1 / fireRate);
        reloadWait = new WaitForSeconds(reloadTime);
        currentAmmunition = magSize;
        fireRateDisplay.text = "Single Fire";
        ammoDisplay.text = currentAmmunition.ToString() + "/" + maxAmmunition.ToString();
    }

    public void Shoot()
    {
        RaycastHit hit;
        muzzleFlash.Play();
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if (hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeDamage(damage, hit.point, hit.normal);
            }

            var hitBox = hit.collider.GetComponent<Hitbox>();
            if (hitBox)
            {
                hitBox.OnRayCastHit(this);
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
        }
        else if (maxAmmunition > 0)
        {
            currentAmmunition = maxAmmunition;
            maxAmmunition = 0;
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
    }
}
