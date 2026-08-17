using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    GameState state;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        state = GetComponent<GameStateInit>();
        state.Construct();
    }

    private void Update()
    {
        state.Loop();
    }

    public void ChangeState(GameState newState)
    {
        state.Destruct();
        state = newState;
        state.Construct();
    }
}
