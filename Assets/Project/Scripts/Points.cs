using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [HideInInspector] public static int score;
    [HideInInspector] public int highScore;
    private int tempScore;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text panelScore;
    [SerializeField] private Text highScoreText;

    private void Awake()
    {
        GameManagerNew.OnStart += ResetScore;
        GameManagerNew.OnEnd += UpdateScore;
    }

    private void Start()
    {
        score = 0;
        InvokeRepeating("AddScore", 1f, 1f);
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        tempScore = score;
    }

    private void AddScore()
    {
        if(GameManagerNew.instance.currentGameState == GameManagerNew.GameStates.GAME)
        {
            int amount = Random.Range(1, 6);
            score += amount;
        }
    }

    private void ResetScore()
    {
        tempScore = 0;
        score = 0;
    }

    private void UpdateScore()
    {
        if(tempScore > highScore)
        {
            highScore = tempScore;
            highScoreText.text = "Highscore: " + tempScore.ToString();
        }

        panelScore.text = "Score: " + tempScore.ToString();

        Debug.Log("SCORE UPDATED");
        Debug.Log(tempScore);
    }
}
