using UnityEngine;

public class RayCastHitter : MonoBehaviour
{
    InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void Start()
    {
        input.Player.Enable();
        input.Player.Attack.started += Attack_started;
    }

    private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(input.Player.MousePosition.ReadValue<Vector2>());
        RaycastHit hitinfo;
        if (Physics.Raycast(ray, out hitinfo))
        {
            hitinfo.collider.TryGetComponent(out Interactable interactable);
            interactable?.Interact();
        }
    }
}