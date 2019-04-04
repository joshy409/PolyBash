using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int multiplier;
    public TextMeshPro txt;
    public static int HighScore;

    void Start ()
    {
        score = 0;
        multiplier = 1;
        Debug.Log(name, this);
    }
	
    public void AddScore(int point)
    {

        score += point *multiplier;
        if (score >= HighScore)
        {
            HighScore = score;
        }
        txt.text = "Score: " + score.ToString();
        Debug.Log(name, this);
        Debug.Log(score, this);
        Debug.Log(multiplier, this);
    }

    public void AddMultiplier(int multi)
    {
        if ((multiplier + multi) <= 1)
        {
            multiplier = 1;
            return;
        }
        multiplier += multi;
    }

}
