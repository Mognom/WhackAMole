using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class ScoreSO : ScriptableObject
{
    private List<int> scores;
    private bool isLastHighestScore = false;

    public void AddScore(int newScore) {
        isLastHighestScore = scores.LastOrDefault() < newScore;
        scores.Add(newScore);
        scores.Sort();
    }

    public List<int> getTopTenScores() {
        List<int> top10 = (from s in scores orderby s descending select s).Take(10).ToList();

        return top10;
    }

    public bool IsLastHighestScore() {
        return isLastHighestScore;
    }
}
