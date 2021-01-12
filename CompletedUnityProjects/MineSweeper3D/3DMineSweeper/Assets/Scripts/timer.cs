using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private float lvlTime;
    public Text Clock;


    // Increment time and display it to the player. Save time to value used for final score. 
    void Update()
    {
        lvlTime += Time.deltaTime;

        string seconds = lvlTime.ToString("f0");
        Clock.text = seconds;

        saveBetweenScenes.setFinalTime(lvlTime);
    }

    //getter for lvlTime
    public float getCurTime()
    {
        return lvlTime;
    }
}
