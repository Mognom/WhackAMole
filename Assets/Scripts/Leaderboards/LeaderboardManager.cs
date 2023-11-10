using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Score = ScoreSO.Score;

public class LeaderboardManager : MonoBehaviour {
    [SerializeField] private ScoreSO scoresLeaderboard;
    [SerializeField] private GameObject[] scoreRows;
    [SerializeField] private Material rainbowMat;

    [SerializeField] private string gameSceneName;

    private void Awake() {
        List<Score> topSeven = scoresLeaderboard.getTopSevenScores();

        int currentScore = scoresLeaderboard.GetCurrentScoreIndex();

        for (int i = 0; i < topSeven.Count && i < 7; i++) {
            Score score = topSeven[i];

            UpdateRowText(i, score, currentScore == i);
        }
        if (currentScore >= 7) {
            Score score = scoresLeaderboard.GetCurrentScore();
            UpdateRowText(7, score, true);
        }
    }

    private void UpdateRowText(int row, Score score, bool current) {
        GameObject scoreUI = scoreRows[row];
        scoreUI.SetActive(true);
        Text[] texts = scoreUI.GetComponentsInChildren<Text>(); // Can't use TMP with materials :(

        texts[0].text = "" + score.Player;
        texts[1].text = "" + score.Points;
        if (current) {
            texts[0].material = rainbowMat;
            texts[1].material = rainbowMat;
        }
    }

    public void OnReplayPressed() {
        // TODO look for a non-string way
        SceneManager.LoadScene(gameSceneName);
    }
}
