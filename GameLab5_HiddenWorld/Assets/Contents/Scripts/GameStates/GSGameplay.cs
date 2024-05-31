using UnityEngine;

public class GSGameplay : IGameState
{
    public void OnStateEnter()
    {
        GameManager.Instance.Camera.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //UIManager.Instance.SetUI(UIManager.UITypes.Gameplay);
        InputManager.Playercontrols.Player.Enable();
        GameManager.Instance.CurrentPlayer.Unpaused();
        if (GameManager.Instance.CurrentLevel == null)
        {
            GameManager.Instance.InitializeGame();
            //GameManager.Instance.LoadLevel();
        }
        Debug.Log("Entered GamePlay State and loaded the level.");
    }

    public void OnStateExit()
    {
        Debug.Log("Exiting GamePlay State");
        GameManager.Instance.CurrentPlayer.Pause();
        InputManager.Playercontrols.Player.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Instance.Camera.enabled = false;
    }

    public void OnStateUpdate()
    {
        
        //if (InputManager.InputSystem.GamePlay.Pause.triggered)
          //  GameStateManager.Instance.CurrentGameState = GameStateManager.GameStates.Pause;
        //if (InputManager.InputSystem.GameOver.LoadLevel.triggered)
        //{
          //  InputManager.InputSystem.GameOver.Disable();
            //GameStateManager.Instance.CurrentGameState = GameStateManager.GameStates.GamePlay;
        //}
        
    }
}
