using UnityEngine;

public class Scaling : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private float minimumScale = 0.9f;
    [SerializeField] private float maximumScale = 1f;
    [SerializeField] private bool isLava = false;
    private Vector3 currentScale;
    private bool isDirectionForwardX = false;
    private bool isDirectionForwardY = false;
    private void OnEnable()
    {
        if(isLava)
        {
            currentScale = new Vector3(minimumScale, maximumScale, maximumScale);
        }
        else
        {
            currentScale = new Vector3(maximumScale, minimumScale, maximumScale);
        }
    }

    private void Update()
    {
        if(isDirectionForwardX)
        {
            currentScale.x += Time.deltaTime * speed;
        }
        else
        {
            currentScale.x -= Time.deltaTime * speed;
        }
        if(isDirectionForwardY)
        {
            currentScale.y += Time.deltaTime * speed;
        }
        else
        {
            currentScale.y -= Time.deltaTime * speed;
        }


        if (currentScale.x <= minimumScale)
            isDirectionForwardX = true;
        if (currentScale.x >= maximumScale)
            isDirectionForwardX = false;

        if (currentScale.y <= minimumScale)
            isDirectionForwardY = true;
        if (currentScale.y >= maximumScale)
            isDirectionForwardY = false;

        transform.localScale = currentScale;
    }
}
