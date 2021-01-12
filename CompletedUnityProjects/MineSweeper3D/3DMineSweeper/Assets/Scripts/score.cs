using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{

    private int prisonersSaved;
    private int bombsExploded;
    private int blockScore;

    //Set all values to 0
    void Start()
    {
        prisonersSaved = 0;
        bombsExploded = 0;
        blockScore = 0;
    }

    //Prisoners saved increment
    public void incrementPrisoner()
    {
        prisonersSaved++;
    }


    //getter for prisoners saved
    public int getPrisonersSaved()
    {
        return prisonersSaved;
    }
    
    // getter for bombs exploded
    public int getbombsExploded()
    {
        return bombsExploded;
    }

    //Bombs exploded increment
    public void incBombsExploded()
    {
        bombsExploded++;
    }

    //Getter for block score
    public int getBlockScore()
    {
        return blockScore;
    }

    //increment for cube score
    public void addBlockScore(int score)
    {
        blockScore += score;
    }


    // Save the values for end score 
    public void valueSave()
    {
        saveBetweenScenes.setCubeScore(blockScore);
        saveBetweenScenes.setPrisonersSaved(prisonersSaved);
        saveBetweenScenes.setBombsExploded(bombsExploded);
    }
}
