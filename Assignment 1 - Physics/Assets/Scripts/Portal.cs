using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Puck puck;
    public GameObject portal;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered");
        if (other.tag == "Puck")
        {           
            StartCoroutine("Teleport");
        }


    }

    IEnumerator Teleport()
    {
        puck.disabled = true;
        GameObject.FindWithTag("Puck").transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
        puck.disabled = false;
        yield return new WaitForSeconds(1f);
    }
}
