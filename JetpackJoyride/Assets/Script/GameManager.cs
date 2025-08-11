// Game and Code By RvRproduct (Roberto Reynoso)

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    // Game Manager Tracking Variables
    private float countingSeconds;
    private int currentSecondsAchieved;
    private bool startGame = false;
    private bool restartGame = false;
    private bool obstacleStart = true;

    // Use This To Start A New Game
    InputActions inputActions;

    // For Audio
    private bool isMusicMuted = false;
    private bool isSoundEffectsMuted = false;

    [Header("UI Timers")]
    [SerializeField] private TextMeshProUGUI currentTimer;
    [SerializeField] private TextMeshProUGUI bestTimer;

    [Header("Other UI")]
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject tryagainScreen;
    [SerializeField] private GameObject startScreen;

    private bool isGameOver = false;

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
        isGameOver = false;

        inputActions = new InputActions();
        inputActions.Enable();
        startScreen.SetActive(true);
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        inputActions.Game.StartGame.started += StartGame;
    }

    private void Update()
    {
        if (restartGame)
        {
            ObstacleManager.Instance.ReturnAllObstaclesToPool();
            StartingGame();
        }

        if (!isGameOver)
        {
            GameTimer();
        }
        
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
                UpdateTimerUI();
                countingSeconds = 0.0f;
            }
        }
    }

    private void UpdateTimerUI()
    {
        int mins = currentSecondsAchieved / 60;
        int seconds = currentSecondsAchieved % 60;

        currentTimer.text = $"{mins:00}:{seconds:00}";
    }

    public void SetBestTimeOnStart()
    {
        int currentBestTime = PlayerPrefs.GetInt("TotalSecondsAchieved");
        int mins =  currentBestTime / 60;
        int seconds = currentBestTime % 60;

        bestTimer.text = $"{mins:00}:{seconds:00}";
    }

    public void SetBestTime()
    {
        if (currentSecondsAchieved > PlayerPrefs.GetInt("TotalSecondsAchieved"))
        {
            PlayerPrefs.SetInt("TotalSecondsAchieved", currentSecondsAchieved);
            SetBestTimeOnStart();
        }
    }

    public void RefreshGame()
    {
        currentSecondsAchieved = 0;
        countingSeconds = 0;
        startGame = false;
        UpdateTimerUI();
        tryagainScreen.SetActive(true);
        titleScreen.SetActive(true);
    }

    public void ReadyRestartInput()
    {
        inputActions.Game.StartGame.started += RestartGame;
    }

    private void RestartGame(InputAction.CallbackContext context)
    {
        inputActions.Game.StartGame.started -= RestartGame;
        obstacleStart = false;
        restartGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        inputActions.Game.StartGame.started -= StartGame;
        obstacleStart = false;
        ObstacleManager.Instance.ReturnAllObstaclesToPool();
        StartingGame();    
    }

    private void StartingGame()
    {
        titleScreen.SetActive(false);
        if (startScreen.activeInHierarchy)
        {
            startScreen.SetActive(false);
        }
        if (tryagainScreen.activeInHierarchy)
        {
            tryagainScreen.SetActive(false);
        }

        isGameOver = false;
        StartCoroutine(ObstacleStart());
    }

    private IEnumerator ObstacleStart()
    {
        if (!restartGame)
        {
            yield return null;
        }

        if (restartGame)
        {
            restartGame = false;
        }

        startGame = true;
        obstacleStart = true;
    }

    public bool GetObstacleStart()
    {
        return obstacleStart;
    }

    public void SetObstacleStart(bool _obstaclestart)
    {
        obstacleStart = _obstaclestart;
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

    public bool GetStartGame()
    {
        return startGame;
    }

    public void SetIsGameOver(bool _isGameOver)
    {
        isGameOver = _isGameOver;
    }


}
