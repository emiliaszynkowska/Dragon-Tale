using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float mouseSensitivity;
    private float xRotation = 0;
    private float yRotation = 0;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation += mouseX;
        yRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        yRotation = Mathf.Clamp(yRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(Mathf.Rad2Deg * yRotation, Mathf.Rad2Deg * xRotation, 0);
        transform.Rotate(Vector3.right * xRotation);
    }
}
