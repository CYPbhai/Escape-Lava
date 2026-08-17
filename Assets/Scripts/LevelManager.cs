using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float stepSize;

    [SerializeField] private int width;
    [SerializeField] private int height;

    int numberOfDiamonds = 0;

    List<GameObject> activeObjects = new List<GameObject>();

    void Start()
    {
        foreach (GameObject tile in tiles)
        {
            PoolManager.Instance.Prewarm(tile, 45);
        }
    }

    public void GenerateLevel()
    {
        numberOfDiamonds = 0;
        StartCoroutine(PopulateLevel());
    }

    public void DegenerateLevel()
    {
        StartCoroutine(DepopulateLevel());
    }

    public void NextLvel()
    {
        StartCoroutine(GenerateNextLevel());
    }

    private IEnumerator PopulateLevel()
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
                if (random == 2) numberOfDiamonds++;
                yield return new WaitForEndOfFrame();
            }
        }
        activeObjects.Reverse();
    }

    private IEnumerator DepopulateLevel()
    {
        foreach(GameObject ao in activeObjects)
        {
            PoolManager.Instance.Despawn(ao);
            yield return new WaitForEndOfFrame();
        }
        activeObjects.Clear();
    }

    private IEnumerator GenerateNextLevel()
    {
        DegenerateLevel();
        yield return new WaitForSeconds(2f);
        GenerateLevel();
    }

    public int GetNumberOfDiamonds()
    {
        return numberOfDiamonds;
    }
}
