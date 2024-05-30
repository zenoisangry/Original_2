using UnityEngine;

public class GSGameOver : IGameState
{
    public void OnStateEnter()
    {
        UIManager.Instance.SetUI(UIManager.UITypes.GameOver);
        //SoundManager.Instance.PlayDeathSound();
        //InputManager.InputSystem.GameOver.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Respawn the player after a brief delay
        GameManager.Instance.Invoke("RespawnPlayer", 2.0f); // Adjust the delay as needed
    }

    public void OnStateExit()
    {
        //InputManager.InputSystem.GameOver.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStateUpdate()
    {
        //if(InputManager.InputSystem.GameOver.LoadLevel.triggered)
        //    GameStateManager.Instance.CurrentGameState = GameStateManager.GameStates.Cutscene;
    }

}
