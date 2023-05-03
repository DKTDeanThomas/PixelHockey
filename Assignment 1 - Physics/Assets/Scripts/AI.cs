using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public float setSpeed;
    private Rigidbody2D AIrb;
    private Vector2 InitialCoords;

    public Transform Bounds;
    private bounds Playerbounds;

    public Rigidbody2D Puckrb;

    public Transform puckBounds;
    private bounds puck;

    private Vector2 targetCoords;

    private bool FirstTimeinOppsHalf = true;


    void Start()
    {
        
        AIrb = GetComponent<Rigidbody2D>();
        InitialCoords = AIrb.position;

        // Defining the AI and pucks boundaries
        puck = new bounds(puckBounds.GetChild(0).position.y, puckBounds.GetChild(1).position.y,
                                 puckBounds.GetChild(2).position.x, puckBounds.GetChild(3).position.x);

        Playerbounds = new bounds(Bounds.GetChild(0).position.y, Bounds.GetChild(1).position.y,
                                 Bounds.GetChild(2).position.x, Bounds.GetChild(3).position.x);

    }

    private void FixedUpdate()
    {
        // Ref bool variable from Puck script. Runs following when WasGoal is false 
        // While a goal has not been scored yet, the pucks's position is compared to the lower puck barrier.
        // If the puck is below the AI's half, the AI follows the puck's coordinates, while staying along its own y axis. 
        // If the puck is in the AI's half, the AI will move towards the puck on both axis at a set speed
        if (!Puck.hitGoal)
        {
           
            if (Puckrb.position.y < puck.DOWN)
            {
                if (FirstTimeinOppsHalf)
                {
                    FirstTimeinOppsHalf = false;
                    
                }
                
                targetCoords = new Vector2(Mathf.Clamp(Puckrb.position.x, Playerbounds.LEFT, Playerbounds.RIGHT),
                                                     InitialCoords.y);
            }

            else
            {
                FirstTimeinOppsHalf = true;

               
                targetCoords = new Vector2(Mathf.Clamp(Puckrb.position.x, Playerbounds.LEFT, Playerbounds.RIGHT),
                                           Mathf.Clamp(Puckrb.position.y, Playerbounds.DOWN, Playerbounds.UP));

            }
            AIrb.MovePosition(Vector2.MoveTowards(AIrb.position, targetCoords, setSpeed * Time.fixedDeltaTime));
        }
    }
    public void ResetPosition()
    {
        AIrb.position = InitialCoords;
    }


}
