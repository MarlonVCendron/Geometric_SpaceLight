using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScoreText : MonoBehaviour
{

    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;

    void Start()
    {
        
        try
        {
            score.text = "PONTOS:\n\n" + PlayerPrefs.GetInt("CurrentScore").ToString();
            highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        }
        catch
        {
            Debug.Log("Erro");
        }
    }
}
