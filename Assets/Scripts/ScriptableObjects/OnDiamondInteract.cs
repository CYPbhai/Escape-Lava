using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/OnDiamondInteract")]
public class OnDiamondInteract : ScriptableObject
{
    public event Action<Vector3, int> OnRaised;

    public void Raise(Vector3 position, int score) => OnRaised?.Invoke(position, score);
}
