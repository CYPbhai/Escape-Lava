using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject initUI;
    float timer = 3;

    public override void Construct()
    {
        initUI.SetActive(true);
        timer = 3;
        levelManager.GenerateLevel();
    }
    public override void Loop()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                timer = 0;
        }
        else
        {
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateGameplay>());
        }
        timerText.text = timer.ToString("0");
    }
    public override void Destruct()
    {
        initUI.gameObject.SetActive(false);
    }
}
