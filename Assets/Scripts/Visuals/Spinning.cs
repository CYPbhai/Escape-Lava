using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    private float currentAngle;

    private void OnEnable()
    {
        currentAngle = Random.Range(0f, 360f);
    }

    private void Update()
    {
        currentAngle -= speed * Time.deltaTime;

        // Unity handles 360 wrapping automatically
        transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
    }
}
