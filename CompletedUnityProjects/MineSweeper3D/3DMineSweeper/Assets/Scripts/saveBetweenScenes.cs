using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is used to store the values used for calculating the final score for the player.
 * consist of getters and setters for all values
 */
public static class saveBetweenScenes 
{
    private static int cubeScore;
    private static int rightFlag;
    private static int wrongFlag;
    private static int noFlag;
    private static int prisonersSaved;
    private static float finalTime;
    private static int bombsExploded;

    public static int getCubeScore()
    {
        return cubeScore;
    }

    public static int getRightFlag()
    {
        return rightFlag;
    }

    public static int getWrongFlag()
    {
        return wrongFlag;
    }

    public static int getNoFlag()
    {
        return noFlag;
    }

    public static int getPrisonersSaved()
    {
        return prisonersSaved;
    }

    public static float getFinalTime()
    {
        return finalTime;
    }

    public static int getBombsExploded()
    {
        return bombsExploded;
    }

    public static void setCubeScore(int i)
    {
        cubeScore = i;
    }

    public static void setRightFlag(int i)
    {
        rightFlag = i;
    }

    public static void setWrongFlag(int i)
    {
        wrongFlag = i;
    }

    public static void setNoFlag(int i)
    {
        noFlag = i;
    }

    public static void setPrisonersSaved(int i)
    {
        prisonersSaved = i;
    }

    public static void setFinalTime(float i)
    {
        finalTime = i;
    }

    public static void setBombsExploded(int i)
    {
        bombsExploded = i;
    }


}
