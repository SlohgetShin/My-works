using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Stuff : MonoBehaviour
{

    GameObject[] pauseMenu;
    Vector3 tempVeloc;
    Vector3 tempAngVeloc;
    Scene curScene;
    string lastBeat;
    // Gets the pause screen object and sets the timescale
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu = GameObject.FindGameObjectsWithTag("pauseObj");
        dontShowPauseMenu();
    }

    // Check for the player pausing, and show the pause screen if they do
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canScenePause(SceneManager.GetActiveScene()))
        {
            if(Time.timeScale == 1)
            {
                tempVeloc =playerController.rb.velocity;
                tempAngVeloc =playerController.rb.angularVelocity;
                Time.timeScale = 0;
                showPauseMenu();

            }
            else
            {
                Time.timeScale = 1;
                dontShowPauseMenu();
                playerController.rb.velocity = tempVeloc;
                playerController.rb.angularVelocity= tempAngVeloc;
            }

        }


    }

    public void showPauseMenu()
    {
        for (int i = 0; i < pauseMenu.Length; i++)
        {
            pauseMenu[i].SetActive(true);
        }
    }

    public void dontShowPauseMenu()
    {
        for (int i = 0; i < pauseMenu.Length; i++)
        {
            pauseMenu[i].SetActive(false);
        }
    }

    //Loads scenes
    public void load(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);

    }

    //restarts the current level
    public void restart()
    {
        curScene = SceneManager.GetActiveScene();
        load(curScene.name);
    }

    //Determines if a scene can access the pause menu. Only levels can pause
    public bool canScenePause(Scene activeScene)
    {
        if (activeScene.name.Contains("Level"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Loads the last level a player was on from gameover or win screen
    public void replayLastLevel()
    {
        lastBeat = DataSaved.getLastSceneName();
        load(lastBeat);
    }


}
