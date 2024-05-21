using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Cooldown duration in seconds
    public float cooldownTime = 3f;

    // Track the last time the action was performed
    private float lastUsedTime;

    void Update()
    {
        // Check if the 'C' key is pressed and enough time has passed since the last use
        if (Input.GetKeyDown(KeyCode.C) && Time.time > lastUsedTime + cooldownTime)
        {
            // Update the last used time to the current time
            lastUsedTime = Time.time;

            // Notify all listeners that the cooldown has been triggered
            NotifyListeners();
        }
    }

    // Method to notify all listeners that the cooldown has been triggered
    void NotifyListeners()
    {
        ToggleVisibility[] visibilityTogglers = Resources.FindObjectsOfTypeAll<ToggleVisibility>();
        foreach (ToggleVisibility toggler in visibilityTogglers)
        {
            toggler.Change();
        }

        ChangeMaterial[] materialChangers = FindObjectsOfType<ChangeMaterial>();
        foreach (ChangeMaterial changer in materialChangers)
        {
            changer.WallChange();
        }
    }

    // Method to get the last used time
    public float GetLastUsedTime()
    {
        return lastUsedTime;
    }

    // Method to get the cooldown time
    public float GetCooldownTime()
    {
        return cooldownTime;
    }
}

