using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshPro text;
    public enum Disp
    {
        SCORE,
        HIGHSCORE,
        MULTIPLIER
    }
    public Disp disp;

	// Use this for initialization
	void Start ()
    {
		switch(disp)
        {
            case Disp.SCORE:
                text.text = "Score: " + ScoreManager.score;
                break;
            case Disp.HIGHSCORE:
                text.text = "Highscore: " + ScoreManager.HighScore;
                break;
            case Disp.MULTIPLIER:
                text.text = "Samba: " + ScoreManager.multiplier;
                break;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (disp)
        {
            case Disp.SCORE:
                text.text = "Score: " + ScoreManager.score;
                break;
            case Disp.HIGHSCORE:
                text.text = "Highscore: " + ScoreManager.HighScore;
                break;
            case Disp.MULTIPLIER:
                text.text = "Samba: " + ScoreManager.multiplier;
                break;
        }
    }
}
