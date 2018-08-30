using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private int speed;

    private enum Direction { UP, DOWN, LEFT, RIGHT }
    private Direction direction = Direction.UP;
    private float screenHeight, screenWidth;

    [SerializeField] private GameObject hitParticle;

    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer trail;

    private void Awake()
    {
        screenWidth = Camera.main.orthographicSize;
        screenHeight = Camera.main.orthographicSize;
        GameManager.instance.hasShield = false;
        animator = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        Debug.Log("Screen Height: " + screenHeight.ToString());
        Debug.Log("Screen Width: " + screenWidth.ToString());
    }

    private void Update()
    {
        if (GameManager.instance.gameOver || !GameManager.instance.isMoving)
            return;

        if(GameManager.instance.hasShield)
        {
            animator.SetBool("hasShield", true);
        } else
        {
            animator.SetBool("hasShield", false);
        }

        Movement();
        CameraBounds();
    }

    private void Movement()
    {
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
        if (transform.position.y > screenHeight - 1.0f)
        {
            // Transforms the player to the bottom of the screen when it crosses the y bound on the top.
            transform.position = new Vector2(transform.position.x, -screenHeight);
        }
        else if (transform.position.y < -screenHeight)
        {
            // Transforms the player to the top of the screen when it crosses the y bound on the bottom.
            transform.position = new Vector2(transform.position.x, screenHeight - 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (GameManager.instance.hasShield)
            {
                GameManager.instance.hasShield = false;
                GameObject hitPs = Instantiate(this.hitParticle, gameObject.transform);
                Destroy(hitPs, 1.0f);
                Camera.main.gameObject.GetComponent<CameraShake>().ShakeCamera();
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
            }
            Destroy(other.gameObject);
        }
    }
}
