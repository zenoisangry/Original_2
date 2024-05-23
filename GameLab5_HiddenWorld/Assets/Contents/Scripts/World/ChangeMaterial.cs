using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material newMaterial;
    public Material originalMaterial;
    private Renderer objectRenderer;

    void Start()
    {
        // Get the Renderer component attached to the object
        objectRenderer = GetComponent<Renderer>();

        // Set the initial material to the originalMaterial
        if (objectRenderer != null && originalMaterial != null)
        {
            objectRenderer.material = originalMaterial;
        }

        InputManager.OnCooldownTriggered += WallChange; // Subscribe to the event
    }
    private void OnDestroy()
    {
        InputManager.OnCooldownTriggered -= WallChange; // Unsubscribe from the event
    }

    public void WallChange()
    {
        if (objectRenderer != null && newMaterial != null && originalMaterial != null)
        {
            objectRenderer.material = objectRenderer.material == originalMaterial ? newMaterial : originalMaterial;
        }
    }
}