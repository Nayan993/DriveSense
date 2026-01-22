using UnityEngine;
using System.Collections.Generic;

public static class LeaderboardManager
{
    private const int MaxScores = 5;

    public static void SaveScore(float newScore)
    {
        List<float> scores = LoadScores();
        scores.Add(newScore);

        scores.Sort((a, b) => b.CompareTo(a)); // descending

        if (scores.Count > MaxScores)
            scores.RemoveRange(MaxScores, scores.Count - MaxScores);

        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetFloat("Score_" + i, scores[i]);
        }

        PlayerPrefs.SetInt("ScoreCount", scores.Count);
        PlayerPrefs.Save();
    }

    public static List<float> LoadScores()
    {
        List<float> scores = new List<float>();
        int count = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < count; i++)
        {
            scores.Add(PlayerPrefs.GetFloat("Score_" + i));
        }

        return scores;
    }
}
