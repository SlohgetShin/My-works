using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Saved data used for replaying levels, and saving a players end time
public static class DataSaved
{
    private static float finalTime;
    private static string lastScene;


    public static void setTime(float time)
    {
        finalTime = 60.0f - time;
    }

    public static float getTime()
    {
        return finalTime;
    }

    public static void setLastSceneName(Scene before)
    {
        lastScene = before.name;
    }

    public static string getLastSceneName()
    {
        return lastScene;
    }

    
}
