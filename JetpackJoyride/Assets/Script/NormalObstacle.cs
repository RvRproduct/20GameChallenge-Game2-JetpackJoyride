using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalObstacle : BaseObstacle, IObstacle
{
    [SerializeField] private float speed = 8.0f;

    private void Update()
    {
        ObstacleAction();
    }

    public void ObstacleAction()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }   
}
