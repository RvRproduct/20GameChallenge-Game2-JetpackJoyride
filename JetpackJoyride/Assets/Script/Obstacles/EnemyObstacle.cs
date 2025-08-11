using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacle : BasePoolObject, IObstacle
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float forceMaxAmount = 600.0f;
    [SerializeField] private float maxJumpTime = 2.0f;
    private float currentJumpTime = 0.0f;
    private float randomJumpMax = 1.0f;
    private float forceMinAmount = 100.0f;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private ParticleSystem deathParticleSystem;
    private SpriteRenderer spriteRenderer;

    [Header("Animation")]
    private Animator animator;

    protected override string ProvidePoolTag()
    {
        return PoolTags.ObstacleTags.EnemyObstacle;
    }

    protected override string ProvidePoolReturnTag()
    {
        return PoolTags.ObstacleReturnTags.ObstacleReturn;
    }

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        deathParticleSystem = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        RandomJumpValue();
    }

    private void OnEnable()
    {
        boxCollider2D.enabled = true;
        spriteRenderer.enabled = true;
    }

    private void Update()
    {
        ObstacleAction();
        EnemyJump();
    }

    private void FixedUpdate()
    {
        if (!GetIsFalling() && rb.velocity.y < 0.01f)
        {
            if (!GetOnFloor())
            {
                SetIsFalling(true);
            }
        }
        else
        {
            if (!GetOnFloor())
            {
                SetIsJumping(true);
            }
        }
    }

    public void ObstacleAction()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void RandomJumpValue()
    {
        currentJumpTime = Random.Range(0, randomJumpMax);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            SetOnFloor(true);
            SetIsFalling(false);
            SetIsJumping(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (!GetOnFloor())
            {
                SetOnFloor(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            SetOnFloor(false);
        }
    }

    private void EnemyJump()
    {
        currentJumpTime += Time.deltaTime;

        if (GetOnFloor())
        {
            if (currentJumpTime >= maxJumpTime)
            {
                if (rb != null)
                {
                    SetIsJumping(true);
                    rb.AddForce(Vector2.up * Random.Range(forceMinAmount, forceMaxAmount), ForceMode2D.Force);
                    RandomJumpValue();
                }
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        BasePoolObjectTrigger(collision);

        if (collision.gameObject.tag == "Projectile")
        {
            if (!ProjectileManager.Instance.GetPlayerController().IsShieldActive())
            {
                ProjectileManager.Instance.GetPlayerController().ActivateShield();
            }

            // Return Objects To Pool
            collision.gameObject.SetActive(false);
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            deathParticleSystem.Play();
            StartCoroutine(OnDeath());
        }
    }

    private IEnumerator PlayDeathParticle()
    {
        while (deathParticleSystem.IsAlive())
        {
            yield return null;
        }
    }

    private IEnumerator OnDeath()
    {
        yield return StartCoroutine(PlayDeathParticle());

        gameObject.SetActive(false);
    }

    private void SetIsFalling(bool _isFalling)
    {
        animator.SetBool("IsFalling", _isFalling);
    }

    private bool GetIsFalling()
    {
        return animator.GetBool("IsFalling");
    }

    private void SetIsJumping(bool _isJumping)
    {
        animator.SetBool("IsJumping", _isJumping);
    }

    private bool GetIsJumping()
    {
        return animator.GetBool("IsJumping");
    }

    private void SetOnFloor(bool _onFloor)
    {
        animator.SetBool("OnFloor", _onFloor);
    }

    private bool GetOnFloor()
    {
        return animator.GetBool("OnFloor");
    }



}
