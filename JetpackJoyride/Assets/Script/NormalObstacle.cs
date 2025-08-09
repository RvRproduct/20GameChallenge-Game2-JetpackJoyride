using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalObstacle : BasePoolObject, IObstacle
{
    [SerializeField] private float speed = 8.0f;

    protected override string ProvidePoolTag()
    {
        return PoolTags.ObstacleTags.NormalObstacle;
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
    }

   
}
