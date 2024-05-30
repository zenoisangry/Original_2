using TMPro;
using UnityEngine;

public class UIGamePlay : UIBase
{
    public override UIManager.UITypes UIType => UIManager.UITypes.Gameplay;
    [SerializeField] private TextMeshProUGUI timerLabel;
    public override void IsActive(bool isActive)
    {
        base.IsActive(isActive);
        
    }

    private void Update()
    {
        
    }
}
