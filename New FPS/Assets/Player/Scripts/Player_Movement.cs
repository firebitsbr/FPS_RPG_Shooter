﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public AudioSource footsteps;
    public AudioSource landing;
    public AudioSource jump;

    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravityMultiplier = 2f;
    public float groundDistance = 0.2f;
    
    Vector3 velocity;
    bool isGrounded;
    bool isStepping = false;
    bool didJump = false;

    void Update()
    {
        // Checks to see if we're on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // If we're on the ground stay there
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;

            if (didJump) {
                landing.Play();
                didJump = false;
            }
        }

        // Variable gathering
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move the character
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Footsteps if character is moving
        if (!isStepping && isGrounded && (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0) ) {
            StartCoroutine(Footsteps());
        }

        // Jump Implementation
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityMultiplier * -9.81f);
            didJump = true;
            jump.Play();
        }

        // Gravity
        velocity.y += gravityMultiplier * -9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator Footsteps() {
        isStepping = true;
        footsteps.Play();

        yield return new WaitForSeconds(.3f);

        isStepping = false;
    }
}
