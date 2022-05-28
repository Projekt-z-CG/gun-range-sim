using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MovingPlatform : MonoBehaviour {
 
    public float speed;
 
    void Start ()
    {
        if(speed == 0) speed = Random.Range(10.0f,20.0f);
        // StartCoroutine(removePlatform());
    }

    void Update ()
    {
        Debug.Log(speed);
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        Debug.Log(speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Object that collided with me: " + collision.gameObject.name);
        speed = -speed;
    }

    IEnumerator destroyPlatform()
    {
        yield return new WaitForSeconds (2);
        gameObject.SetActive(false);
        yield return new WaitForSeconds (2);
        gameObject.SetActive(true);
    }
}