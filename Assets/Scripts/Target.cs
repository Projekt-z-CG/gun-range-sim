using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script handling target behavior
 */
public class Target : MonoBehaviour
{
    // Variable storing distance from shooting place
    public int distance = 1;
    // Variable storing hit effect
    [SerializeField] GameObject hitEffect;
    // Variable storing reference to Input Manager
    InputManager inputManager;

    // Start is called before the first frame update and sets Input Manager to the one owned by Player.
    void Start()
    {
        inputManager = GameObject.Find("Player Switch 1").GetComponent<InputManager>();
    }

    // Creates effect on hit, and gives point depending of place hit
    public void OnRayCastHit(Vector3 hitPos, Vector3 hitNormal) {
        Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));

        if (this.gameObject.tag == "Head")
        {
            inputManager.score += 10 * distance;
        }
        if (this.gameObject.tag == "Torso")
        {
            inputManager.score += 5 * distance;
        }
    }
}
