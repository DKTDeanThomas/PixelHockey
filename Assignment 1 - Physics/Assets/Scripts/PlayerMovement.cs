using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    bool screenclick = true;
    bool canMove = true;

    Rigidbody2D rb;
    private Vector2 startCoords;

    public Transform Bounds;

    bounds playerbounds;

    Collider2D playerCollider;
   
    void Start()
    {              
        rb = GetComponent<Rigidbody2D>();
        startCoords = rb.position;
        playerCollider = GetComponent<Collider2D>();   

        // Defining the players boundaries
        playerbounds = new bounds(Bounds.GetChild(0).position.y, Bounds.GetChild(1).position.y,
                                  Bounds.GetChild(2).position.x, Bounds.GetChild(3).position.x); 
    }

    
    void Update()
    {
        // If statement that runs so long as the player presses down the left mouse button
        if(Input.GetMouseButton(0))
        {
            // obtaining current mouse coordinates per frame and storing them in the mousecoords vector2
            // converting to world coordinates as regualar screen coordinates are based on resolution. This would cause differences based on screen resolutions
            Vector2 mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(screenclick)
            {
                screenclick = false;


                if (playerCollider.OverlapPoint(mouseCoords))
                {
                    canMove = true;
                } 
                else
                {
                    canMove = false;
                }
            }

            if (canMove) 
            {
                // Ensures the player paddle does not move outside the defined boundaries by restricting movement
                Vector2 clampedmouseCoords = new Vector2(Mathf.Clamp (mouseCoords.x, playerbounds.LEFT, playerbounds.RIGHT),
                                                         Mathf.Clamp (mouseCoords.y, playerbounds.DOWN, playerbounds.UP));

                // Makes sure the players paddle is alligned with the Clamped mouses current coordinates, per frame
                rb.MovePosition(clampedmouseCoords);
            }
        }
        else
        { 
            screenclick = true; 
        }

    }

    public void ResetPosition()
    {
        rb.position = startCoords;
    }
}
