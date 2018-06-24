using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [HideInInspector] public int score;
    [HideInInspector] public int highScore;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        score = 0;
        InvokeRepeating("AddScore", 1f, 1f);
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void AddScore()
    {
        int amount = Random.Range(1, 6);
        score += amount;
    }
}
