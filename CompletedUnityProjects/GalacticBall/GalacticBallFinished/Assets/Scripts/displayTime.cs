using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class displayTime : MonoBehaviour
{
    public Text FinalTime;
    float finaltime;

    //Displays the final time on the win screen
    void Start()
    {
        finaltime = DataSaved.getTime();
        string minutes = ((int)finaltime / 60).ToString();
        string seconds = (finaltime % 60).ToString("f2");
        FinalTime.text = minutes + ":" + seconds;
        
    }





}
