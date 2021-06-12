﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBirdBehavior : MonoBehaviour
{
    // FIELDS
    // SERIALIZED FIELDS
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float deathLimit = 30f;
    [SerializeField] private bool isFlyingRight = false;
    [SerializeField] private float AlertBoundary = 30f;

    // PRIVATE FIELDS
    private float birdSpeed;
    private bool hasCrossedBoundaries = false;

    // REFERENCES
    private SpawnManager spawnManagerScript;
    private Animator letterBirdAnimator;
    private AudioSource letterBirdAudioSource;

    // SFX
    private AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        birdSpeed = Random.Range(minSpeed, maxSpeed);

        // Changes the run direction function of the spawn position
        if (transform.position.x >= 0)
        {
            isFlyingRight = false;
        }
        else
        {
            isFlyingRight = true;
        }

        // Reference
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        letterBirdAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // LetterBird's movement
        switch (isFlyingRight)
        {
            case false:
                // Therefore the guard is running toward the left side
                transform.Translate(Vector2.left * Time.deltaTime * birdSpeed);
                break;

            case true:
                // The guard is running toward the right side
                transform.Translate(Vector2.right * Time.deltaTime * birdSpeed);
                break;
        }

        // If the LetterBird reaches the boundaries
        if(isFlyingRight && transform.position.x >= AlertBoundary && hasCrossedBoundaries == false)
        {
            Debug.Log("ALERT right !!!");
            spawnManagerScript.isAlert = true;
            spawnManagerScript.alertCounter = 0;
            hasCrossedBoundaries = true;
        }
        else if((!isFlyingRight) && transform.position.x < -AlertBoundary && hasCrossedBoundaries == false)
        {
            Debug.Log("ALERT left !!!");
            spawnManagerScript.isAlert = true;
            spawnManagerScript.alertCounter = 0;
            hasCrossedBoundaries = true;
        }

        // Destroy the game object if it cross the death boundery
        if (Mathf.Abs(transform.position.x) >= deathLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            birdSpeed = 0;
            letterBirdAnimator.SetBool("isDead", true);

            // SFX
            letterBirdAudioSource.Stop();
            letterBirdAudioSource.PlayOneShot(deathSound);
        }
    }
}
