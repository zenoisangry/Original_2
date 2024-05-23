using UnityEngine;
public class ToggleVisibility : MonoBehaviour
{
    [SerializeField] private bool world2;
    private void Start()
    {
        SetInitialVisibility();
        InputManager.OnCooldownTriggered += Change; // Subscribe to the event
    }

    private void OnDestroy()
    {
        InputManager.OnCooldownTriggered -= Change; // Unsubscribe from the event
    }

    private void SetInitialVisibility()
    {
        gameObject.SetActive(!world2);
    }

    public void Change()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}