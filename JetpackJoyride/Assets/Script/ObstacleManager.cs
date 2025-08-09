using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTags;

public class ObstacleManager : ObjectPool
{
    public static ObstacleManager Instance;

    [Header("Obstacle Spawn Weights")]
    private Dictionary<string, float> obstacleWeights;

    [Header("Spawn Points")]
    [SerializeField] private Transform upperSpawnPoint;
    [SerializeField] private Transform lowerSpawnPoint;

    [Header("Spawn Rate")]
    [SerializeField] private float maxSpawnRateTime = 2.0f;
    private float currentSpawnRateTime = 0.0f;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Awake();

        SetUpObstacleWeights();
    }

    private void Start()
    {
        SetUpObjectPool();
        SpawnObstacleInWorld();
    }

    private void Update()
    {
        SpawnTimer();
    }

    private void SetUpObstacleWeights()
    {
        obstacleWeights = new Dictionary<string, float>();
        obstacleWeights.Add(ObstacleTags.NormalObstacle, 0.75f);
        obstacleWeights.Add(ObstacleTags.RotatingObstacle, 0.50f);
        obstacleWeights.Add(ObstacleTags.EnemyObstacle, 0.25f);
    }

    private string RandomizeObstacleSelection()
    {
        float total = 0;
        foreach (float weight in obstacleWeights.Values)
        {
            total += weight;
        }

        float randomValue = Random.value * total;

        foreach (KeyValuePair<string, float> obstacleWeight in obstacleWeights)
        {
            if (randomValue < obstacleWeight.Value)
            {
                return obstacleWeight.Key;
            }

            randomValue -= obstacleWeight.Value;
        }

        return ObstacleTags.NormalObstacle;

    }

    private void SpawnTimer()
    {
        currentSpawnRateTime += Time.deltaTime;

        if (currentSpawnRateTime >= maxSpawnRateTime)
        {
            SpawnObstacleInWorld();
            currentSpawnRateTime = 0;
        }
    }

    private void SpawnObstacleInWorld()
    {
        switch(RandomizeObstacleSelection())
        {
            case ObstacleTags.NormalObstacle:
                SpawnNormalObstacle();
                break;
            case ObstacleTags.RotatingObstacle:
                SpawnRotatingObstacle();
                break;
            case ObstacleTags.EnemyObstacle:
                SpawnEnemyObstacle();
                break;
        }
    }

    private Vector3 RandomizedSpawnLocation()
    {
        Vector3 spawnLocation = upperSpawnPoint.position;

        spawnLocation = new Vector3(
            spawnLocation.x,
            Random.Range(lowerSpawnPoint.position.y, upperSpawnPoint.position.y),
            spawnLocation.z);

        return spawnLocation;
    }

    private void SpawnNormalObstacle()
    {
        GetValidObjectInPool(ObstacleTags.NormalObstacle)
            .transform.position = RandomizedSpawnLocation();
    }

    private void SpawnRotatingObstacle()
    {

    }

    private void SpawnEnemyObstacle()
    {

    }

}
