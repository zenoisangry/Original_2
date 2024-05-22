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
    }
    public void WallChange()
    {
            // Toggle between the original and new material
            if (objectRenderer != null && newMaterial != null)
            {
                if (objectRenderer.material.name == originalMaterial.name + " (Instance)")
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