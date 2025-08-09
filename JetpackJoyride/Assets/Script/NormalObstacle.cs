using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalObstacle : BaseObstacle, IObstacle
{
    [SerializeField] private float speed;

    private void Update()
    {
        ObstacleAction();
    }

    public void ObstacleAction()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }   
}
