using System.Collections;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private OnLavaInteract onLavaInteract;
    [SerializeField] private OnDiamondInteract onDiamondInteract;

    private int score = 0;

    private void Start()
    {
        onLavaInteract.OnRaised += OnLavaInteract_OnRaised;
        onDiamondInteract.OnRaised += OnDiamondInteract_OnRaised;
    }
    private void OnEnable()
    {
        score = 0; 
        scoreText.text = "Score: " + score.ToString();
    }
    private void OnDiamondInteract_OnRaised(Vector3 pos, int scr)
    {
        if (!gameObject.activeSelf) return;
        score += scr;
        StartCoroutine(UpdateUI());
    }

    private void OnLavaInteract_OnRaised(Vector3 pos, int scr)
    {
        if (!gameObject.activeSelf) return;

        score += scr;
        if (score <= 0)
            score = 0;
        StartCoroutine(UpdateUI());
    }

    public IEnumerator UpdateUI()
    {
        if (!gameObject.activeSelf) yield return null;
        Vector3 startScale = scoreText.transform.localScale;
        float elapsed = 0f;
        float duration = 0.1f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            scoreText.transform.localScale = Vector3.Lerp(startScale, Vector3.one * 1.2f, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        scoreText.text = "Score: " + score.ToString();

        startScale = scoreText.transform.localScale;
        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            scoreText.transform.localScale = Vector3.Lerp(startScale, Vector3.one, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        scoreText.transform.localScale = Vector3.one;
    }

}
