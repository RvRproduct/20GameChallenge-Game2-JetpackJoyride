using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public struct ObjectPoolPair
    {
        public string poolTag;
        public int MAXPOOLSIZE;
        public GameObject objectToPool;
    }

    [Header("Object Pool")]
    [SerializeField] protected List<ObjectPoolPair> objectPoolPairs;
    protected Dictionary<string, List<GameObject>> objectPool;

    protected virtual void Awake()
    {
        objectPool = new Dictionary<string, List<GameObject>>();
    }

    protected void SetUpObjectPool()
    {
        foreach (ObjectPoolPair objectPoolPair in objectPoolPairs)
        {
            objectPool.Add(objectPoolPair.poolTag, new List<GameObject>());

            for (int obj = 0; obj < objectPoolPair.MAXPOOLSIZE; obj++)
            {
                objectPool[objectPoolPair.poolTag]
                    .Add(Instantiate(objectPoolPair.objectToPool, gameObject.transform));
                objectPool[objectPoolPair.poolTag][obj].SetActive(false);
            }
        }   
    }

    protected List<ObjectPoolPair> GetObjectPoolPairs()
    {
        return objectPoolPairs;
    }

    protected GameObject GetValidObjectInPool(string _poolTag)
    {
        foreach (GameObject obj in objectPool[_poolTag])
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

}
