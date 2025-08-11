// Game and Code By RvRproduct (Roberto Reynoso)

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
    [SerializeField] private GameObject shieldObject;
    private SpriteRenderer spriteRenderer;
    private bool isUsingJetpack = false;
    private bool hasShield = false;
    private BoxCollider2D boxCollider2D;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip poofSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        inputActions = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        inputActions.Enable();
    }

    private void OnEnable()
    {
        inputActions.Player.Jetpack.started += IsUsingJetpack;
        inputActions.Player.Jetpack.canceled += IsNotUsingJetpack;
    }

    private void OnDisable()
    {
        inputActions.Player.Jetpack.started -= IsUsingJetpack;
        inputActions.Player.Jetpack.canceled -= IsNotUsingJetpack;
    }

    private void Start()
    {
        GameManager.Instance.SetBestTimeOnStart();
        StartCoroutine(WaitForObstacles());
    }

    private void FixedUpdate()
    {
        UsingJetpack();
    }

    private void UsingJetpack()
    {
        if (isUsingJetpack)
        {
            ProjectileManager.Instance.SpawnGemProjectileInWorld();

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
        if (spriteRenderer != null)
        {
            PlayerAnimationManager.Instance.TryTriggerJump();
            PlayerAnimationManager.Instance.SetIsJumping(true);
        }
        
        isUsingJetpack = true;
    }

    private void IsNotUsingJetpack(InputAction.CallbackContext context)
    {
        if (spriteRenderer != null)
        {
            PlayerAnimationManager.Instance.TryTriggerFall();
            PlayerAnimationManager.Instance.SetIsJumping(false);
        }
        
        isUsingJetpack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<IObstacle>() != null
            && GameManager.Instance.GetObstacleStart()
            && GameManager.Instance.GetStartGame())
        {
            if (!hasShield && !PlayerAnimationManager.Instance.GetIsHurting())
            {
                SoundManager.Instance.PlaySoundEffect(poofSound);
                boxCollider2D.enabled = false;
                GameManager.Instance.SetIsGameOver(true);
                PlayerAnimationManager.Instance.TryTriggerDeath();
            }
            else
            {
                if (!PlayerAnimationManager.Instance.GetIsHurting())
                {
                    PlayerAnimationManager.Instance.SetIsHurting(true);
                    SoundManager.Instance.PlaySoundEffect(hurtSound);
                    PlayerAnimationManager.Instance.TryTriggerHurt();
                    shieldObject.SetActive(false);
                    hasShield = false;
                }
                
            }

            // WHY LMAO?
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.SetActive(false);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IObstacle>() != null
            && GameManager.Instance.GetObstacleStart()
            && GameManager.Instance.GetStartGame())
        {
            if (!hasShield && !PlayerAnimationManager.Instance.GetIsHurting())
            {
                SoundManager.Instance.PlaySoundEffect(poofSound);
                boxCollider2D.enabled = false;
                GameManager.Instance.SetIsGameOver(true);
                PlayerAnimationManager.Instance.TryTriggerDeath();
            }
            else
            {
                if (!PlayerAnimationManager.Instance.GetIsHurting())
                {
                    PlayerAnimationManager.Instance.SetIsHurting(true);
                    SoundManager.Instance.PlaySoundEffect(hurtSound);
                    PlayerAnimationManager.Instance.TryTriggerHurt();
                    shieldObject.SetActive(false);
                    hasShield = false;
                }
            }
            

            collision.gameObject.SetActive(false);
        }
    }

    public void ActivateShield()
    {
        hasShield = true;
        shieldObject.SetActive(true);
    }

    public bool IsShieldActive()
    {
        return hasShield;
    }

    public void StopHurting()
    {
        PlayerAnimationManager.Instance.SetIsHurting(false);
    }

    public void OnDeath()
    {
        GameManager.Instance.SetBestTime();
        GameManager.Instance.RefreshGame();
        gameObject.SetActive(false);
        GameManager.Instance.ReadyRestartInput();
    }

    private IEnumerator WaitForObstacles()
    {
        while (!GameManager.Instance.GetStartGame())
        {
            yield return null; 
        }

        boxCollider2D.enabled = true;
    }
}
