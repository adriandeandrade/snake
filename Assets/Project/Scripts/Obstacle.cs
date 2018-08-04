using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;
    private Vector2 direction;

    [SerializeField] private GameObject hitParticle;

    private bool directionChanged = false;

    private void Start()
    {
        if (FindObjectOfType<Snake>() != null)
        {
            FindTarget();
        } else
        {
            Debug.Log("Cannot find target.");
            Invoke("FindTarget", 0.1f);
        }
        
        direction = target.position - transform.position;
    }

    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if(Points.score > 250 && !directionChanged)
        {
            Invoke("ChangeDirectionTowardsPlayer", Random.Range(0.5f, 1.5f));
            directionChanged = true;
        }
    }

    public void InitializeObject(float xSize, float ySize)
    {
        transform.localScale = new Vector2(xSize, ySize);
        speed = Random.Range(5f, 8f);
    }

    private void ChangeDirectionTowardsPlayer()
    {
        Transform targetPos = FindObjectOfType<Snake>().transform;
        Vector2 newDir = new Vector2(targetPos.position.x, targetPos.position.y);
        direction = newDir;
    }

    private void FindTarget()
    {
        target = FindObjectOfType<Snake>().transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Snake"))
        {
            GameObject ps = Instantiate(hitParticle, gameObject.transform);
            
            Destroy(ps, 1.5f);
            
        }   
    }
}
