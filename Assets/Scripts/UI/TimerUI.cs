using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI timerText;
    private float timer = 30f;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        timer = 30f;
        StartCoroutine(ScaleUpAnimation());
    }

    private IEnumerator ScaleUpAnimation()
    {
        float elapsed = 0;
        float duration = 0.3f;

        while(elapsed/duration < 1)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsed / duration);
            yield return null;
        }
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("00");
            fillImage.fillAmount = timer / 30f;
            fillImage.color = Color.Lerp(Color.green, Color.red, (30f-timer) / 30f);
            return;
        }
        GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateGameOver>());
    }
}
