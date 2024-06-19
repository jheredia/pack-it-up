using System;

using System.Collections.Generic;
using System.Linq;
using PackItUp.Controllers;
using PackItUp.Managers;
using PackItUp.MapGenerator;
using PackItUp.MockSystems;
using PackItUp.Shop;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event EventHandler OnGameStart;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;
    public event EventHandler OnShopOpen;

    const string MAIN_MENU_SCENE = "MainMenu";

    [SerializeField] MockTimer _timer;
    [SerializeField] MockInventory _inventory;
    [SerializeField] Shop _shop;

    [SerializeField]
    private bool _debugMode = false;
    [SerializeField] string _debugScene;

    private TopDownCharacterController[] _players;
    [SerializeField]
    private List<string> _levels;

    [SerializeField]
    private int _startingLevelIndex;
    private string _currentLevel;

    // Ends level to test shop UI
    public bool _activateShop;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"There's more than one GameManager in the scene! {transform} - {Instance}");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);



        _currentLevel = _levels[_startingLevelIndex];

        _players = FindObjectsOfType<TopDownCharacterController>();
        // Create player controllers, set up timer, etc
    }


    public string GetLevel() => _currentLevel;

    public MockInventory GetInventory() => _inventory;

    public MockTimer GetTimer() => _timer;

    public Shop GetShop() => _shop;

    public TopDownCharacterController[] GetPlayers() => _players;


    public void StartGame()
    {
        if (_debugMode) SceneManager.LoadScene(_debugScene);
        else SceneManager.LoadScene(_currentLevel);
    }

    void NextLevel()
    {
        _currentLevel = _levels.First();
    }

    public void AdvanceLevelAndStart()
    {
        //_shop.enabled = false;
        NextLevel();
        StartGame();
    }

    void LoadMenu(object sender, EventArgs e)
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
        Destroy(this);
    }

    public void DrawShop(object sender, EventArgs e)
    {
        // Draw shop
        OnShopOpen?.Invoke(this, null);
        //_shop.enabled = true;
    }

    public void DrawPauseMenu(object sender, EventArgs e)
    {
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        if (_activateShop)
        {
            OnShopOpen?.Invoke(this, null);
            _activateShop = false;
        }
    }
}
