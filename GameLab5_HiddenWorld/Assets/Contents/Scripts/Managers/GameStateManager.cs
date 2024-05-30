using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStateManager;

public class GameStateManager : MonoBehaviour
{
    #region SINGLETON
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance)
                return _instance;
            _instance = GameObject.FindFirstObjectByType<GameStateManager>();
            if (!_instance)
                Debug.LogError("MISSING GAMESTATEMANAGER");
            return _instance;
        }
    }
    #endregion
    public enum GameStates
    {
        MainMenu,
        GamePlay,
        Win,
        GameOver,
        Cutscene,
        Pause
    }
    public GameStates startingGameState;
    private Dictionary<GameStates, IGameState> registeredState = new();
    [SerializeField] private GameStates _currentGameState;
    public GameStates CurrentGameState
    {
        get => _currentGameState;
        set
        {
            registeredState[_currentGameState]?.OnStateExit();
            _currentGameState = value;
            registeredState[_currentGameState]?.OnStateEnter();
        }
    }
    private void RegisterStates()
    {
        registeredState.Add(GameStates.Cutscene, new GSCutScene());
        registeredState.Add(GameStates.Win, new GSWin());
        registeredState.Add(GameStates.GameOver, new GSGameOver());
        registeredState.Add(GameStates.GamePlay, new GSGameplay());
        registeredState.Add(GameStates.MainMenu, new GSMainMenu());
        registeredState.Add(GameStates.Pause, new GSPause());
    }

    private void Awake()
    {
        RegisterStates();
    }
    private void Update()
    {
        registeredState[CurrentGameState]?.OnStateUpdate();
    }
    public void StartState()
    {
        CurrentGameState = startingGameState;
    }
}
