using UnityEngine;
using UnityEngine.VFX;

public class LavaTile : MonoBehaviour, Interactable
{
    [SerializeField] private VisualEffect lavaEffect;
    public void Interact()
    {
        lavaEffect.Play();
    }

    public void Reset() { }
}
