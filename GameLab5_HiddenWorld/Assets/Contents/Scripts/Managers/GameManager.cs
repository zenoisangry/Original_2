using Cinemachine;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance)
                return _instance;
            _instance = GameObject.FindFirstObjectByType<GameManager>();
            if (!_instance)
                Debug.LogError("MISSING GAMEMANAGER");
            return _instance;
        }
    }
    #endregion

    public static event Action OnLevelLoaded;
    public CinemachineVirtualCamera Camera { get; private set; }
    [SerializeField] private LevelController[] levelPrefabs;
    [SerializeField] private GameObject playerPrefab;

    public WorldSwitcher switcher;
    public LevelController CurrentLevel { get; private set; }
    public Player CurrentPlayer { get; private set; }
    public bool IsLastLevel => currentLevelIndex == levelPrefabs.Length - 1;
    
    
    private int currentLevelIndex = 0;

    private void Awake()
    {
        Debug.Log("GameManager Awake");
        Camera = GameObject.FindFirstObjectByType<CinemachineVirtualCamera>();
        if (Camera != null) 
            Camera.enabled = false;
        InstantiatePlayer();
    }
    private void Start()
    {
        Debug.Log("GameManager Start");
        GameStateManager.Instance.StartState();
    }

    public void InitializeGame()
    {
        InstantiatePlayer();
        LoadLevel();
    }

    private void InstantiatePlayer()
    {
        if (CurrentPlayer == null && playerPrefab != null)
        {
            GameObject playerInstance = Instantiate(playerPrefab);
            CurrentPlayer = playerInstance.GetComponent<Player>();
            if (CurrentPlayer == null)
            {
                Debug.LogError("Player component not found on player prefab.");
            }
            else
            {
                // Set the player as the follow target for the camera
                if (Camera != null)
                {
                    Camera.Follow = CurrentPlayer.transform;
                }
            }
        }
    }

    public void LoadLevel()
    {
        Debug.Log("Loading level...");
        if (CurrentLevel)
        {
            Debug.Log("Destroying current level...");
            Destroy(CurrentLevel.gameObject);
        }
        if (levelPrefabs != null && levelPrefabs.Length > 0 && currentLevelIndex < levelPrefabs.Length)
        {
            Debug.Log("Instantiating new level prefab...");
            CurrentLevel = Instantiate(levelPrefabs[currentLevelIndex]);
            Debug.Log("Moving player to spawn point...");
            CurrentLevel.MovePlayer();
            Debug.Log("Level loaded successfully.");
            OnLevelLoaded?.Invoke();  // Notify that a new level has been loaded

        }
        else
        {
            Debug.LogError("Level prefabs not set or currentLevelIndex out of range.");
        }
    }

    public void LoadNextLevel()
    {
        if (IsLastLevel)
        {
            Debug.Log("Last level reached. Implement win state here.");
            // Handle game win state or loop back to the first level, etc.
        }
        else
        {
            currentLevelIndex++;
            LoadLevel();
            if (switcher != null)
            {
                switcher.InitializeSwitchers();
            }
            else
            {
                Debug.LogWarning("Switcher is not assigned in GameManager.");
            }
        }
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawning player...");
        if (CurrentPlayer != null && CurrentLevel != null)
        {
            CurrentPlayer.RespawnPlayer(CurrentLevel.spawnPoint.position);
            GameStateManager.Instance.CurrentGameState = GameStateManager.GameStates.GamePlay;
        }
        else
        {
            Debug.LogError("CurrentPlayer or CurrentLevel is null.");
        }
    }

    public void UpdateLevelIndex()
    {
        currentLevelIndex++;
    }

    public void ResetLevelIndex()
    {
        currentLevelIndex = 0;
    }
}

