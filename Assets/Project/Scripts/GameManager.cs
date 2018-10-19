using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    private ObjectSpawner objSpawner;

    private enum GameStates { MENU, GAME, TRYAGAIN, START, GAMEOVER }
    private GameStates currentGameState;

    public delegate void OnRoundStart();
    public delegate void OnRoundEnd();

    public static event OnRoundStart OnStart;
    public static event OnRoundEnd OnEnd;

    public bool gameOver;
    public bool isMoving;
    public bool hasShield;
    public bool shieldSpawned;
    public bool tryAgainScreen;
    public bool isStartScreen;

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;

        currentGameState = GameStates.START;
        
        objSpawner = FindObjectOfType<ObjectSpawner>();
        gameOver = true;
        tryAgainScreen = false;
        isStartScreen = true;
        isMoving = false;
        shieldSpawned = false;
    }

    private void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        switch (currentGameState)
        {
            case GameStates.MENU:
                break;
            case GameStates.START:
                if (Input.anyKey)
                {
                    StartRound();
                    currentGameState = GameStates.GAME;
                }
                break;
            case GameStates.GAME:

                break;
            case GameStates.TRYAGAIN:
                retryButton.onClick.AddListener(delegate () { ResetRound(); });
                break;
            case GameStates.GAMEOVER:
                break;
        }
    }

    public void StartRound()
    {
        UIManager.ActivatePanel("scorePanel");
        UIManager.DeactivatePanel("startRoundPanel");
        isMoving = true;
        gameOver = false;
        tryAgainScreen = false;
        isStartScreen = false;

        if (OnStart != null)
        {
            OnStart();
        }
    }

    public void EndRound()
    {
        gameOver = true;
        isMoving = false;

        if (OnEnd != null)
        {
            OnEnd();
        }

        UIManager.ActivatePanel("tryAgainPanel");
        UIManager.DeactivatePanel("scorePanel");
        currentGameState = GameStates.TRYAGAIN;
        tryAgainScreen = true;


    }

    public void ResetRound()
    {
        currentGameState = GameStates.START;
        UIManager.DeactivatePanel("tryAgainPanel");
        UIManager.ActivatePanel("startRoundPanel");
        isStartScreen = true;
    }
}
