public class MinusScoreText : BaseScoreText
{
    private void OnEnable()
    {
        scoreText.text = "-25";
        animator.Play("PopUpScore");
    }
}
