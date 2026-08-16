using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class DiamondTile : MonoBehaviour, Interactable
{
    [SerializeField] private VisualEffect diamondCollectVFX;
    [SerializeField] private Transform visual;
    [SerializeField] private float collectAnimationSpeed = 1;

    [SerializeField] private OnDiamondInteract onDiamondInteract;
    [SerializeField] private int score;
    private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void Interact()
    {
        diamondCollectVFX.Play();
        col.enabled = false;
        onDiamondInteract?.Raise(transform.position, score);
        StartCoroutine(ScaleDownAnimation());
    }

    public void Reset() 
    {
        visual.localScale = Vector3.one;
        col.enabled = true;
    }

    IEnumerator ScaleDownAnimation()
    {
        Vector3 startScale = visual.localScale;
        float elapsed = 0f;
        float duration = 1f / collectAnimationSpeed;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            visual.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        visual.localScale = Vector3.zero;
    }
}
