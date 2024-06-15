using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockEndZone : MonoBehaviour
{
    public EventHandler OnPlayerEntered;
    private GameStateManager _gameStateManager;

    private void Awake()
    {
        _gameStateManager = GameManager.Instance.GetGameStateManager();
    }

    private void OnEnable()
    {
        _gameStateManager.OnObjectiveCompleted += ActivateVisualCue;
    }

    private void OnDisable()
    {
        _gameStateManager.OnObjectiveCompleted -= ActivateVisualCue;
    }

    private void ActivateVisualCue(object sender, EventArgs e) { }
}
