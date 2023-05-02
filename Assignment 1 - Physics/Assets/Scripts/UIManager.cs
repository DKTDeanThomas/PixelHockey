using UnityEngine;


public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject Win;
    public GameObject Lose;

    [Header("Other")]
    public Score scorescript;
    public PlayerMovement playermovementscript;
    public AI aiscript;
    public Puck puckscript; 

    public void ShowRestartCanvas(bool didAiWin)
    {
        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (didAiWin)
        {
            Win.SetActive(false);
            Lose.SetActive(true);
        }

        else
        {
            Win.SetActive(true);
            Lose.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scorescript.ResetScores();
        puckscript.Centerpuck();
        playermovementscript.ResetPosition();
        aiscript.ResetPosition();
    }
}
