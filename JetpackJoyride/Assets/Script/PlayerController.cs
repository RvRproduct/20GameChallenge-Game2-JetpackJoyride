using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    InputActions inputActions;
    Rigidbody2D rb;
    [SerializeField] private float accerleration = 10f;
    [SerializeField] private float maxSpeed = 5.0f;
    private bool isUsingJetpack = false;

    private void Awake()
    {
        inputActions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        inputActions.Enable();
        inputActions.Player.Jetpack.started += IsUsingJetpack;
        inputActions.Player.Jetpack.canceled += IsNotUsingJetpack;
    }

    private void FixedUpdate()
    {
        UsingJetpack();
    }

    private void UsingJetpack()
    {
        if (isUsingJetpack)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(Vector2.up * accerleration * rb.mass, ForceMode2D.Force);
            }

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    private void IsUsingJetpack(InputAction.CallbackContext context)
    {
        isUsingJetpack = true;
    }

    private void IsNotUsingJetpack(InputAction.CallbackContext context)
    {
        isUsingJetpack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IObstacle>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IObstacle>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
