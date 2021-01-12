using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class used to update the flags a player has left to use
public class displayFlags : MonoBehaviour
{

    private int flags;
    public Text show;
    public GameObject valSyst;

    // Get the flags left value and update the text on the player hud
    void Update()
    {
        flags = valSyst.GetComponent<valuesSystem>().getFlagsLeft();
        string temp = flags.ToString();
        show.text = temp;
    }
}
