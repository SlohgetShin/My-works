using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is composed of functions to show and hide main menu sub menus. These functions are called on the buttons
public class MenuController : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject LvlMenu;
    public GameObject ConMenu;
    public GameObject CredMenu;
    // Set up the main menu default look
    void Start()
    {
        showMainMenu();
        hideControls();
        hideCredits();
        hideLvlSelect();
    }

   public void showMainMenu()
    {
        MainMenu.SetActive(true);
    }

   public void hideMainMenu()
    {
        MainMenu.SetActive(false);
    }

    public void showLvlSelect()
    {
        LvlMenu.SetActive(true);
    }

    public void hideLvlSelect()
    {
        LvlMenu.SetActive(false);
    }

    public void showControls()
    {
        ConMenu.SetActive(true);
    }

    public void hideControls()
    {
        ConMenu.SetActive(false);
    }

    public void showCredits()
    {
        CredMenu.SetActive(true);
    }

    public void hideCredits()
    {
        CredMenu.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
