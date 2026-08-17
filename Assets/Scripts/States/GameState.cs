using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public virtual void Construct() { }
    public virtual void Loop() { }
    public virtual void Destruct() { }
}
