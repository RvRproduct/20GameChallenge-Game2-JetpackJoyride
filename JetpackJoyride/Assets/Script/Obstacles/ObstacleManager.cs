using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolTags;

public class ObstacleManager : ObjectPool
{
    public static ObstacleManager Instance;

    [Header("Obstacle Spawn Weights")]
    private static readonly Dictionary<string, float> obstacleWeights =
        new Dictionary<string, float>
        {
            { ObstacleTags.NormalObstacle, 0.75f },
            { ObstacleTags.RotatingObstacle, 0.50f },
            { ObstacleTags.EnemyObstacle, 0.25f }
        };

    private Dictionary<string, float> availableObstacleWeights;

    [Header("Obstacle Scale Values")]
    private static readonly List<float> obstacleScales =
        new List<float>
        {
            { 2.0f },
            { 4.0f },
            { 6.0f }
        };


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
        availableObstacleWeights = new Dictionary<string, float>();

        foreach (ObjectPoolPair _objectPool in GetObjectPoolPairs())
        {
            if (obstacleWeights.ContainsKey(_objectPool.poolTag))
            {
                availableObstacleWeights.Add
                    (_objectPool.poolTag, obstacleWeights[_objectPool.poolTag]);
            }
        }
    }

    private string RandomizeObstacleSelection()
    {
        float total = 0;
        foreach (float weight in availableObstacleWeights.Values)
        {
            total += weight;
        }

        float randomValue = Random.value * total;

        foreach (KeyValuePair<string, float> obstacleWeight in availableObstacleWeights)
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

    private Quaternion RandomizeObstacleRotation()
    {
        float snapAngle = 45.0f;
        float z = Mathf.Round(Random.Range(0f, 360f) / snapAngle) * snapAngle;
        return Quaternion.Euler(0, 0, z);
    }


    public List<float> GetObstacleScales()
    {
        return obstacleScales;
    }

    private void TransformObstacle(GameObject validObject)
    {
        validObject.transform.position = RandomizedSpawnLocation();
        validObject.transform.rotation = RandomizeObstacleRotation();
        // We are changing the scale values on the object's script themselves
    }

    private void SpawnNormalObstacle()
    {
        GameObject validObject = GetValidObjectInPool(ObstacleTags.NormalObstacle);
        TransformObstacle(validObject);
        
    }

    private void SpawnRotatingObstacle()
    {
        GameObject validObject = GetValidObjectInPool(ObstacleTags.RotatingObstacle);
        TransformObstacle(validObject);
    }

    private void SpawnEnemyObstacle()
    {
        GameObject validObject = GetValidObjectInPool(ObstacleTags.EnemyObstacle);
        validObject.transform.position = RandomizedSpawnLocation();
    }

}
