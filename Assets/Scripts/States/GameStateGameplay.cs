using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateGameplay : GameState
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject[] textAnimationPrefabs;
    [SerializeField] private RayCastHitter rayCasteHitter;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private OnLavaInteract onLavaInteract;
    [SerializeField] private OnDiamondInteract onDiamondInteract;

    [SerializeField] private GameObject[] heartArray;
    private List<GameObject> activeTextObjects;

    int numOfDiamonds;
    int life;

    private void Start()
    {
        activeTextObjects = new List<GameObject>();
        foreach (GameObject go in textAnimationPrefabs)
        {
            PoolManager.Instance.Prewarm(go, 20);
        }
    }

    public override void Construct()
    {
        numOfDiamonds = levelManager.GetNumberOfDiamonds();
        rayCasteHitter.gameObject.SetActive(true);
        gameplayUI.gameObject.SetActive(true);
        life = 5;
        onLavaInteract.OnRaised += OnLavaInteract_OnRaised;
        onDiamondInteract.OnRaised += OnDiamondInteract_OnRaised;
        foreach(GameObject heart in heartArray)
        {
            heart.SetActive(true);
        }
    }

    private void OnDiamondInteract_OnRaised(Vector3 pos, int scr)
    {
        numOfDiamonds--;
        if (numOfDiamonds == 0)
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateWin>());

        GameObject go = PoolManager.Instance.Spawn(textAnimationPrefabs[0],
            new Vector3(pos.x, pos.y, -20),
            Quaternion.identity, canvas.transform);
        activeTextObjects.Add(go);
        StartCoroutine(DespawnHandling(go));
    }

    private void OnLavaInteract_OnRaised(Vector3 pos, int scr)
    {
        life--;
        heartArray[life].SetActive(false);
        if(life == 0)
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateGameOver>());

        GameObject go = PoolManager.Instance.Spawn(textAnimationPrefabs[1],
            new Vector3(pos.x, pos.y, -20),
            Quaternion.identity, canvas.transform);
        StartCoroutine(DespawnHandling(go));
    }

    public override void Destruct()
    {
        rayCasteHitter.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(false);

        onLavaInteract.OnRaised -= OnLavaInteract_OnRaised;
        onDiamondInteract.OnRaised -= OnDiamondInteract_OnRaised;


        StopAllCoroutines();
        foreach (GameObject go in activeTextObjects)
        {
            go.GetComponent<Animator>().Play("IdleScore");
            PoolManager.Instance.Despawn(go);
        }
        activeTextObjects.Clear();
    }

    private IEnumerator DespawnHandling(GameObject go)
    {
        if (!gameObject.activeSelf) yield return null;
        yield return new WaitForSeconds(1f);
        activeTextObjects.Remove(go);
        PoolManager.Instance.Despawn(go);
    }
}
