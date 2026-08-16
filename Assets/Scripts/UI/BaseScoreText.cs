using TMPro;
using UnityEngine;

public abstract class BaseScoreText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI scoreText;
    protected Animator animator;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
