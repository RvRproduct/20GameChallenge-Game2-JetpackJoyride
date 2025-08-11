using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    // Game Manager Tracking Variables
    private float countingSeconds;
    private int currentSecondsAchieved;
    private bool startGame = false;

    // Use This To Start A New Game
    InputActions inputActions;

    // For Audio
    private bool isMusicMuted = false;
    private bool isSoundEffectsMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("TotalSecondsAchieved"))
        {
            PlayerPrefs.SetInt("TotalSecondsAchieved", 0);
        }
        countingSeconds = 0;
        currentSecondsAchieved = 0;

        inputActions = new InputActions();
        inputActions.Enable();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        inputActions.Game.StartGame.started += StartGame;
    }

    private void OnDisable()
    {
        inputActions.Game.StartGame.started -= StartGame;
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        startGame = true;
        inputActions.Game.StartGame.started -= StartGame;
    }

    private void Update()
    {
        GameTimer();
    }

    private void GameTimer()
    {
        if (startGame)
        {
            countingSeconds += Time.deltaTime;

            // One Second Intervals
            if (countingSeconds >= 1.0f)
            {
                currentSecondsAchieved++;
                countingSeconds = 0.0f;
            }
        }
    }

    public void SetBestTime()
    {
        if (currentSecondsAchieved > PlayerPrefs.GetInt("TotalSecondsAchieved"))
        {
            PlayerPrefs.SetInt("TotalSecondsAchieved", currentSecondsAchieved);
        }
    }

    public void RefreshGame()
    {
        currentSecondsAchieved = 0;
        countingSeconds = 0;
        startGame = false;
    }

    public void ReadyStartInput()
    {
        inputActions.Game.StartGame.started += StartGame;
    }

    public bool GetIsMusicMuted()
    {
        return isMusicMuted;
    }

    public bool GetIsSoundEffectsMuted()
    {
        return isSoundEffectsMuted;
    }

    public void SetIsMusicMuted(bool _muteMusic)
    {
        isMusicMuted = _muteMusic;
    }

    public void SetIsSoundEffectsMuted(bool _muteSoundEffects)
    {
        isSoundEffectsMuted = _muteSoundEffects;
    }


}
