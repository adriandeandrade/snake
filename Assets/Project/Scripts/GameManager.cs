using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;

    //private UIManager uiManager;
    //private ObjectSpawner objSpawner;

    //public delegate void OnRoundStart();
    //public delegate void OnRoundEnd();

    //public static event OnRoundStart OnStart;
    //public static event OnRoundEnd OnEnd;

    //public bool gameOver;
    //public bool isMoving;
    //public bool hasShield;
    //public bool shieldSpawned;
    //public bool tryAgainScreen;
    //public bool isStartScreen;

    //private void Start()
    //{
    //    if (instance != null)
    //        return;
    //    instance = this;

    //    uiManager = GetComponent<UIManager>();
    //    objSpawner = FindObjectOfType<ObjectSpawner>();
    //    gameOver = true;
    //    tryAgainScreen = false;
    //    isStartScreen = true;
    //    isMoving = false;
    //    shieldSpawned = false;
    //}

    //private void Update()
    //{
    //    if (isStartScreen)
    //    {
    //        if (Input.anyKeyDown)
    //        {
    //            StartRound();
    //        }
    //    }
    //}

    //public void StartRound()
    //{
    //    uiManager.ActivatePanel("scorePanel");
    //    uiManager.DeactivatePanel("startRoundPanel");
    //    isMoving = true;
    //    gameOver = false;
    //    tryAgainScreen = false;
    //    isStartScreen = false;

    //    if(OnStart != null)
    //    {
    //        OnStart();
    //    }
    //}

    //public void EndRound()
    //{
    //    gameOver = true;
    //    isMoving = false;

    //    uiManager.ActivatePanel("tryAgainPanel");
    //    tryAgainScreen = true;
    //    uiManager.DeactivatePanel("scorePanel");

    //    if (OnEnd != null)
    //    {
    //        OnEnd();
    //    }
    //}

    //public void ResetRound()
    //{
    //    uiManager.DeactivatePanel("tryAgainPanel");
    //    uiManager.ActivatePanel("startRoundPanel");
    //    isStartScreen = true;
    //}
}
