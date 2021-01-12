using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prisonerContainer : MonoBehaviour
{

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject scoreBoard;

    //When the prisoner collides with another object.
    void OnCollisionEnter(Collision other)
    {
        //If it collides withe the collecting container, destroy the prisoner, reset the gravGun values, add to prisoners saved.
        if(other.gameObject.tag == "trash"){

            gun.GetComponent<gun>().setGravGunActive(false);
            gun.GetComponent<gun>().clearHeldObj();
            gun.GetComponent<gun>().clearObjBody();
            scoreBoard.GetComponent<score>().incrementPrisoner();
            Destroy(gameObject);
        }

        //IF it collides with an explosion, destroy the prisoner
        if(other.gameObject.tag == "explosion")
        {
            Destroy(gameObject);
        }
    }
}
