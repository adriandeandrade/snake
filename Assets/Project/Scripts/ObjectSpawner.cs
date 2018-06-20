using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    public float maxXSize = 0.3f;
    public float maxYSize = 0.3f;

    private float screenHeight, screenWidth;

    private void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = Camera.main.orthographicSize;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector2 spawnPos = GetSpawnLocation();

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().InitializeObject(maxXSize, maxYSize);
        Destroy(obstacle, 8f);
    }

    private Vector2 GetSpawnLocation()
    {
        Vector2 dir = Random.insideUnitCircle * screenWidth;
        Vector2 pos;

        if (Mathf.Abs(dir.x) > (Mathf.Abs(dir.y)))
        {
            pos = new Vector2(Mathf.Sign(dir.x) * Camera.main.orthographicSize * Camera.main.aspect, dir.y * Camera.main.orthographicSize);
        }
        else
        {
            pos = new Vector2(dir.x * Camera.main.orthographicSize * Camera.main.aspect, Mathf.Sign(dir.y) * Camera.main.orthographicSize);
        }

        return pos;
    }
}

