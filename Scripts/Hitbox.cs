using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Health health;
    // Start is called before the first frame update

    // Update is called once per frame
    public void OnRayCastHit(Gun weapon)
    {
        health.TakeDamage(weapon.damage);
    }
}
