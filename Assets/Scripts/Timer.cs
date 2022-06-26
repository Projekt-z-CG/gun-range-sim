using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/**
 * Script handling timer behavior
 */
public class Timer : MonoBehaviour
{
    // Variable storing time spent in seconds
    private float timeSpent = 0;
    // Flag storing info if timer is running
    private bool timerIsRunning = false;
    // Flag storing info if Player is near a Button
    public bool nearButton = false;
    // Flag storing info if near by button is Finish Button
    private bool isFinishButton = false;
    // Variable storing reference to staring point of map
    public GameObject spawn;
    // Variable storing reference to timer text
    public TMP_Text timeText;
    // Variable storing refernece to tip text
    public TMP_Text tip;

    // Update is called once per frame, and shows time on timer, shows tips if near button, and handles starting/stopping/restarting timer 
    void Update()
    {
        if (timerIsRunning)
        {
            timeSpent += Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeSpent / 60);
            float seconds = Mathf.FloorToInt(timeSpent % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearButton && !isFinishButton)
            {
                if (!timerIsRunning)
                {
                    timerIsRunning = true;
                }
                else
                {
                    timerIsRunning = false;
                    timeSpent = 0;
                    float minutes = Mathf.FloorToInt(timeSpent / 60);
                    float seconds = Mathf.FloorToInt(timeSpent % 60);
                    timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
            else if (nearButton && isFinishButton)
            {
                if (!timerIsRunning)
                {
                    timeSpent = 0;
                    float minutes = Mathf.FloorToInt(timeSpent / 60);
                    float seconds = Mathf.FloorToInt(timeSpent % 60);
                    timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                    this.transform.rotation = spawn.transform.rotation;
                    this.transform.position = spawn.transform.position;
                }
                else
                {
                    timerIsRunning = false;
                }
            }
        }
        if(Time.timeScale == 0.0f)
        {
            tip.gameObject.SetActive(false);
        }
        else{
            tip.gameObject.SetActive(nearButton);
        }
    }

    // Resets, and stops timer, and moves Plater to Spawn point when lands in Lava, or sets relevant flags when near Button 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Lava")
        {
            timerIsRunning = false;
            timeSpent = 0;
            float minutes = Mathf.FloorToInt(timeSpent / 60);
            float seconds = Mathf.FloorToInt(timeSpent % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            this.transform.rotation = spawn.transform.rotation;
            this.transform.position = spawn.transform.position;
        }
        else if (other.gameObject.name == "Button")
        {
            nearButton = true;
            if(other.gameObject.tag == "FinishButton")
            {
                isFinishButton = true;
            }
        }
    }

    // Resets the relevant flags when moving away from the button.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Button")
        {
            nearButton = false;
            if(other.gameObject.tag == "FinishButton")
            {
                isFinishButton = false;
            }
        }
    }
}