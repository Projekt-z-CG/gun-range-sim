using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;
    WaitForSeconds rapidFireWait;
    WaitForSeconds reloadWait;
    [SerializeField] bool rapidFire = false;
    [SerializeField] float range = 50f;
    [SerializeField] float damage = 10f;
    [SerializeField] float fireRate = 5f;
    [SerializeField] int maxAmmunition = 30;
    [SerializeField] int currentAmmunition;
    [SerializeField] float reloadTime;


    private void Awake()
    {
        cam = Camera.main.transform;
        rapidFireWait = new WaitForSeconds(1 / fireRate);
        reloadWait = new WaitForSeconds(reloadTime);
        currentAmmunition = maxAmmunition;
    }

    public void Shoot()
    {
        RaycastHit hit;
        Debug.Log("pls");
        if(Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if(hit.collider.GetComponent<Damageable>() != null)
            {
                hit.collider.GetComponent<Damageable>().TakeDamage(damage,hit.point,hit.normal);
            }
        }
        currentAmmunition--;
    }

    public IEnumerator RapidFire()
    {
        if(CanShoot())
        {
            Shoot();
            if (rapidFire)
            {
                while (CanShoot())
                {
                    yield return rapidFireWait;
                    Shoot();
                }
                StartCoroutine(Reload());
            }
        }
        else
        {
            StartCoroutine(Reload());
        }
        
    }

    public IEnumerator Reload()
    {
        if(currentAmmunition == maxAmmunition)
        {
            yield return null;
        }
        yield return reloadWait;
        currentAmmunition = maxAmmunition;
    }

    bool CanShoot()
    {
        bool enoughAmmunition = currentAmmunition > 0;
        return enoughAmmunition;
    }
}
