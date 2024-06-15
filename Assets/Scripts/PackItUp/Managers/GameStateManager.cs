using System;
using System.Collections.Generic;
using PackItUp.Controllers;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Inform that the player completed the level successfully
    public event EventHandler OnLevelSuccess;

    // Inform that the player failed to complete the level
    public event EventHandler OnLevelFailed;

    // Inform of a game start
    public event EventHandler OnLevelStart;


    // Inform that the player completed the objective
    public event EventHandler OnObjectiveCompleted;

    // There will only be one game manager that handles when to start a new game loop
    private GameManager _gameManager;

    // Win condition will start as false as the player has not completed the objective of the game
    private bool _winCondition = false;

    // We need to know how many players are present in the game so we can enable/disable its movement between levels
    private List<TopDownCharacterController> _players;

    // A single inventory will be present in the game, this is managed by the game manager 
    //and we should listen to the event the inventory emits when all key items are collected
    private MockInventory _inventory;

    // The timer exists only in the game manager, we need to listen to the event emmited when the timer runs out
    private MockTimer _timer;

    // The current level, this will live in the game manager, the game state manager is agnostic of which level is the player on
    private MockLevel _currentLevel;

    // In each level, we'll have an end zone, this will be activate once the objective is completed allowing the player 
    // to end the game in a success state
    private MockEndZone _endZone;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _timer = _gameManager.GetComponent<MockTimer>();
        _inventory = _gameManager.GetComponent<MockInventory>();
        _players = _gameManager.GetPlayers();
        _currentLevel = _gameManager.GetLevel();
        _endZone = _currentLevel.GetEndZone();
    }

    private void OnEnable()
    {
        _gameManager.OnGameStart += StartGame;
        _timer.OnTimerRunOut += EndGameFailedState;
        _endZone.OnPlayerEntered += TryEndGameSuccesfully;
        _inventory.OnKeyItemsCollected += CompleteObjective;
    }

    private void OnDisable()
    {
        _gameManager.OnGameStart -= StartGame;
        _timer.OnTimerRunOut -= EndGameFailedState;
        _endZone.OnPlayerEntered -= TryEndGameSuccesfully;
        _inventory.OnKeyItemsCollected -= CompleteObjective;
    }

    private void StartGame(object sender, EventArgs e)
    {
        _winCondition = false;
        OnLevelStart?.Invoke(this, EventArgs.Empty);
    }


    private void CompleteObjective(object sender, EventArgs e)
    {
        _winCondition = true;
        OnObjectiveCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void TryEndGameSuccesfully(object sender, EventArgs e)
    {
        if (_winCondition) EndGameSuccessState();
    }

    private void EndGameSuccessState()
    {
        OnLevelSuccess?.Invoke(this, EventArgs.Empty);
    }

    private void EndGameFailedState(object sender, EventArgs e)
    {
        OnLevelFailed?.Invoke(this, EventArgs.Empty);
    }
}
