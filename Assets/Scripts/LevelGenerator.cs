using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float stepSize;

    [SerializeField] private int width;
    [SerializeField] private int height;

    List<GameObject> activeObjects = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        foreach(GameObject tile in tiles)
        {
            PoolManager.Instance.Prewarm(tile, 45);
        }
    }

    void Start()
    {
        StartCoroutine(GenerateLevel());
    }

    public IEnumerator GenerateLevel()
    {
        for(int i=0; i<width; ++i)
        {
            for(int j=0; j<height; ++j)
            {
                int random = Random.Range(0, 3);
                activeObjects.Add(
                PoolManager.Instance.Spawn(tiles[random], 
                    new Vector3(i * stepSize + startPosition.x, j * stepSize + startPosition.y, 0), 
                    Quaternion.identity, transform));
                yield return new WaitForEndOfFrame();
            }
        }
        activeObjects.Reverse();
        StartCoroutine(DegenerateLevel());
    }

    public IEnumerator DegenerateLevel()
    {
        foreach(GameObject ao in activeObjects)
        {
            PoolManager.Instance.Despawn(ao);
            yield return new WaitForEndOfFrame();
        }
        activeObjects.Clear();
    }
}
