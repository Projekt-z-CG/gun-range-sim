using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script which handles moving the target on Gun Range Map
 */  
public class MovingTargets : MonoBehaviour {
 
    // Variable for storing speed of moving platform
    public float speed = 0;
 
    // Start is called before the first frame update and sets speed between 5 and 10 if equals 0
    void Start ()
    {
         if(speed == 0) 
             speed = Random.Range(5.0f,10.0f);
    }

    // Update is called once per frame and moves target by given speed
    void Update ()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // If triggered by Structure changes dirrection of moving the target
    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.name == "Structure")
        {
            speed = -speed;
        }
    }

}