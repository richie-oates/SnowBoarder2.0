using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] float boostAmount = 1.5f;
    [SerializeField] float boostDuration = 5f;
    [TagSelector]
    [SerializeField] string groundTag;

    SurfaceEffector2D surfaceEffector2D;
    PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        surfaceEffector2D = GameObject.FindGameObjectWithTag(groundTag).GetComponent<SurfaceEffector2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerController.isGrounded)
        {
           StartCoroutine(BoostSpeedForSeconds(boostAmount, boostDuration));
        }
    }

    IEnumerator BoostSpeedForSeconds(float amount, float duration)
    {
        surfaceEffector2D.speed *= amount;
        yield return new WaitForSeconds(duration);
        surfaceEffector2D.speed /= amount;
    }
}
