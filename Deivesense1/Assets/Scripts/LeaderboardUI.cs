using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    [Header("Assign score texts in order (top to bottom)")]
    [SerializeField] private TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        ShowScores();
    }

    void ShowScores()
    {
        List<float> scores = LeaderboardManager.LoadScores();

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < scores.Count)
            {
                scoreTexts[i].text = (i + 1) + ". " + scores[i].ToString("0");
            }
            else
            {
                scoreTexts[i].text = (i + 1) + ". ---";
            }
        }
    }
}
