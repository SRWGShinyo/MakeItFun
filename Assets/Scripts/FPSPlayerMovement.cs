using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public float jumpHeight = 3f;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    // Update is called once per frame

    Vector3 currentVelocity;
    bool isGrounded = true;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        anim.SetBool("isGround", isGrounded);

        if (isGrounded && currentVelocity.y < 0)
            currentVelocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        anim.SetBool("isRunning", move != Vector3.zero);

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            currentVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        currentVelocity.y += gravity * Time.deltaTime;

        controller.Move(currentVelocity * Time.deltaTime);
    }
}
