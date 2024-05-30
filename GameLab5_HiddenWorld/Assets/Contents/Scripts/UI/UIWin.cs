using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UIBase
{
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject mainMenuButton;
    public override UIManager.UITypes UIType => UIManager.UITypes.Win;

    public override void IsActive(bool isActive)
    {
        base.IsActive(isActive);
        nextLevelButton.SetActive(!GameManager.Instance.IsLastLevel);
        mainMenuButton.SetActive(GameManager.Instance.IsLastLevel);

    }
}
