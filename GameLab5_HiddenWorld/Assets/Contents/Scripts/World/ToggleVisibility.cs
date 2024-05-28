using UnityEngine;
public class ToggleVisibility : MonoBehaviour
{
    public bool isInWorld2;

    public void SwitchVisibility(bool toWorld1)
    {
        gameObject.SetActive(isInWorld2 ? !toWorld1 : toWorld1);
    }
}