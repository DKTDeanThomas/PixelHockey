using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    float starttime = 5f;
    float currenttime = 0f;
    public GameObject canvasStart;
    public GameObject canvasGame;
    public Text Count;
    public GameObject canvasRestart; 
    public Score scorescript;
    public PlayerMovement playermovementscript;
    public AI aiscript;
    public Puck puckscript;
    public GameObject Win;
    public GameObject Lose;

    void Start()
    {
        currenttime = starttime;

    }

    void Update()
    {

        currenttime -= 1 * Time.deltaTime;
        Count.text = currenttime.ToString("0");

        if (currenttime <= 0)
        {
            currenttime = 0;

            canvasStart.SetActive(false);

        }

    }

    public void restartCanvas(bool aiWon)
    {
        Time.timeScale = 0;

        canvasGame.SetActive(false);
        canvasRestart.SetActive(true);

        Win.SetActive(!aiWon);
        Lose.SetActive(aiWon);
    }

    public void restartGame()
    {
        Time.timeScale = 1;

        canvasGame.SetActive(true);
        canvasRestart.SetActive(false);

        scorescript.ResetScores();
        puckscript.recenterPuck(2);
        playermovementscript.ResetPosition();
        aiscript.ResetPosition();
    }
}
