using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material world1Material;
    public Material world2Material;

    private Renderer objRenderer;

    void Awake()
    {
        objRenderer = GetComponent<Renderer>();
        SetMaterialToWorld1();
    }

    public void SwitchMaterial(bool toWorld1)
    {
        objRenderer.material = toWorld1 ? world1Material : world2Material;
    }

    public void SetMaterialToWorld1()
    {
        objRenderer.material = world1Material;
    }

    public void SetMaterialToWorld2()
    {
        objRenderer.material = world2Material;
    }

}