using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    public float maxXSize = 0.3f;
    public float maxYSize = 0.3f;
    private float timer;
    private float spawnDelay = 2f;
    public bool isSpawning;

    private float screenHeight, screenWidth;
    public List<GameObject> spawnedObjects = new List<GameObject>();

    private void Awake()
    {
        isSpawning = false;
        GetTimer();
        GameManager.OnStart += StartSpawning;
    }

    private void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = Camera.main.orthographicSize;
    }

    private void Update()
    {
        if (GameManager.instance.gameOver && !GameManager.instance.isMoving)
            return;

        if (timer < Time.time && isSpawning)
        {
            SpawnObject();
            GetTimer();
        }
    }

    private void SpawnObject()
    {
        Vector2 spawnPos = GetSpawnLocation();

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().InitializeObject(maxXSize, maxYSize);
        spawnedObjects.Add(obstacle);
        Destroy(obstacle, 8f);
    }

    private void GetTimer()
    {
        timer = Time.time + Random.Range(.5f, 2f);
    }

    private void StartSpawning()
    {
        isSpawning = true;
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

