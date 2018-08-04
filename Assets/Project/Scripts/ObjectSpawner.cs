using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject shieldObjectPrefab;
    private GameObject trash;

    public float maxXSize = 0.3f;
    public float maxYSize = 0.3f;
    private float shieldTimer;
    private float shieldSpawnDelay;
    [SerializeField] private float startSpawnDelay = 2f;

    [Range(.5f, 2f)] [SerializeField] private float obstacleSpawnDelay;

    private float screenHeight, screenWidth;

    private void Awake()
    {
        GameManager.OnStart += StartObstacleSpawning;
        GameManager.OnStart += SpawnPlayer;
        GameManager.OnStart += CreateTrashObject;
        GameManager.OnEnd += StopObstacleSpawning;
    }

    private void Start()
    {
        trash = new GameObject();
        trash.name = "trash";
        screenHeight = Camera.main.orthographicSize;
        screenWidth = Camera.main.orthographicSize;
    }

    private void Update()
    {
        if (GameManager.instance.gameOver && !GameManager.instance.isMoving)
            return;

        if (!GameManager.instance.hasShield && !GameManager.instance.shieldSpawned)
        {
            if (shieldTimer < Time.time)
            {
                SpawnShield();
                GetShieldSpawnTimer();
            }
        }
    }

    public void CreateTrashObject()
    {
        trash = new GameObject();
        trash.name = "trash";
    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(startSpawnDelay);
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(obstacleSpawnDelay);
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
    }

    private void SpawnObject()
    {
        Vector2 spawnPos = GetSpawnLocation();

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().InitializeObject(maxXSize, maxYSize);
        obstacle.transform.parent = trash.transform;
        Destroy(obstacle, 8f);
    }

    private void SpawnShield()
    {
        GameManager.instance.shieldSpawned = true;
        float xPos = Random.Range(-5, screenWidth);
        float yPos = Random.Range(-5, screenHeight);
        Vector2 spawnPos = new Vector2(xPos, yPos);
        Instantiate(shieldObjectPrefab, spawnPos, Quaternion.identity);
    }

    private void GetShieldSpawnTimer()
    {
        shieldTimer = Time.time + Random.Range(15f, 30f);
    }

    private void StartObstacleSpawning()
    {
        StartCoroutine("SpawnObstacles");
        Debug.Log("Now spawning objects");
    }

    private void StopObstacleSpawning()
    {
        StopCoroutine("SpawnObstacles");
        Debug.Log("No longer spawning objects");
        Destroy(trash);
    }

    private Vector2 GetSpawnLocation()
    {
        Vector2 dir = Random.insideUnitCircle * screenWidth;
        Vector2 pos;

        if (Mathf.Abs(dir.x) > (Mathf.Abs(dir.y)))
            pos = new Vector2(Mathf.Sign(dir.x) * Camera.main.orthographicSize * Camera.main.aspect, dir.y * Camera.main.orthographicSize);
        else
            pos = new Vector2(dir.x * Camera.main.orthographicSize * Camera.main.aspect, Mathf.Sign(dir.y) * Camera.main.orthographicSize);

        return pos;
    }
}

