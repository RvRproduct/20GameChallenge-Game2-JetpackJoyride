using PoolTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ObstacleReturnTags.ObstacleReturn)
        {
            gameObject.SetActive(false);
        }
    }
}
