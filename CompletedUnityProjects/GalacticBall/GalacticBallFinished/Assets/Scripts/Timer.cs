using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float lvlTime;
    public Text Clock;
    private GameObject player;

    //Get the player object
    void Start()
    {
        player = GameObject.FindWithTag("Player");    
    }

    // Decrease the timer until it hits zero. Save the time for the win screen
    void Update()
    {
        lvlTime -= Time.deltaTime;

        string minutes = ((int)lvlTime / 60).ToString();
        string seconds = (lvlTime % 60).ToString("f2");
        Clock.text = minutes + ":" + seconds;

        if (lvlTime < 0)
        {
            timeOut();
        }

        DataSaved.setTime(lvlTime);
    }

    //Sends player to death screen if time runs out
    void timeOut()
    {
        SceneManager.LoadSceneAsync("Death");
    }

    //Getter for lvlTime
    public float getCurTime()
    {
        return lvlTime;
    }

}
