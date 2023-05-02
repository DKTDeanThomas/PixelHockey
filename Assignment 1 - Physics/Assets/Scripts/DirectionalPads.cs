using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPads : MonoBehaviour
{
   
    public Vector2 newDirection;

    Vector2 currentVelocity;
    Vector2 newVelocity;
    Vector2 velocityChange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Puck")
        {
           
            currentVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;      
            newVelocity = currentVelocity.magnitude * newDirection.normalized;  
            
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;  
            
            velocityChange = newVelocity - currentVelocity;       
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(velocityChange, ForceMode2D.Impulse);
        }
    }
}
