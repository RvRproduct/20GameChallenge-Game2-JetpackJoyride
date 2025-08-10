using PoolTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : ObjectPool
{
    public static ProjectileManager Instance;

    [Header("Spawn Point")]
    [SerializeField] Transform spawnPoint;

    [Header("Spawn Rate")]
    [SerializeField] private float maxSpawnRateTime = 0.25f;
    private float currentSpawnRateTime;

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
        currentSpawnRateTime = maxSpawnRateTime;
    }

    private void Start()
    {
        SetUpObjectPool();
    }

    private void Update()
    {
        if (currentSpawnRateTime < maxSpawnRateTime)
        {
            currentSpawnRateTime += Time.deltaTime;
        }
    }

    public void SpawnGemProjectileInWorld()
    {
        if (currentSpawnRateTime >= maxSpawnRateTime)
        {
            GameObject validObject = GetValidObjectInPool(ProjectileTags.GemProjectile);
            validObject.transform.position = spawnPoint.position;
            currentSpawnRateTime = 0.0f;
        }
        
    }
}
