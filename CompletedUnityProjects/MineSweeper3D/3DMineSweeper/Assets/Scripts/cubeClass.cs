using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cubeClass : MonoBehaviour
{
    private bool isCleared;
    private bool containsBomb;
    private bool nearBombs;
    private bool containsHostages;
    private bool isFlagged;
    private int bombsNear;
    private int numberOfHostages;
    private int blockScore;
    public List<GameObject> surroundings;
    [SerializeField] private Material normal;
    [SerializeField] private Material flaggedMat;
    private Material emptyMat;
    private bool surroundedCubeDestroyed;

    public cubeClass()
    {
        containsBomb = false;
        isFlagged = false;
        nearBombs = false;
        containsHostages = false;
        bombsNear = 0;
        surroundings = new List<GameObject>();
        blockScore = 4;
        surroundedCubeDestroyed = false;
    }

    void Start()
    {
        //Count the surrounding bombs of a cube
        for(int i =0; i < surroundings.Count; i++)
        {
            if (surroundings[i].GetComponent<cubeClass>().getContainsBomb())
            {
                setNearBombs(true);
                blockScore *= 2;
                bombsNear++;
            }
        }
    }

    void update()
    {
        /*if (surroundedCubeDestroyed)
        {
            Debug.Log("workin");
            gameObject.transform.rotation = Quaternion.LookRotation(gameObject.transform.position - Camera.main.transform.position);
        }*/
    }

    public void setSurroundedCubeDestroyed(bool value)
    {
        surroundedCubeDestroyed = value;
    }

    public bool getContainsBomb()
    {
        return containsBomb;
    }

    public void setNearBombs(bool yes)
    {
        nearBombs = yes;
    }

    public void setContainsBomb(bool bomb)
    {
        containsBomb = bomb;
    }

    public bool getNearBombs()
    {
        return nearBombs;
    }

    public void setBombsNear(int bombs)
    {
        bombsNear = bombs;
    }

    public bool getContainsHostages()
    {
        return containsHostages;
    }

    public void setContainsHostages(bool hostage)
    {
        containsHostages = hostage;
    }

    public void setNumberHostages(int hostages)
    {
        numberOfHostages = hostages;
    }

    public int getNumberHostages()
    {
        return numberOfHostages;
    }

    public void setIsFlagged(bool flag)
    {
        isFlagged = flag;
    }

    public bool getIsFlagged()
    {
        return isFlagged;
    }

    public void setMaterial(Material mater)
    {
        GetComponent<Renderer>().sharedMaterial = mater;
    }

    public Material getMaterial()
    {
        return GetComponent<Renderer>().sharedMaterial;
    }

    public int getNumberOfBombs()
    {
        return bombsNear;
    }

    public int getBlockScore()
    {
        return blockScore;
    }

    //If an explosion hits a cube
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "explosion")
        {
            setContainsHostages(false);
            setNumberHostages(0);
        }
    }




}
