using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public enum enumScore
    {
        AiScore,
        PlayerScore
    }

    public int playerHit = 0;
    public int aiHit = 0;

    public UI ui;

    public int winScore;


    public Text AiScoretxt;
    public Text PlayerScoretxt;


    public int Aiscore;
    public int Playerscore;

    private void FixedUpdate()
    {
        if(Aiscore == winScore)
        {
            ui.restartCanvas(true);
        }
        else if(Playerscore == winScore)
        {
            ui.restartCanvas(false);
        }
    }


    public void Increment(enumScore scoreType)
    {
        if (scoreType == enumScore.AiScore)
        {
            Aiscore++;
            AiScoretxt.text = Aiscore.ToString();
            
        }

        else
        {
            Playerscore++;
            PlayerScoretxt.text = Playerscore.ToString();
         
        }
    }

    public void Decrement(enumScore scoreType)
    {
        if (scoreType == enumScore.AiScore)
        {
            Aiscore--;
            AiScoretxt.text = Aiscore.ToString();

        }

        else
        {
            Playerscore--;
            PlayerScoretxt.text = Playerscore.ToString();
         
            
        }
    }

    public void ResetScores()
    {
        Aiscore = Playerscore = 0;
        AiScoretxt.text = PlayerScoretxt.text = "0";
    }
}
