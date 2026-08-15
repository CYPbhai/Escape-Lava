using UnityEngine;

public class DynamicLight : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float currentAngle = 180;

    private void Update()
    {
        currentAngle -= speed * Time.deltaTime;

        // Unity handles 360 wrapping automatically
        transform.rotation = Quaternion.Euler(currentAngle, 30f, 0f);
    }
}
