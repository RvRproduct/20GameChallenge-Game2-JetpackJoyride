using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : BaseObstacle, IObstacle
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

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
