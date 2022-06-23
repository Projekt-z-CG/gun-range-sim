using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MovingTargets : MonoBehaviour {
 
    public float speed;
    public bool triggered = false;
 
    void Start ()
    {
        // if(speed == 0) 
        //     speed = Random.Range(5.0f,10.0f);
    }

    void Update ()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.name == "Structure")
        {
            speed = -speed;
        }
        if(collider.gameObject.tag == "Player")
        {
            //Debug.Log("Trigger enter");
            collider.transform.parent = transform;
            // StartCoroutine(deactivatePlatform());
        }
    }

    void OnTriggerExit(Collider collider) {
        //Debug.Log(collider.gameObject.name + " Trigger exit");
        if(collider.tag == "Player") 
        {
            //Debug.Log("Trigger exit");
            collider.gameObject.transform.parent = GameObject.Find("Structure").transform;
            // collider.gameObject.transform.SetParent(GameObject.Find("Structure").transform, true);
        }
    }
}