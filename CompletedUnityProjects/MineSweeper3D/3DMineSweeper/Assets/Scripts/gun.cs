using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    private bool gravGunActive;
    [SerializeField] private Material normal;
    [SerializeField] private Material flaggedMat;
    private Material emptyMat;
    private CursorMode cursorMode;
    [SerializeField] private Texture2D crossHair;
    private Vector2 center = Vector2.zero;

    private GameObject heldObj;
    private new Rigidbody objBody;
    [SerializeField] private Transform player;
    private float carryDist;
    [SerializeField]private  Font lookGood;
    [SerializeField] private GameObject prisoner;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject valSyst;

    // Lock the cursor, and set distance for things to be carried
    void Start()
    {
        cursorMode = CursorMode.Auto;
        Cursor.SetCursor(crossHair,  center, cursorMode);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        carryDist = 3f;

    }
    // Check for gun functions each frame
    void Update()
    {
        blockDestroyer();
        gravGun();
        flag();
    }

    
    private void gravGun()
    {

        Vector3 distFromPlayer = player.forward * carryDist;
        distFromPlayer += player.position;
        //If roght mouse button pressed
        if (Input.GetMouseButtonDown(1))
        {
            //Set the layer that the gravgun function will interact with
            int layerMask = 1<<9;

            //If it's active then release the object, and set values to null and active to false
            if (gravGunActive)
            {
                //If the object is a prisoner
                if(heldObj.tag != "trash")
                {
                    objBody.useGravity = true;
                }
                //If the object is the container
                else
                {
                    objBody.useGravity = false;
                }
                gravGunActive = false;
                objBody = null;
                heldObj = null;
            }
            //If object is not carried
            else
            {
                RaycastHit movableHit;
                //Cast a ray to hit an object on layermask, if hit then set that object to the held object and set distance from front of player. Turn off gravity and set active to true
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out movableHit, 50f, layerMask))
                {
                    gravGunActive = true;
                    objBody = movableHit.rigidbody;
                    heldObj = movableHit.transform.gameObject;
                    objBody.useGravity = false;

                    heldObj.transform.SetPositionAndRotation(distFromPlayer, Quaternion.identity);
                }
            }
        }else
        {   //If gravgun is active, then make the held object follow the player when the player moves
            if (gravGunActive)
            {
                heldObj.transform.SetPositionAndRotation(distFromPlayer, Quaternion.identity);
            }
        }
    }

    private void blockDestroyer()
    {
        //If left mouse button clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Set interactive layer to only 8
            int layerMask = 1 << 8;

            RaycastHit blockHit;
            //Cast a ray to see if a block is hit
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out blockHit, 100f, layerMask))
            {
                //If hit and contains a bomb, trigger explosion
                if (blockHit.transform.GetComponent<cubeClass>().getContainsBomb())
                {
                    spawnExplosion(blockHit.transform.gameObject);
                    score.GetComponent<score>().incBombsExploded();
                }
                //If hit and doesn't contain a bomb, destroy the block
                else
                {
                    cubeDestruction(blockHit.transform.gameObject);
                }
            }
        }
    }

    private void flag()
    {
        //If middle mouse button clicked
        if (Input.GetMouseButtonDown(2))
        {
            //Change interaction layer to only 8
            int layerMask = 1<<8;

            RaycastHit blockHit;
            //If a block is hit by the raycast
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out blockHit, 100f, layerMask))
            {
               blockHit.transform.gameObject.GetComponent<cubeClass>().getMaterial();

                //If the block is not flagged
                if(blockHit.transform.gameObject.GetComponent<cubeClass>().getMaterial() == normal)
                {
                    blockHit.transform.gameObject.GetComponent<cubeClass>().setMaterial(flaggedMat);
                    blockHit.transform.gameObject.GetComponent<cubeClass>().setIsFlagged(true);
                    valSyst.GetComponent<valuesSystem>().incFlagsLeft(0);
                }
                //if the block is flagged
                else
                {
                    blockHit.transform.gameObject.GetComponent<cubeClass>().setMaterial(normal);
                    blockHit.transform.gameObject.GetComponent<cubeClass>().setIsFlagged(false);
                    valSyst.GetComponent<valuesSystem>().incFlagsLeft(1);
                }
            }

        }
    }

    public void clearHeldObj()
    {
        heldObj = null;
    }

    public void clearObjBody()
    {
        objBody = null;
    }

    public void setGravGunActive(bool yes)
    {
        gravGunActive = yes;
    }

    private void cubeDestruction(GameObject cube)
    {
        Debug.Log(cube.transform.GetComponent<cubeClass>().getNearBombs());
        //If a cube destroyed is near a bomb, get rid of the rigidbody and collider, and replace the material with a textmesh showing bombs surrounding the block
        if (cube.transform.GetComponent<cubeClass>().getNearBombs())
        {
            DestroyImmediate(cube.transform.GetComponent<MeshFilter>());
            cube.transform.GetComponent<Renderer>().material = null;
            Destroy(cube.transform.GetComponent<Rigidbody>());
            Destroy(cube.transform.GetComponent<BoxCollider>());
            cube.transform.gameObject.AddComponent<TextMesh>();
            cube.transform.GetComponent<TextMesh>().font = lookGood;
            cube.transform.GetComponent<TextMesh>().characterSize = .5f;
            cube.transform.GetComponent<TextMesh>().color = Color.red;
            cube.transform.GetComponent<TextMesh>().text = cube.transform.GetComponent<cubeClass>().getNumberOfBombs().ToString();
            //cube.transform.GetComponent<cubeClass>().setSurroundedCubeDestroyed(true);

            score.GetComponent<score>().addBlockScore(cube.transform.GetComponent<cubeClass>().getBlockScore());

            //If the block contains hostages, spawn them
            if (cube.transform.GetComponent<cubeClass>().getContainsHostages())
            {
                for (int i = 0; i < cube.transform.GetComponent<cubeClass>().getNumberHostages(); i++)
                {
                    Instantiate(prisoner, cube.transform.position, Quaternion.identity);
                }
            }

        }
        //If the cube is not surrounded by bombs, then just normal destroy
        else
        {
            Destroy(cube.transform.GetComponent<MeshFilter>());
            Destroy(cube.transform.GetComponent<Rigidbody>());
            Destroy(cube.transform.GetComponent<BoxCollider>());

            score.GetComponent<score>().addBlockScore(cube.transform.GetComponent<cubeClass>().getBlockScore());
            //If the block contains hostages, spawn them
            if (cube.transform.GetComponent<cubeClass>().getContainsHostages())
            {
                for (int i = 0; i < cube.transform.GetComponent<cubeClass>().getNumberHostages(); i++)
                {
                    Instantiate(prisoner, cube.transform.position, Quaternion.identity);
                }
            }

        }
    }

    //Cause an explosion at the Gameobject sources transform
    private void spawnExplosion(GameObject source)
    {
        GameObject temp = Instantiate(explosion, source.transform.position, Quaternion.identity);
        temp.AddComponent<explosionCounter>();
    }
}
