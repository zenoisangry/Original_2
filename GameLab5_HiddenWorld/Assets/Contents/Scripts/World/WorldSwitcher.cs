using System.Collections;
using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public float cooldownTime = 3.0f;
    private bool inWorld1 = true;
    private bool isCooldown = false;

    private ChangeMaterial[] changeM;
    private ToggleVisibility[] toggle;

    void Start()
    {
        // Find all objects with the MaterialSwitcher component
        changeM = FindObjectsOfType<ChangeMaterial>();

        // Find all objects with the VisibilitySwitcher component
        toggle = FindObjectsOfType<ToggleVisibility>();
    }

    void Update()
    {
        if (InputManager.Playercontrols.Player.ChangeWorld.triggered && !isCooldown)
        {
            StartCoroutine(SwitchWorlds());
        }
    }

    IEnumerator SwitchWorlds()
    {
        isCooldown = true;

        inWorld1 = !inWorld1;

        foreach (ChangeMaterial switcher in changeM)
        {
            switcher.SwitchMaterial(inWorld1);
        }

        foreach (ToggleVisibility switcher in toggle)
        {
            switcher.SwitchVisibility(inWorld1);
        }

        // Wait for the cooldown period
        yield return new WaitForSeconds(cooldownTime);

        isCooldown = false;
    }
}
