// Game and Code By RvRproduct (Roberto Reynoso)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : BasePoolObject, IObstacle
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        // This is where we randomize the scale values
        // When the Object Spawns, this is quick and dirty I know
        float randomScale = RandomizeObstacleYScale();
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, randomScale);
        boxCollider2D.size = new Vector2(boxCollider2D.size.x, randomScale);
    }

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

    private float RandomizeObstacleYScale()
    {
        int randomScale = Random.Range(0, ObstacleManager.Instance.GetObstacleScales().Count - 1);
        return ObstacleManager.Instance.GetObstacleScales()[randomScale];
    }
}
