using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script which handles mouse input
 */
public class PlayerLook1 : MonoBehaviour
{
    // Object to store camera reference
    public Camera cam;
    // Variable to store current rotation values
    private float xRotation = 0f;

    // Variable to store current rotation values
    public float xSensitivity = 30f;
    // Variable to store current rotation values
    public float ySensitivity = 30f;

    // Start is called before the first frame update, sets cursos block
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Called in the update of input manager, processes input of the mouse and applies it to the camera and player rotation
    public void ProcessLook (Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
