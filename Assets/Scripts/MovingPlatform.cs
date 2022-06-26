using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script which handles moving the platform on Parkour Map
 */ 
public class MovingPlatform : MonoBehaviour {
    
    // Variable for storing speed of moving platform
    public float speed = 0;
 
    // Start is called before the first frame update and sets speed between 5 and 10 if equals 0
    void Start ()
    {
        if(speed == 0) 
            speed = Random.Range(5.0f,10.0f);
    }

    // Update is called once per frame and moves platform by given speed
    void Update ()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    /**
     * If triggered by Structure changes dirrection of moving platform
     * If triggered by Player sets the moving platform as parent of Player so it moves with platform
     */
    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.name == "Structure")
        {
            speed = -speed;
        }
        if(collider.gameObject.tag == "Player")
        {
            collider.transform.parent = transform;
            // StartCoroutine(deactivatePlatform());
        }
    }

    /**
     * If trigger exited by Player resets the parent of Player so it stays in place
     */
    void OnTriggerExit(Collider collider) {
        if(collider.tag == "Player") 
        {
            collider.gameObject.transform.parent = GameObject.Find("Structure").transform;
        }
    }
}