using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;
    private Vector2 direction;

    private void Start()
    {
        if(FindObjectOfType<Snake>() != null)
        {
            target = FindObjectOfType<Snake>().transform;
        }

        direction = target.position - transform.position;
    }

    private void Update()
    {
        
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    public void InitializeObject(float xSize, float ySize)
    {
        transform.localScale = new Vector2(xSize, ySize);
        speed = Random.Range(5f, 8f);
        //RotateObject();
    }
}
