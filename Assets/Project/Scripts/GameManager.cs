using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UIManager uiManager;
    private ObjectSpawner objSpawner;
    [SerializeField] private GameObject playerPrefab;

    public delegate void OnRoundStart();
    public static event OnRoundStart OnStart;
    public delegate void OnRoundEnd();
    public static event OnRoundEnd OnEnd;

    public int lives;
    public bool gameOver;
    public bool isMoving;
    public bool hasShield;
    public bool shieldSpawned;

    private void Start()
    {
        if (instance != null)
            return;
        instance = this;

        uiManager = GetComponent<UIManager>();
        objSpawner = FindObjectOfType<ObjectSpawner>();
        lives = 5;
        gameOver = false;
        isMoving = false;
        shieldSpawned = false;
    }

    private void Update()
    {
        if (!isMoving && !gameOver)
        {
            if (Input.anyKeyDown)
            {
                StartRound();
            }
        }
    }

    public void StartRound()
    {
        uiManager.ActivatePanel("scorePanel");
        uiManager.DeactivatePanel("startRoundPanel");
        isMoving = true;
        Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);

        if(OnStart != null)
        {
            OnStart();
        }
    }

    public void EndRound()
    {
        gameOver = true;
        isMoving = false;
        objSpawner.isSpawning = false;

        if (OnEnd != null)
        {
            OnEnd();
        }

        // Clear all spawned obstacles
        foreach (GameObject obj in objSpawner.spawnedObjects)
        {
            Destroy(obj);
        }

        objSpawner.spawnedObjects.Clear();

        if (lives <= 0)
        {
            uiManager.ActivatePanel("gameOverPanel");
            uiManager.DeactivatePanel("scorePanel");
        }
        else
        {
            lives--;
            uiManager.ActivatePanel("tryAgainPanel");
            uiManager.DeactivatePanel("scorePanel");
        }
    }

    public void ResetRound()
    {
        gameOver = false;
        uiManager.DeactivatePanel("gameOverPanel");
        uiManager.DeactivatePanel("tryAgainPanel");
        uiManager.ActivatePanel("startRoundPanel");
    }
}
