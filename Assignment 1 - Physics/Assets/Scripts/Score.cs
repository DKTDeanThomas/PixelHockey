using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int playerHit = 0;
    public int aiHit = 0;

    public enum score
    {
        AiScore, PlayerScore
    }

    public Text AiScoretxt, PlayerScoretxt;

    public UIManager ui;

    public int maxScore;

    private int aiscore, playerscore;

    private int AiScore
    {
        get { return aiscore; }
        set
        {
        aiscore = value;

            if (value == maxScore)
                ui.ShowRestartCanvas(true);
        }
    }

    private int PlayerScore
    {
        get { return playerscore; }
        set
        {
            playerscore = value;

            if (value == maxScore)
                ui.ShowRestartCanvas(false);
        }
    }

    public void Increment(score whichScore)
    {
        if (whichScore == score.AiScore)
        {
            AiScoretxt.text = (++AiScore).ToString();
           
        }

        else
        {
            PlayerScoretxt.text = (++PlayerScore).ToString();
         
        }
    }

    public void Decrement(score whichScore)
    {
        if (whichScore == score.AiScore)
        {
            AiScoretxt.text = (--AiScore).ToString();
            
        }

        else
        {
            PlayerScoretxt.text = (--PlayerScore).ToString();
            
        }
    }

    public void ResetScores()
    {
        AiScore = PlayerScore = 0;
        AiScoretxt.text = PlayerScoretxt.text = "0";
    }
}
