using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private int speed;

    private enum Direction { UP, DOWN, LEFT, RIGHT }
    private Direction direction = Direction.UP;
    private float screenHeight, screenWidth;

    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject hitParticle;

    private void Awake()
    {
        screenWidth = Camera.main.orthographicSize;
        screenHeight = Camera.main.orthographicSize;
        GameManager.instance.hasShield = false;
    }

    private void Update()
    {
        if (GameManager.instance.gameOver || !GameManager.instance.isMoving)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Direction.UP;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Direction.DOWN;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Direction.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Direction.RIGHT;
        }

        Movement();
        CameraBounds();
    }

    private void Movement()
    {
        switch (direction)
        {
            case Direction.UP:
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                break;
            case Direction.DOWN:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;
            case Direction.LEFT:
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case Direction.RIGHT:
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                break;
        }
    }

    private void CameraBounds()
    {
        // X axis bounds
        if (transform.position.x > screenWidth)
        {
            transform.position = new Vector2(-screenWidth, transform.position.y);
        }
        else if (transform.position.x < -screenWidth)
        {
            transform.position = new Vector2(screenWidth, transform.position.y);
        }

        // Y axis bounds
        if (transform.position.y > screenHeight)
        {
            transform.position = new Vector2(transform.position.x, -screenHeight);
        }
        else if (transform.position.y < -screenHeight)
        {
            transform.position = new Vector2(transform.position.x, screenHeight);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (GameManager.instance.hasShield)
            {
                GameManager.instance.hasShield = false;
                shield.SetActive(false);
                GameObject ps = Instantiate(hitParticle, gameObject.transform);
                //Destroy(other.gameObject);
                Destroy(ps, 1.0f);
                Camera.main.gameObject.GetComponent<CameraShake>().ShakeCamera(/*0.5f, 0.3f*/);
            }
            else
            {
                GameManager.instance.EndRound();
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Shield"))
        {
            if (!GameManager.instance.hasShield)
            {
                GameManager.instance.hasShield = true;
                GameManager.instance.shieldSpawned = false;
                Destroy(other.gameObject);
                InitShield();
            }
            Destroy(other.gameObject);
        }
    }

    private void InitShield()
    {
        shield.SetActive(true);
    }
}
