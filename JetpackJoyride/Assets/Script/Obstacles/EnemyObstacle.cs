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
    private bool onFloor = false;

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
        RandomJumpValue();
    }

    private void Update()
    {
        ObstacleAction();
        EnemyJump();
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
            onFloor = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = false;
        }
    }

    private void EnemyJump()
    {
        currentJumpTime += Time.deltaTime;

        if (onFloor)
        {
            if (currentJumpTime >= maxJumpTime)
            {
                if (rb != null)
                {
                    rb.AddForce(Vector2.up * Random.Range(forceMinAmount, forceMaxAmount), ForceMode2D.Force);
                    RandomJumpValue();
                }
            }
        }
        
        
    }
}
