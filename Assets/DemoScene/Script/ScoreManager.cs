using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int multiplier;
    public static int HighScore;

    void Start ()
    {
        score = 0;
        multiplier = 1;
    }
	
    public void AddScore(int point)
    {
        score += point *multiplier;
        if (score >= HighScore)
        {
            HighScore = score;
        }
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

    public static void SetMultiplier(int multi)
    {
        if(multi < 1)
        {
            multiplier = 1;
            return;
        }
        multiplier = multi;
    }

    public static void Reset()
    {
        score = 0;
        multiplier = 1;
    }
}
