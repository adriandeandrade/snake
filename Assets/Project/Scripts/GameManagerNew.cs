using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerNew : MonoBehaviour
{
    public static GameManagerNew instance;

    [HideInInspector] public enum GameStates { START, GAME, TRYAGAIN}
    [HideInInspector] public GameStates currentGameState;

    private delegate void UpdateBehavior();
    private UpdateBehavior CurrentUpdate;

    public delegate void OnRoundStart();
    public delegate void OnRoundEnd();

    public static event OnRoundStart OnStart;
    public static event OnRoundEnd OnEnd;

    [SerializeField] private GameObject retryPanel;
    private Button retryButton;

    //public bool gameOver;
    public bool isMoving;
    public bool hasShield;
    public bool shieldSpawned;
    public bool playerDead;

    private void Start()
    {
        #region Singleton
        if (instance != null)
            return;
        instance = this;
        #endregion
        ChangeGameStates(GameStates.START);
    }

    private void Update()
    {
        CurrentUpdate();
    }

    void ChangeGameStates(GameStates gameState)
    {
        switch(gameState)
        {
            case GameStates.START:
                CurrentUpdate = StartState;
                break;
            case GameStates.GAME:
                CurrentUpdate = GameState;
                break;
            case GameStates.TRYAGAIN:
                CurrentUpdate = RetryState;
                break;
        }
    }

    void StartState()
    {
        if(Input.anyKey)
        {
            StartRound();
        }
    }

    void GameState()
    {
        if(playerDead)
        {
            EndRound();
        }
    }

    void RetryState()
    {

    }

    private void StartRound()
    {
        currentGameState = GameStates.GAME;
        ChangeGameStates(currentGameState);

        UIManager.ActivatePanel("scorePanel");
        UIManager.DeactivatePanel("startRoundPanel");

        isMoving = true;
        //gameOver = false;

        if (OnStart != null)
        {
            OnStart();
        }
    }

    private void EndRound()
    {
        currentGameState = GameStates.TRYAGAIN;
        ChangeGameStates(currentGameState);

        UIManager.ActivatePanel("tryAgainPanel");
        UIManager.DeactivatePanel("scorePanel");

        retryButton = retryPanel.GetComponentInChildren<Button>();
        retryButton.onClick.AddListener(() => ResetRound());

        isMoving = false;

        if (OnEnd != null)
        {
            OnEnd();
        }
    }

    private void ResetRound()
    {
        currentGameState = GameStates.START;
        ChangeGameStates(currentGameState);

        playerDead = false;
        retryButton.onClick.RemoveAllListeners();

        UIManager.DeactivatePanel("tryAgainPanel");
        UIManager.ActivatePanel("startRoundPanel");
    }
}
