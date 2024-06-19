using PackItUp.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameHandler : MonoBehaviour
{   
    private GameStateManager _gameStateManager;
    private ParticleSystem _myParticleSystem;
    public AudioClip audioClip;
    private bool _isEnding = false;

    private void Awake()
    {
        _gameStateManager = GetComponentInParent<GameStateManager>();
        _myParticleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _gameStateManager.OnLevelSuccess += BeginWinProcess;
    }

    private void BeginWinProcess(object sender, EventArgs e)
    {
        //Add anything we want to happen immediately on success here
        if (_isEnding) return;
        _isEnding = true;
        _myParticleSystem.Play();
        GameManager.Instance.PlaySoundFX(audioClip);
        StartCoroutine(DelayOrCoroutineProcess());
    }

    private IEnumerator DelayOrCoroutineProcess()
    {
        //Add a delay or anything we want to happen over time after achieving win state here
        yield return new WaitForSeconds(3);
        EndWinProcess();
    }

    private void EndWinProcess()
    {
        GameManager.Instance.LoadCredits();
    }
}
