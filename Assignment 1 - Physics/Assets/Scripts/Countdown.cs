using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float starttime = 5f;
    float currenttime = 0f;
    public GameObject countScreen;

    public Text Count;
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

            countScreen.SetActive(false);

        }

    }
}
