using System.Collections;
using UnityEngine;

public class GameStateWin : GameState
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private LevelManager levelManager;

    float timer;

    public override void Construct()
    {
        timer = 3f;
        winUI.SetActive(true);
        StartCoroutine(UnLoadLevel());
    }

    public override void Loop()
    {
        if(timer >0)
        {
            timer -= Time.deltaTime;
            return;
        }
        GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateInit>());
    }

    public override void Destruct()
    {
        winUI.SetActive(false);
    }

    private IEnumerator UnLoadLevel()
    {
        yield return new WaitForSeconds(1f);
        levelManager.DegenerateLevel();
    }
}
