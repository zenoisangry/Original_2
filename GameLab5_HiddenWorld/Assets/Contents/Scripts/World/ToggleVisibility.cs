using UnityEngine;
public class ToggleVisibility : MonoBehaviour
{
    [SerializeField] private bool world2;
    private void Start()
    {
        if (world2) 
        { 
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        if (world2)
        {
            gameObject.SetActive(true);
        }
            
    }
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