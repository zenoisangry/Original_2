using TMPro;
using UnityEngine;

public class UICutScene : UIBase
{
    public override UIManager.UITypes UIType => UIManager.UITypes.CutScene;
    [SerializeField] private TextMeshProUGUI levelDesctiptionLabel;
    [SerializeField] private GameObject levelMap;

    public override void IsActive(bool isActive)
    {
        base.IsActive(isActive);
    }

    public void UpdateCutSceneText(string text)
    {
        levelDesctiptionLabel.text = text;
    }
}
