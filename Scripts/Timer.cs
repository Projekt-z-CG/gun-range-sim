using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    public float timeSpent = 0;
    public bool timerIsRunning = false;
    public GameObject player;
    public GameObject spawn;
    public TMP_Text timeText;

    void Update()
    {
        if (timerIsRunning)
        {
            timeSpent += Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeSpent / 60);
            float seconds = Mathf.FloorToInt(timeSpent % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " trigger enter");
        if (other.gameObject == player)
        {
            timeSpent = 0;
            if (gameObject.name == "Lava")
            {
                player.transform.rotation = spawn.transform.rotation;
                player.transform.position = spawn.transform.position;
            }
            if (gameObject.name == "Button")
            {
                if(!timerIsRunning)
                    timerIsRunning = true;
                else
                    timerIsRunning = false;
            }
        }
    }
}