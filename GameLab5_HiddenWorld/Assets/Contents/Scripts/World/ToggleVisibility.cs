using UnityEngine;
public class ToggleVisibility : MonoBehaviour
{
    public bool isInWorld2;

    void OnDestroy()
    {
        Debug.Log("ToggleVisibility object destroyed.");
    }

    public void SwitchVisibility(bool toWorld1)
    {
        if (this == null || gameObject == null)
        {
            Debug.LogWarning("ToggleVisibility object is null or has been destroyed.");
            return;
        }

        gameObject.SetActive(isInWorld2 ? !toWorld1 : toWorld1);
    }
}