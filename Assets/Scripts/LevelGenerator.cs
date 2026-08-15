using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("0 - island, 1 - lava, 2 - diamond")]
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float stepSize;

    [SerializeField] private int width;
    [SerializeField] private int height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                Instantiate(tiles[random], new Vector3(i * stepSize + startPosition.x, j * stepSize + startPosition.y, 0), Quaternion.identity, transform);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
