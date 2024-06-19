using System;
using System.Collections;
using System.Collections.Generic;
using PackItUp.Controllers;
using PackItUp.Interactables;
using PackItUp.MapGenerator;
using PackItUp.MockSystems;
using UnityEngine;

namespace PackItUp.Managers
{
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

        public event EventHandler OnEndGameCountdownCancel;

        // There will only be one game manager that handles when to start a new game loop
        private GameManager _gameManager;

        // Win condition will start as false as the player has not completed the objective of the game

        [SerializeField] // For testing purposes
        private bool _winCondition = false;
        private Behaviour_MoveObjectsToPoints _pickupMover;

        // Exit condition will start as false as neither player is on an end zone yet
        [SerializeField] // For testing purposes
        private bool _exitCondition = false;


        // We need to know how many players are present in the game so we can enable/disable its movement between levels
        private TopDownCharacterController[] _players;

        // A single inventory will be present in the game, this is managed by the game manager 
        //and we should listen to the event the inventory emits when all key items are collected
        private MockInventory _inventory;

        // The timer exists only in the game manager, we need to listen to the event emmited when the timer runs out
        private Timer _timer;

        // The current level, this will live in the game manager, the game state manager is agnostic of which level is the player on
        private Level _currentLevel;

        // In each level, we'll have an end zone, this will be activate once the objective is completed allowing the player 
        // to end the game in a success state
        private EndZone[] _endZones;
        private Component_ObjectInstantiator _pickupInstantiator;

        private UIControl _timerUIControl;
        private Component_KeyItemTracker _keyItemTracker;
        [SerializeField]
        private float _initTimer;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _players = _gameManager.GetPlayers();
            _endZones = FindObjectsOfType<EndZone>();
            _pickupInstantiator = FindObjectOfType<Component_ObjectInstantiator>();
            _pickupInstantiator.BeginInstantiation();
            _pickupMover = FindObjectOfType<Behaviour_MoveObjectsToPoints>();
            _pickupMover.StartMovingObjects();
            _exitCondition = false;
            _winCondition = false;
            _timerUIControl = FindObjectOfType<UIControl>();
            _keyItemTracker = FindObjectOfType<Component_KeyItemTracker>();
        }

        private void OnEnable()
        {
            EndZone.OnPlayerEnteredZone += TryEndGameSuccessfully;
            // EndZone.OnPlayerExitZone += CancelEndGameCountdown;
            EndZone.OnEndZoneEmpty += DeactivateExitCondition;
            _timerUIControl.timerFinished.AddListener(EndGameFailedState);
            _keyItemTracker.onAllKeyItemsPickedUp.AddListener(CompleteObjective);
        }

        private void OnDisable()
        {
            EndZone.OnPlayerEnteredZone -= TryEndGameSuccessfully;
            // EndZone.OnPlayerExitZone -= CancelEndGameCountdown;
            EndZone.OnEndZoneEmpty -= DeactivateExitCondition;
            _timerUIControl.timerFinished.RemoveListener(EndGameFailedState);
            _keyItemTracker.onAllKeyItemsPickedUp.AddListener(CompleteObjective);
        }

        private void Start()
        {
            SetActivePlayers(false);
            StartCoroutine(WaitForTimer());
            StartGame();
        }

        private IEnumerator WaitForTimer()
        {
            yield return new WaitForSeconds(_initTimer);
            yield return new WaitForSeconds(_initTimer);
            yield return new WaitForSeconds(_initTimer);
        }

        private void StartGame()
        {
            SetActivePlayers(true);
            _winCondition = false;
            _timerUIControl.StartTimer();
            OnLevelStart?.Invoke(this, EventArgs.Empty);
        }

        private void SetActivePlayers(bool active)
        {
            foreach (TopDownCharacterController _player in _players)
            {
                _player.SetActive(active);
            }
        }

        private void CompleteObjective()
        {
            Debug.Log("Objective completed");
            _winCondition = true;
            OnObjectiveCompleted?.Invoke(this, EventArgs.Empty);
            // Another player is still in an end zone
            if (_exitCondition) EndGameSuccessState();
        }

        private void TryEndGameSuccessfully(object sender, GameObject playerObject)
        {
            Debug.Log("Try end game");
            //     var objectName = playerObject.transform.parent ? playerObject.transform.parent.name : playerObject.name;
            //     //NOTE... right now this is only called by the end zone, that's why Im casting the sender to EndZone
            //     Debug.Log($"Player {objectName} entered Zone {((EndZone)sender).name}");
            //     //JUST TO TEST THE VISUAL CUE
            //     EndZone.ActivateCue(true);
            _exitCondition = true;
            if (_winCondition) EndGameSuccessState();
        }

        //JUST ADDED THIS TO TEST THE VISUAL CUE - AND COULD BE USEFUL IF THE PLAYER NOTICE HES MISSING SOMETHING AND GOES TO FETCH IT
        private void CancelEndGameCountdown(object sender, GameObject playerObject)
        {
            var objectName = playerObject.transform.parent ? playerObject.transform.parent.name : playerObject.name;
            //NOTE... right now this is only called by the end zone, that's why Im casting the sender to EndZone
            Debug.Log($"Player {objectName} left Zone {((EndZone)sender).name}");
            //JUST TO TEST THE VISUAL CUE
            // EndZone.ActivateCue(false);
            OnEndGameCountdownCancel?.Invoke(this, EventArgs.Empty);
        }

        private void DeactivateExitCondition(object sender, EventArgs e)
        {
            Debug.Log("Deactivate exit condition");
            _exitCondition = false;
        }

        private void EndGameSuccessState()
        {
            Debug.Log("End level success");
            _timerUIControl.TogglePause();
            OnLevelSuccess?.Invoke(this, EventArgs.Empty);
        }

        private void EndGameFailedState()
        {
            Debug.Log("End level fail");
            SetActivePlayers(false);
            OnLevelFailed?.Invoke(this, EventArgs.Empty);
        }

    }
}
