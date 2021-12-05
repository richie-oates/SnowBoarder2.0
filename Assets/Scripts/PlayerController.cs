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
    [SerializeField] ParticleSystem snowTrailEffect;

    public bool isGrounded;
    bool hasCrashed;

    AudioSource audioSource;
    [SerializeField] AudioClip crashSoundEffect;
    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb2D;
    bool isJumping;
    float horizontalInput;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = GameObject.FindGameObjectWithTag(groundTag).GetComponent<SurfaceEffector2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        SpeedControl();
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!hasCrashed)
        { 
            RotatePlayer();
            JumpPlayer();
        }
    }

    private void JumpPlayer()
    {
        if (isGrounded && isJumping)
        {
            rb2D.AddForce(Vector2.up * jumpAmount);
            isJumping = false;
        }
    }

    private void SpeedControl()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            surfaceEffector2D.speed *= speedChange;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            surfaceEffector2D.speed *= 1 / speedChange;
        }
    }

    private void RotatePlayer()
    {
        if (!isGrounded && Mathf.Abs(horizontalInput) > 0.01)
        {
            rb2D.AddTorque(-horizontalInput * torqueAmount);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag) && !hasCrashed)
        {
            isGrounded = true;
            snowTrailEffect.Play();
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            isGrounded = false;
            snowTrailEffect.Stop();
        }
    }

    public void Crashed()
    {
        
        if (!hasCrashed) audioSource.PlayOneShot(crashSoundEffect);
        hasCrashed = true;
    }
}
