using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : BasePoolObject, IObstacle
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    protected override string ProvidePoolTag()
    {
        return PoolTags.ObstacleTags.RotatingObstacle;
    }

    protected override string ProvidePoolReturnTag()
    {
        return PoolTags.ObstacleReturnTags.ObstacleReturn;
    }

    private void Update()
    {
        ObstacleAction();
    }

    public void ObstacleAction()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
