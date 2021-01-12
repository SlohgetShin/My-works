using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttaching : MonoBehaviour
{
    public GameObject Player;

    //When the player is on a moving platform, make the parent transform of the player the platform. Makes it easier for player to stay on.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
            Player.transform.parent = transform;
    }

    //On exit reset player parent transform to null
    private void OnTriggerExit(Collider other)
    {
        Player.transform.parent = null;
    }
}
