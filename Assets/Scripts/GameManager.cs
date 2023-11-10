using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private int score;
    [SerializeField] private EventChannel scoreEventChannel;
    [SerializeField] private EventChannel gameOverEventChannel;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private ScoreSO scoreLeaderboard;

    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private string leaderboardSceneName;

    private AudioSource audioSource;


    private void Awake() {
        scoreEventChannel.channel += OnScoreIncrease;
        gameOverEventChannel.channel += OnGameOver;

        audioSource = GetComponent<AudioSource>();
    }

    private void OnGameOver() {
        Time.timeScale = 0.2f;
        scoreLeaderboard.AddScore(score);
        scoreEventChannel.channel -= OnScoreIncrease;
        audioSource.clip = gameOverClip;
        audioSource.Play();

        StartCoroutine(GoToLeaderboard(.2f));
    }

    private IEnumerator GoToLeaderboard(float idleTime) {
        yield return new WaitForSeconds(idleTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene(leaderboardSceneName);
    }
    private void OnScoreIncrease() {
        score += 1;
        scoreText.text = "" + score;
    }
}
