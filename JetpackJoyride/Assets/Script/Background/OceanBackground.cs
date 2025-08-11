// Game and Code By RvRproduct (Roberto Reynoso)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanBackground : BasePoolObject
{
    [SerializeField] private float speed = 8;

    protected override string ProvidePoolReturnTag()
    {
        return PoolTags.BackgroundReturnTags.BackgroundReturn;
    }

    protected override string ProvidePoolTag()
    {
        return PoolTags.BackgroundTags.OceanBackground;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == PoolTags.BackgroundSpawnTags.BackgroundSpawn)
        {
            BackgroundManager.Instance.SpawnBackgroundInWorld();
        }

        if (collision.gameObject.tag == PoolTags.BackgroundReturnTags.BackgroundReturn)
        {
            gameObject.SetActive(false);
        }
    }
}
