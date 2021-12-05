using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [TagSelector]
    [SerializeField] string groundTag;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] float sceneReloadDelay;

    PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(groundTag))
        {
            playerController.Crashed();
            crashEffect.Play();
            Invoke("ReloadScene", sceneReloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
