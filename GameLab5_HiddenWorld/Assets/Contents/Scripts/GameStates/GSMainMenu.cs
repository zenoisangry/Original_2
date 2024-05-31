using UnityEngine;

public class GSMainMenu : IGameState
{
    public void OnStateEnter()
    {
        UIManager.Instance.SetUI(UIManager.UITypes.MainMenu);
        GameManager.Instance.ResetLevelIndex();
    }

    public void OnStateExit()
    {
    }

    public void OnStateUpdate()
    {
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
