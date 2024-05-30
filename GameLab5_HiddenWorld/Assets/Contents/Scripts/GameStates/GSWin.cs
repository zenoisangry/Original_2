using UnityEngine;

public class GSWin : IGameState
{
    public void OnStateEnter()
    {
        //UIManager.Instance.SetUI(UIManager.UITypes.Win);
        GameManager.Instance.UpdateLevelIndex();
        //SoundManager.Instance.PlayWinSound();
        //InputManager.InputSystem.GameOver.Enable();
    }

    public void OnStateExit()
    {
        //InputManager.InputSystem.GameOver.Disable();
    }

    public void OnStateUpdate()
    {
    }
}
