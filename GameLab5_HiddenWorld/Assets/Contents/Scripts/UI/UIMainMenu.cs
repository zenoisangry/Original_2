using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    public override UIManager.UITypes UIType => UIManager.UITypes.MainMenu;

    public Button newGameButton;
    public Button quitButton;

    public void Init()
    {
        newGameButton.onClick.AddListener(() => { GameStateManager.Instance.CurrentGameState = GameStateManager.GameStates.GamePlay; });
        quitButton.onClick.AddListener(() => { GSMainMenu.Quit(); });
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
