using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int distance = 1;
    [SerializeField] GameObject hitEffect;
    InputManager inputManager;

    void Start()
    {
        inputManager = GameObject.Find("Player Switch 1").GetComponent<InputManager>();
    }

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
