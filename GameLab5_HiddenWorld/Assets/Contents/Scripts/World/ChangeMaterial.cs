using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material world1Material;
    public Material world2Material;

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnDestroy()
    {
        Debug.Log("ChangeMaterial object destroyed.");
    }

    public void SwitchMaterial(bool toWorld1)
    {
        if (meshRenderer == null)
        {
            Debug.LogWarning("MeshRenderer is null or has been destroyed.");
            return;
        }

        meshRenderer.material = toWorld1 ? world1Material : world2Material;
    }

    public void SetMaterialToWorld1()
    {
        meshRenderer.material = world1Material;
    }

    public void SetMaterialToWorld2()
    {
        meshRenderer.material = world2Material;
    }

}