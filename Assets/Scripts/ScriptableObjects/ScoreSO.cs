using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ScoreSO : ScriptableObject {
    [SerializeField] private List<Score> scores;
    private int currentScoreIndex;

    public void AddScore(int newScore) {
        //isLastHighestScore = scores.LastOrDefault().Points < newScore;
        Score score = new Score(newScore, Environment.UserName);
        scores.Add(score);
        // Sort descending
        scores.Sort((x, y) => y.Points.CompareTo(x.Points));
        currentScoreIndex = scores.IndexOf(score);
    }

    public List<Score> getTopSevenScores() {
        List<Score> topScores = (from s in scores select s).Take(7).ToList();
        return topScores;
    }

    public int GetCurrentScoreIndex() {
        return currentScoreIndex;
    }

    public Score GetCurrentScore() {
        return scores[currentScoreIndex];
    }


    [System.Serializable]
    public struct Score {
        public int Points;
        public string Player;
        public Score(int score, String player) {
            this.Points = score;
            this.Player = player;
        }
    }
}
