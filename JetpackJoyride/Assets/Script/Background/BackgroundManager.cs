// Game and Code By RvRproduct (Roberto Reynoso)

using PoolTags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : ObjectPool
{
    public static BackgroundManager Instance;

    [Header("Spawn Point")]
    [SerializeField] private Transform backgroundSpawnPoint;

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
    }

    private void Start()
    {
        SetUpObjectPool();
        StartingSpawnBackgroundInWorld();
    }


    public void SpawnBackgroundInWorld()
    {
        GameObject validObject = GetValidObjectInPool(BackgroundTags.OceanBackground);
        validObject.transform.position = backgroundSpawnPoint.position;
    }

    private void StartingSpawnBackgroundInWorld()
    {
        GameObject validObject = GetValidObjectInPool(BackgroundTags.OceanBackground);
        validObject.transform.position = Vector3.zero;
    }
}
