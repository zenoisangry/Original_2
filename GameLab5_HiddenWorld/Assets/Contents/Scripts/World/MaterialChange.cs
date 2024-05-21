using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material newMaterial;
    public Material originalMaterial;
    private Renderer objectRenderer;

    void Start()
    {
        // Save the original material
        if (TryGetComponent(out objectRenderer))
        {
            originalMaterial = objectRenderer.material;
        }
    }
    public void WallChange()
    {
            // Toggle between the original and new material
            if (objectRenderer != null && newMaterial != null)
            {
                if (objectRenderer.material == originalMaterial)
                {
                    objectRenderer.material = newMaterial;
                }
                else
                {
                    objectRenderer.material = originalMaterial;
                }
            }
    }
}