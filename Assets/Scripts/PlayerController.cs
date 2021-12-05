using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount;
    [SerializeField] float jumpAmount;
    [SerializeField] float speedChange;
    [TagSelector]
    [SerializeField] string groundTag;

    public bool isGrounded;

    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb2D;
    bool isJumping;
    float horizontalInput;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = GameObject.FindGameObjectWithTag(groundTag).GetComponent<SurfaceEffector2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            surfaceEffector2D.speed *= speedChange;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            surfaceEffector2D.speed *= 1 / speedChange;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if(!isGrounded && Mathf.Abs(horizontalInput) > 0.01)
        {
            RotatePlayer();
        }
        else if (isJumping)
        {
            JumpPlayer();
        }
    }

    private void JumpPlayer()
    {
        rb2D.AddForce(Vector2.up * jumpAmount);
        isJumping = false;
    }

    private void RotatePlayer()
    {
        rb2D.AddTorque(-horizontalInput * torqueAmount);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
        }
    }
}
