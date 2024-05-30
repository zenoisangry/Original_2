using UnityEngine;
public class GSCutScene : IGameState
{
    private int textIndex;
    public void OnStateEnter()
    {
        /*
        textIndex = 0;
        GameManager.Instance.LoadLevel();
        ShowText();
        InputManager.InputSystem.CutScene.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIManager.Instance.SetUI(UIManager.UITypes.CutScene);
        */
    }

    public void OnStateExit()
    {
        /*
        InputManager.InputSystem.CutScene.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        */
    }

    public void OnStateUpdate()
    {
        /*
        if (InputManager.InputSystem.CutScene.ForwardDialoge.triggered)
            ShowText();            
        */
    }

    /*
    private void ShowText()
    {
        if (GameManager.Instance.CurrentLevel.HasDoneText(textIndex))
        {
            SoundManager.Instance.PlayTalkSound();
            UIManager.Instance.UpdateCutSceneText(GameManager.Instance.CurrentLevel.GetNextTextLine(textIndex));
            textIndex++;
        }
        else
            GameStateManager.Instance.CurrentGameState = GameStateManager.GameStates.GamePlay;
    }
    */
}
