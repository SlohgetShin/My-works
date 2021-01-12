using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScreenManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Directions;
    public GameObject CredMenu;
    // Show default menu setup
    void Start()
    {
        showMainMenu();
        hideDirections();
        hideCredits();
    }

    //Display main menu
    public void showMainMenu()
    {
        MainMenu.SetActive(true);
    }

    // Hide main menu
    public void hideMainMenu()
    {
        MainMenu.SetActive(false);
    }

    //Display direction menu
    public void showDirections()
    {
        Directions.SetActive(true);
    }

    //Hide direction menu
    public void hideDirections()
    {
        Directions.SetActive(false);
    }

    //Display credits
    public void showCredits()
    {
        CredMenu.SetActive(true);
    }

    // Hide credits
    public void hideCredits()
    {
        CredMenu.SetActive(false);
    }
}
