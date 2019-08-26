using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    public void IncreaseScore()
    {
        score++;
        PlayerPrefs.SetInt("CurrentScore", score);

        if(score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
