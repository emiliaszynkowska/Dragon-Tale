using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private float speed;
    private float gravity = 10;
    private Vector3 movement;
    private Vector3 velocity;
    private CharacterController controller;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        movement = transform.right * x + transform.forward * z;
        movement.y = 0;
        controller.Move(movement * speed * Time.deltaTime);
        
        // Sprint
        if (Input.GetKey("left shift"))
            speed = movementSpeed * 2;
        else
        {
            speed = movementSpeed;
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = 0;
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

}
