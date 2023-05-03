using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puck : MonoBehaviour
{
    
    public Score scoreRef;

    // static bool for method availability
    public static bool hitGoal;

    public float maxSpeed;

    private Rigidbody2D puckRB;

    public bool disabled;

    private bounds puck;
    public Transform puckBounds;

    private bool hitTwice = false; 



    void Start()
    {
        puckRB = GetComponent<Rigidbody2D>();
        hitGoal = false;

        puck = new bounds(puckBounds.GetChild(0).position.y, puckBounds.GetChild(1).position.y,
                                 puckBounds.GetChild(2).position.x, puckBounds.GetChild(3).position.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the AI's goal was collided with, increment the Player's score by 1, set the WasGoal variable to true, and reset the puck position
        // Vice versa, if player's goal
        // WasGoal set to true to avoid infinite looping

        if (!hitGoal)
        {
            if (collision.tag == "AIGoal")
            {
                scoreRef.Increment(Score.enumScore.PlayerScore);
                hitGoal = true;
                StartCoroutine(pkReset(false));
            }
            else if (collision.tag == "PlayerGoal")
            {
                scoreRef.Increment(Score.enumScore.AiScore);
                hitGoal = true;
                StartCoroutine(pkReset(true));
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {          
            scoreRef.playerHit += 1;
            hitTwice = false;

            Debug.Log("Player hit puck!");

            if (scoreRef.playerHit > 1)
            {
                hitTwice = true;
                Debug.Log("Twiceeeeee!");
            }
                

        }
        else if (other.gameObject.CompareTag("AI"))
        {
            scoreRef.aiHit += 1;
            hitTwice = false;

            Debug.Log("AI hit puck!");

            if (scoreRef.aiHit > 1)
            {
                hitTwice = true;
                Debug.Log("Twiceeeeee!");

            }
        }

        if (scoreRef.aiHit > 1 || scoreRef.playerHit > 1)
        {
            if (puckRB.position.y < puck.DOWN)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    scoreRef.playerHit += 1;

                   
                    Debug.Log("Player hit puck again!");

                }
            }
            else
            {
                if(other.gameObject.CompareTag("AI"))
                {
                    scoreRef.aiHit += 1;

                    
                    Debug.Log("AI hit puck again!");
                }
            }
        }
       
    }


    private IEnumerator pkReset(bool aiScored)
    {

        // waits 1 real time second, before resetting puck position, then resets hitGoal's status to false
        yield return new WaitForSecondsRealtime(1);
        hitGoal = false;
        puckRB.velocity = puckRB.position = new Vector2(0, 0);

        if (aiScored)
            // If AI scored, set puck to players side. If Player scored, set puck to AI's side
            recenterPuck(0);
        else
            recenterPuck(1);


    }

    public void recenterPuck(int sidetoSpawn)
    {
        switch (sidetoSpawn)
        {
            case 0:
                puckRB.position = new Vector2(0, -1);
                break;
            case 1:
                puckRB.position = new Vector2(0, 1);
                break;
            case 2:
                puckRB.velocity = puckRB.position = new Vector2(0, 0);

                break;
        }
    }
    private void FixedUpdate()

    // Restricts the pucks velocity from exceeding the set maximum 
    {
        puckRB.velocity = Vector2.ClampMagnitude(puckRB.velocity, maxSpeed);
       

        if (hitTwice)
        {
            if (scoreRef.playerHit > 1 && scoreRef.aiHit <= 1)
            {
                Debug.Log("Player penalised");
                scoreRef.Decrement(Score.enumScore.PlayerScore);
                scoreRef.playerHit = scoreRef.aiHit = 0;
                pkReset(true);
               
            }

            else if (scoreRef.aiHit > 1 && scoreRef.playerHit <= 1)
            {
                Debug.Log("AI penalised");
                scoreRef.Decrement(Score.enumScore.AiScore);
                scoreRef.playerHit = scoreRef.aiHit = 0;
                pkReset(false);
                
            }
        }
        

    }
}
