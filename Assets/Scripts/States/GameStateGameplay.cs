using UnityEngine;

public class GameStateGameplay : GameState
{
    [SerializeField] private RayCastHitter rayCasteHitter;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private OnLavaInteract onLavaInteract;
    [SerializeField] private OnDiamondInteract onDiamondInteract;

    [SerializeField] private GameObject[] heartArray;
    int numOfDiamonds;
    int life;
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

    private void OnDiamondInteract_OnRaised(Vector3 arg1, int arg2)
    {
        numOfDiamonds--;
        if (numOfDiamonds == 0)
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateWin>());
    }

    private void OnLavaInteract_OnRaised(Vector3 arg1, int arg2)
    {
        life--;
        heartArray[life].SetActive(false);
        if(life == 0)
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateGameOver>());
    }

    public override void Destruct()
    {
        rayCasteHitter.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(false);

        onLavaInteract.OnRaised -= OnLavaInteract_OnRaised;
        onDiamondInteract.OnRaised -= OnDiamondInteract_OnRaised;

        levelManager.DegenerateLevel();
    }
}
