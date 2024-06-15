using System;
using System.Collections.Generic;
using PackItUp.Controllers;
using PackItUp.Interactables;
using PackItUp.MockSystems;
using UnityEngine;

namespace PackItUp.Managers {
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
        private EndZone[] _endZones;

        private void Awake() 
        {
            _gameManager = GameManager.Instance;
            _timer = _gameManager.GetTimer();
            _inventory = _gameManager.GetInventory();
            _players = _gameManager.GetPlayers();
            _currentLevel = _gameManager.GetLevel();
            _endZones = _currentLevel.GetEndZones();
        }

        private void OnEnable()
        {
            _gameManager.OnGameStart += StartGame;
            _timer.OnTimerRunOut += EndGameFailedState;
            EndZone.OnPlayerEnteredZone += TryEndGameSuccessfully;
            EndZone.OnPlayerExitZone += CancelEndGameCountdown;
            _inventory.OnKeyItemsCollected += CompleteObjective;
        }

        private void OnDisable()
        {
            _gameManager.OnGameStart -= StartGame;
            _timer.OnTimerRunOut -= EndGameFailedState;
            EndZone.OnPlayerEnteredZone -= TryEndGameSuccessfully;
            EndZone.OnPlayerExitZone += TryEndGameSuccessfully;
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

        private void TryEndGameSuccessfully(object sender, GameObject playerObject)
        {
            var objectName = playerObject.transform.parent ? playerObject.transform.parent.name : playerObject.name;
            //NOTE... right now this is only called by the end zone, that's why Im casting the sender to EndZone
            Debug.Log($"Player {objectName} entered Zone {((EndZone)sender).name}");
            //JUST TO TEST THE VISUAL CUE
            EndZone.ActivateCue(true);
            if (_winCondition) EndGameSuccessState();
        }
        
        //JUST ADDED THIS TO TEST THE VISUAL CUE - AND COULD BE USEFUL IF THE PLAYER NOTICE HES MISSING SOMETHING AND GOES TO FETCH IT
        private void CancelEndGameCountdown(object sender, GameObject playerObject) {
            var objectName = playerObject.transform.parent ? playerObject.transform.parent.name : playerObject.name;
            //NOTE... right now this is only called by the end zone, that's why Im casting the sender to EndZone
            Debug.Log($"Player {objectName} left Zone {((EndZone)sender).name}");
            //JUST TO TEST THE VISUAL CUE
            EndZone.ActivateCue(false);
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
