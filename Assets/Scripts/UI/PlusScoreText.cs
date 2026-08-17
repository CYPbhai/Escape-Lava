using UnityEngine;

public class PlusScoreText : BaseScoreText
{
    private void OnEnable()
    {
        scoreText.text = "+50";
        animator.Play("PopUpScore");
    }
}
