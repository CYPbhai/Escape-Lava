using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private readonly Dictionary<GameObject, Queue<GameObject>> pools = new();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Prewarm(GameObject prefab, int count)
    {
        if (prefab == null) return;

        Queue<GameObject> queue = GetOrCreateQueue(prefab);
        for (int i = 0; i < count; i++)
        {
            GameObject go = CreateAndRegister(prefab);
            go.SetActive(false);
            queue.Enqueue(go);
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject go = GetFromPool(prefab);
        go.transform.SetPositionAndRotation(position, rotation);
        go.transform.SetParent(parent != null ? parent : transform, true);
        go.SetActive(true);
        return go;
    }

    public GameObject GetFromPool(GameObject prefab)
    {
        if (prefab == null) return null;

        Queue<GameObject> queue = GetOrCreateQueue(prefab);

        GameObject go = null;
        while (queue.Count > 0 && go == null)
            go = queue.Dequeue();

        go ??= CreateAndRegister(prefab);
        return go;
    }

    public void Despawn(GameObject instance)
    {
        if (instance == null || !instance.activeSelf) return;

        PrefabIdentity identity = instance.GetComponent<PrefabIdentity>();
        if (identity == null || identity.prefab == null)
        {
            Destroy(instance);
            return;
        }

        instance.SetActive(false);
        instance.transform.SetParent(transform);
        GetOrCreateQueue(identity.prefab).Enqueue(instance);
    }

    public GameObject CreateAndRegister(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, transform);
        PrefabIdentity identity = go.GetComponent<PrefabIdentity>() ?? go.AddComponent<PrefabIdentity>();
        identity.prefab = prefab;
        go.SetActive(false);
        return go;
    }

    private Queue<GameObject> GetOrCreateQueue(GameObject prefab)
    {
        if (!pools.TryGetValue(prefab, out Queue<GameObject> queue))
        {
            queue = new Queue<GameObject>();
            pools[prefab] = queue;
        }
        return queue;
    }
}