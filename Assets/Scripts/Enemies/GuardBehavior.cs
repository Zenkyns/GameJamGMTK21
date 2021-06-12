﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehavior : MonoBehaviour
{
    // FIELDS
    // SERIALIZED FIELDS
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float deathLimit = 30f;
    [SerializeField] private bool isRunningRight = false;

    // PRIVATE FIELDS
    private float guardSpeed;
    // Start is called before the first frame update
    void Start()
    {
        guardSpeed = Random.Range(minSpeed, maxSpeed);

        // Changes the run direction function of the spawn position
        if(transform.position.x >= 0)
        {
            isRunningRight = false;
        }
        else
        {
            isRunningRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (isRunningRight)
        {
            case false:
                // Therefore the guard is running toward the left side
                transform.Translate(Vector2.left * Time.deltaTime * guardSpeed);
                break;

            case true:
                // The guard is running toward the right side
                transform.Translate(Vector2.right * Time.deltaTime * guardSpeed);
                break;
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
    }
}