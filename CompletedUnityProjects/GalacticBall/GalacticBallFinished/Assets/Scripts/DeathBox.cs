using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    //If a player falls off the stage and hots this box, send to death screen
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            load("Death");
        }
    }

    //Loads a scene
    public void load(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);

    }
}
