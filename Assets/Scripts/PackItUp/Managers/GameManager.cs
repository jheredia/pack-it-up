using System;

using System.Collections.Generic;
using System.Linq;
using PackItUp.Controllers;
using PackItUp.Managers;
using PackItUp.MapGenerator;
using PackItUp.MockSystems;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event EventHandler OnGameStart;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;

    const string MAIN_MENU_SCENE = "MainMenu";

    [SerializeField] GameStateManager gameStateManagerPrefab;
    [SerializeField] MockTimer _timer;
    [SerializeField] MockInventory _inventory;

    private TopDownCharacterController[] _players;
    private List<Level> _levels = new();
    [SerializeField]
    private Level _currentLevel;
    private GameStateManager _gameStateManager;


    // Luciano
    [SerializeField]
    private int _levelIndex;
    [SerializeField]
    private List<AbstractMapGenerator> _levelGenerators;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"There's more than one GameManager in the scene! {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Maybe? DontDestroyOnLoad(gameObject);


        _gameStateManager = Instantiate(gameStateManagerPrefab, transform);

        _currentLevel.Generator = _levelGenerators[_levelIndex];

        _players = FindObjectsOfType<TopDownCharacterController>();
        // Create player controllers, set up timer, etc
    }

    private void OnEnable()
    {
        _gameStateManager.OnLevelFailed += DrawPauseMenu;
        _gameStateManager.OnLevelSuccess += DrawShop;
    }

    private void OnDisable()
    {
        _gameStateManager.OnLevelFailed -= DrawPauseMenu;
        _gameStateManager.OnLevelSuccess -= DrawShop;
    }

    private void Start()
    {
        StartGame();
    }

    public Level GetLevel() => _currentLevel;

    public MockInventory GetInventory() => _inventory;

    public MockTimer GetTimer() => _timer;

    public TopDownCharacterController[] GetPlayers() => _players;

    public GameStateManager GetGameStateManager() => _gameStateManager;

    void StartGame()
    {
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    void NextLevel()
    {
        _currentLevel = _levels.First();
    }

    void AdvanceLevelAndStart()
    {
        NextLevel();
        StartGame();
    }

    void LoadMenu(object sender, EventArgs e)
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
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
