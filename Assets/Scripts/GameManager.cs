using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private int score;
    [SerializeField] private EventChannel scoreEventChannel;
    [SerializeField] private EventChannel gameOverEventChannel;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private ScoreSO scoreLeaderboard;


    private void Awake() {
        scoreEventChannel.channel += OnScoreIncrease;
        gameOverEventChannel.channel += OnGameOver;
    }

    private void OnGameOver() {
        Time.timeScale = 0.2f;
        scoreLeaderboard.AddScore(score);
        scoreEventChannel.channel -= OnScoreIncrease;


        // TODO Change scene
        Debug.Log("Game over! your score is " + score);
    }

    private void OnScoreIncrease() {
        score += 1;
        scoreText.text = "" + score;
    }
}
