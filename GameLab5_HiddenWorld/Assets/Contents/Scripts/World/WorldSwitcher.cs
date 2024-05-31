using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldSwitcher : MonoBehaviour
{
    public float cooldownTime = 3.0f;
    private bool inWorld1 = true;
    private bool isCooldown = false;

    private List<ChangeMaterial> changeM = new List<ChangeMaterial>();
    private List<ToggleVisibility> toggle = new List<ToggleVisibility>();
    private List<Enemy> enemies = new List<Enemy>();

    void Start()
    {
        InitializeSwitchers();
        GameManager.OnLevelLoaded += InitializeSwitchers; // Subscribe to level loaded event
    }

    void OnDestroy()
    {
        GameManager.OnLevelLoaded -= InitializeSwitchers; // Unsubscribe to prevent memory leaks
    }

    public void InitializeSwitchers()
    {
        
         // Find all objects with the ChangeMaterial component
        changeM = FindObjectsOfType<ChangeMaterial>().ToList();
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());

        // Find all objects with the ToggleVisibility component
        
        toggle = FindObjectsOfType<ToggleVisibility>().ToList();
        Debug.Log($"Initialized {changeM.Count} ChangeMaterial and {toggle.Count} ToggleVisibility components.");
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

        foreach (var changeMaterial in changeM)
        {
            if (changeMaterial != null)
            {
                changeMaterial.SwitchMaterial(inWorld1);
            }
            else
            {
                Debug.LogWarning("ChangeMaterial switcher is null or has been destroyed.");
            }
        }

        foreach (var toggleVisibility in toggle)
        {
            if (toggleVisibility != null)
            {
                toggleVisibility.SwitchVisibility(inWorld1);
            }
            else
            {
                Debug.LogWarning("ToggleVisibility switcher is null or has been destroyed.");
            }
        }

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.PlayerRef = GameObject.FindWithTag("Player");
                enemy.gameObject.SetActive(true); // Ensure the enemy is activated
                Debug.Log($"Updated PlayerRef and forced FOV check for enemy: {enemy.name}. CanSeePlayer: {enemy.CanSeePlayer}");
            }
            else
            {
                Debug.LogWarning("Enemy is null or has been destroyed.");
            }
        }

        // Wait for the cooldown period
        yield return new WaitForSeconds(cooldownTime);

        isCooldown = false;
    }
}
