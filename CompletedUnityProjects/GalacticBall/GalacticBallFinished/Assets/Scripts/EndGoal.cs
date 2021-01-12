using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{

    //When the player hits the goal platform, load the win screen
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag =="Player")
        {
            load("Win");
        }
    }

    //Loads a scene
    public void load(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);

    }
}