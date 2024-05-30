using UnityEngine;

public class GSPause : IGameState
{
    public void OnStateEnter()
    {
        UIManager.Instance.OpenUI(UIManager.UITypes.Pause);
        //InputManager.InputSystem.Pause.Enable();
    }

    public void OnStateExit()
    {
        //InputManager.InputSystem.Pause.Disable();
    }
    public void OnStateUpdate()
    {
        //if(InputManager.InputSystem.Pause.Resume.triggered)
        //    GameStateManager.Instance.CurrentGameState = GameStateManager.GameStates.GamePlay;
    }
}