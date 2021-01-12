using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class calculateScore : MonoBehaviour
{
    private float finalScore;

    private float timeMulti;
    private int savedScore;
    private float goodFlagScore;
    private float badFlagScore;
    private float noFlagScore;
    private float bombsSetOffScore;
    private float cubeScore;

    private string results;
    public Text howGood;
    //Calculate the score of  the player.
    void Start()
    {
        timeMulti = determineTimeMulti(saveBetweenScenes.getFinalTime());
        savedScore = saveBetweenScenes.getPrisonersSaved() * 20;
        goodFlagScore = saveBetweenScenes.getRightFlag() * 30;
        badFlagScore = saveBetweenScenes.getWrongFlag() * -10;
        noFlagScore = saveBetweenScenes.getNoFlag() * -5;
        bombsSetOffScore = saveBetweenScenes.getBombsExploded() * -20;
        cubeScore = saveBetweenScenes.getCubeScore();
        addemUp();

    }

    // Set final score text to players score
    void Update()
    {
        howGood.text = "Final Score: " + results;
    }

    //Determine the time multiplier bonus
    private float determineTimeMulti(float time)
    {
        if(time <= 600f)
        {
            return 2f;
        }else if(time <= 1200f)
        {
            return 1.5f;
        }else if(time<= 1800f)
        {
            return 1.25f;
        }
        else
        {
            return 1f;
        }
    }

    //Add all individual scores
    private void addemUp()
    {
        float good = ((float)savedScore + (float)goodFlagScore) * timeMulti;

        finalScore = (int)good + bombsSetOffScore + noFlagScore + badFlagScore + cubeScore;

        results = finalScore.ToString("f0");
    }
}
