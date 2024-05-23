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
        if (InputManager.Playercontrols.Player.ChangeWorld.triggered && Time.time > lastUsedTime + cooldownTime)
        {
            // Update the last used time to the current time
            lastUsedTime = Time.time;

            // Trigger the cooldown event
            InputManager.TriggerCooldown();
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

