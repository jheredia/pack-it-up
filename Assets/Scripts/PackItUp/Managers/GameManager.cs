using System;

using System.Collections.Generic;
using System.Linq;
using PackItUp.Controllers;
using PackItUp.Managers;
using PackItUp.MockSystems;
using PackItUp.Shop;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;

    const string MAIN_MENU_SCENE = "MainMenu";
    const string CREDITS_SCENE = "Credits";
    const string SHOP_SCENE = "Shop";

    [SerializeField] Timer _timer;
    [SerializeField] MockInventory _inventory;

    [SerializeField]
    private bool _debugMode = false;
    [SerializeField] string _debugScene;

    private TopDownCharacterController[] _players;
    [SerializeField]
    private List<string> _levels;

    [SerializeField]
    private int _startingLevelIndex;
    private int _currentLevelIndex;
    private string _currentLevel;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);



        _currentLevel = _levels[_startingLevelIndex];

        _players = FindObjectsOfType<TopDownCharacterController>();
        _audioSource = GetComponent<AudioSource>();
        // Create player controllers, set up timer, etc
    }


    public string GetLevel() => _currentLevel;

    public MockInventory GetInventory() => _inventory;

    public Timer GetTimer() => _timer;

    public TopDownCharacterController[] GetPlayers() => _players;


    public void StartGame()
    {
        if (_debugMode) SceneManager.LoadScene(_debugScene);
        else SceneManager.LoadScene(_currentLevel);
    }

    void NextLevel()
    {
        _currentLevelIndex++;
        _currentLevel = _levels[_currentLevelIndex];
    }

    public void AdvanceLevelAndStart()
    {
        NextLevel();
        StartGame();
    }

    public void LoadMenu(object sender, EventArgs e)
    {
        StopMainTheme();
        SceneManager.LoadScene(MAIN_MENU_SCENE);
        Destroy(gameObject);
    }

    public void LoadCredits(AudioClip clip = null)
    {
        StopMainTheme();
        SceneManager.LoadScene(CREDITS_SCENE);
        if (clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(SHOP_SCENE);
    }

    public void DrawPauseMenu(object sender, EventArgs e)
    {
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }

    public void StopMainTheme()
    {
        _audioSource.Stop();
    }

    public void PlaySoundFX(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
