using System;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int[] highScores = new int[5];
    private TextMeshProUGUI[] highScoreTexts;

    private void Awake()
    {
        Transform panel = transform.Find("HighScores");
        foreach (Transform tr in panel)
        {
            if (tr.name.Contains("Score"))
            {
                var textTransform = tr.Find("Text");
                if (textTransform == null) continue;
                TextMeshProUGUI highScore = textTransform.GetComponent<TextMeshProUGUI>();
                if (highScore == null) continue;

                Array.Resize(ref highScoreTexts, highScoreTexts == null ? 1 : highScoreTexts.Length + 1);
                highScoreTexts[highScoreTexts.Length - 1] = highScore;
            }
        }
        LoadHighScores();
    }

    public void Initalize()
    {
        for (int i = 0; i < highScores.Length; i++)
            PlayerPrefs.SetInt("HighScore" + i, 0);
    }

    public void SetText(int index)
    {
        if (highScoreTexts == null) return;
        if (index < 0 || index >= highScoreTexts.Length) return;
        var text = highScoreTexts[index];
        if (text == null) return;
        text.text = "Score " + (index + 1) + ": " + highScores[index];
    }

    public void SaveHighScores()
    {
        PlayerPrefs.Save();
    }

    public void LoadHighScores()
    {
        if (!PlayerPrefs.HasKey("HighScore0")) Initalize();
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = PlayerPrefs.GetInt("HighScore" + i, 0);
            SetText(i);
        }
    }

    public void SetHighScore(int index, int score)
    {
        highScores[index] = score;
        PlayerPrefs.SetInt("HighScore" + index, score);
        SetText(index);
    }

    public void CheckAndSetHighScore(int value)
    {
        if (highScores == null || highScores.Length == 0) return;

        int lowest = highScores[0];
        int index = 0;

        for (int i = 1; i < highScores.Length; i++)
        {
            if (highScores[i] <= lowest)
            {
                lowest = highScores[i];
                index = i;
            }
        }

        if (value > lowest) SetHighScore(index, value);
    }

}
