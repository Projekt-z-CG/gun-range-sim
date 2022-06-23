using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    private float timeSpent = 0;
    private bool timerIsRunning = false;
    public bool nearButton = false;
    private bool isFinishButton = false;
    public GameObject spawn;
    public TMP_Text timeText;
    public TMP_Text tip;

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