using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    GameObject[] pauseMenu;
    Scene curScene;
    string lastBeat;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gun;

    private CursorMode cursorMode;
    [SerializeField] private Texture2D crossHair;
    private Vector2 center = Vector2.zero;


    // Find the pauseMenu, and set it to not display
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu = GameObject.FindGameObjectsWithTag("pauseObj");
        dontShowPauseMenu();

    }

    // Update is called once per frame
    void Update()
    {
        //If in the GameScene and wants to pause
        if (Input.GetKeyDown(KeyCode.P) && canScenePause(SceneManager.GetActiveScene()))
        {
            //If not paused, pause and disable the gun script and playerController, unlock the cursor, show the menu.
            if (Time.timeScale == 1)
            {
                player.GetComponent<playerController>().enabled = false;
                gun.GetComponent<gun>().enabled = false;
                Time.timeScale = 0;
                cursorMode = CursorMode.Auto;
                Cursor.SetCursor(null, center, cursorMode);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                showPauseMenu();

            }
            //If paused, unpause and enable the gun script and playercontroller, lock the cursor, hide the menu 
            else
            {
                Time.timeScale = 1;

                player.GetComponent<playerController>().enabled = true;
                gun.GetComponent<gun>().enabled = true;
                cursorMode = CursorMode.Auto;
                Cursor.SetCursor(crossHair, center, cursorMode);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
                dontShowPauseMenu();
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

    //Loads scene based on sceneName. Utilized in the buttons
    public void load(string sceneName)
    {
        cursorMode = CursorMode.Auto;
        Cursor.SetCursor(null, center, cursorMode);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadSceneAsync(sceneName);

    }

    //Checks if the scene can pause
    public bool canScenePause(Scene activeScene)
    {
        if (activeScene.name.Contains("Game"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Closes the game
    public void exitGame()
    {
        Application.Quit();
    }


}
