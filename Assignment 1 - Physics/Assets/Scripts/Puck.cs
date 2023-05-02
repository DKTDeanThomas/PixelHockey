using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puck : MonoBehaviour
{
    
    public Score scoreInstance;

    // static bool for method availability
    public static bool WasGoal;

    public float maxSpeed;

    private Rigidbody2D rb;

    public bool disabled;

    private bounds puck;
    public Transform puckBounds;

   private bool hitTwice = false; 



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;

        puck = new bounds(puckBounds.GetChild(0).position.y, puckBounds.GetChild(1).position.y,
                                 puckBounds.GetChild(2).position.x, puckBounds.GetChild(3).position.x);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the AI's goal was collided with, increment the Player's score by 1, set the WasGoal variable to true, and reset the puck position
        // Vice versa, if player's goal
        // WasGoal set to true to avoid infinite looping

        if (!WasGoal)
        {
            if (other.tag == "AIGoal")
            {
                scoreInstance.Increment(Score.score.PlayerScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(false));
            }
            else if (other.tag == "PlayerGoal")
            {
                scoreInstance.Increment(Score.score.AiScore);
                WasGoal = true;
                StartCoroutine(ResetPuck(true));
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {          
            scoreInstance.playerHit += 1;
            hitTwice = false;

            Debug.Log("Player hit puck!");

            if (scoreInstance.playerHit > 1)
            {
                hitTwice = true;
                Debug.Log("Twiceeeeee!");
            }
                

        }
        else if (other.gameObject.CompareTag("AI"))
        {
            scoreInstance.aiHit += 1;
            hitTwice = false;

            Debug.Log("AI hit puck!");

            if (scoreInstance.aiHit > 1)
            {
                hitTwice = true;
                Debug.Log("Twiceeeeee!");

            }
        }

        if (scoreInstance.aiHit > 1 || scoreInstance.playerHit > 1)
        {
            if (rb.position.y < puck.DOWN)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    scoreInstance.playerHit += 1;

                   
                    Debug.Log("Player hit puck again!");

                }
            }
            else
            {
                if(other.gameObject.CompareTag("AI"))
                {
                    scoreInstance.aiHit += 1;

                    
                    Debug.Log("AI hit puck again!");
                }
            }
        }
       
    }


    private IEnumerator ResetPuck(bool didAIScore)
    {

        // waits 1 real time second, before resetting puck position, then resets WasGoal's status to false
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = rb.position = new Vector2(0, 0);

        if (didAIScore)
            // If AI scored, set puck to players side. If Player scored, set puck to AI's side
            rb.position = new Vector2(0, -1);
        else
            rb.position = new Vector2(0, 1);

    }

    public void Centerpuck()
    {
        rb.position = new Vector2(0, 0);


    }
    private void FixedUpdate()

    // Restricts the pucks velocity from exceeding the set maximum 
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
       

        if (hitTwice)
        {
            if (scoreInstance.playerHit > 1 && scoreInstance.aiHit <= 1)
            {
                Debug.Log("Player penalised");
                scoreInstance.Decrement(Score.score.PlayerScore);
                scoreInstance.playerHit = scoreInstance.aiHit = 0;
                ResetPuck(true);
               
            }

            else if (scoreInstance.aiHit > 1 && scoreInstance.playerHit <= 1)
            {
                Debug.Log("AI penalised");
                scoreInstance.Decrement(Score.score.AiScore);
                scoreInstance.playerHit = scoreInstance.aiHit = 0;
                ResetPuck(false);
                
            }
        }
        

    }
}
