﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Home
{
    public class PlayerRotation : MonoBehaviour
    {
        public float mouseSensitivity;
        private float xRotation;
        private float yRotation;
        private Camera cam;

        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            // Get Mouse Input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            // Set Rotation Variables
            xRotation += mouseX;
            yRotation -= mouseY;
            // Set X Rotation (Player)
            transform.localRotation = Quaternion.Euler(0, Mathf.Rad2Deg * xRotation, 0);
            // Set Y Rotation (Camera)
            cam.transform.localRotation = Quaternion.Euler(Mathf.Clamp(Mathf.Rad2Deg * yRotation, -45, 45), 0, 0);
        }

    }
}