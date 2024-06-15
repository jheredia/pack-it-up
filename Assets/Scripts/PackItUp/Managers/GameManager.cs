using System;
using System.Collections.Generic;
using System.Linq;
using PackItUp.Controllers;
using PackItUp.Managers;
using PackItUp.MockSystems;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event EventHandler OnGameStart;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;

    [SerializeField] GameStateManager gameStateManagerPrefab;
    [SerializeField] MockTimer _timer;
    [SerializeField] MockInventory _inventory;

    private List<TopDownCharacterController> _players;
    private List<MockLevel> _levels = new();
    private MockLevel _currentLevel, _lastLevel;
    private GameStateManager _gameStateManager;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Debug.LogError($"There's more than one GameManager in the scene! {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        // DontDestroyOnLoad(gameObject);
        _currentLevel = FindObjectOfType<MockLevel>();
        
        _gameStateManager = Instantiate(gameStateManagerPrefab, transform);
        
        // _levels = LevelManager.Instance.GenerateLevels();
        //_currentLevel = _levels.First();
        //TODO If levels are really going to be MonoBehaviours, then they should be "Instantiated" ... The alternative is treat them as "generated data" and not GameObjects
        

        // Create player controllers, set up timer, etc
    }

    private void OnEnable()
    {
        _gameStateManager.OnLevelFailed += DrawMenu;
        _gameStateManager.OnLevelSuccess += DrawShop;
    }

    private void OnDisable()
    {
        _gameStateManager.OnLevelFailed -= DrawMenu;
        _gameStateManager.OnLevelSuccess -= DrawShop;
    }

    private void Start()
    {
        StartGame();
    }

    public MockLevel GetLevel() => _currentLevel;

    public MockInventory GetInventory() => _inventory;

    public MockTimer GetTimer() => _timer;

    public List<TopDownCharacterController> GetPlayers() => _players;

    public GameStateManager GetGameStateManager() => _gameStateManager;

    void StartGame()
    {
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    void NextLevel()
    {
        _lastLevel = _currentLevel;
        _currentLevel = _levels.First();
    }

    void AdvanceLevelAndStart()
    {
        NextLevel();
        StartGame();
    }


    void ExitGame()
    {
        // Close game
    }

    void DrawMenu(object sender, EventArgs e)
    {
        // Draw menu
    }

    void DrawShop(object sender, EventArgs e)
    {
        // Draw shop
    }

    void DrawPauseMenu(object sender, EventArgs e)
    {
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }
}
