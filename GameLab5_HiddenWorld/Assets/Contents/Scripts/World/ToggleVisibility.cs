using UnityEngine;
public class ToggleVisibility : MonoBehaviour
{
    public void Change()
    {
        // If the target object is inactive, activate it
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        // If the target object is active, deactivate it
        else
        {
            gameObject.SetActive(false);
        }
    }
}