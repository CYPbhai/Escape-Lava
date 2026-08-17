using UnityEngine;
using UnityEngine.VFX;

public class LavaTile : MonoBehaviour, Interactable
{
    [SerializeField] private VisualEffect lavaEffect;
    [SerializeField] private OnLavaInteract onLavaInteract;
    [SerializeField] private int score;
    public void Interact()
    {
        if (!gameObject.activeSelf) return;
        lavaEffect.Play();
        onLavaInteract?.Raise(transform.position, score);
    }
}
