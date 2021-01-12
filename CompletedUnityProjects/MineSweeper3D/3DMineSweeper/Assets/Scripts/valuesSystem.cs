using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class valuesSystem : MonoBehaviour
{
    private int totalBombs;
    private int totalHostages;
    private int bombsNeedDispersed;
    private int hostagesNeedDispersed;
    private GameObject[, ,] playArea;
    private float time;
    [SerializeField] private GameObject prisons;
    private int areaSize;
    private System.Random rand;
    private int rightFlag, wrongFlag, noFlag;
    private int flagsLeft;

    // Called once when the scene loads
    // Sets the number of bombs, hostages, and game area size
    void Awake()
    {
        totalBombs = 100;
        totalHostages = 100;
        bombsNeedDispersed = totalBombs;
        hostagesNeedDispersed = totalHostages;
        rand = new System.Random();
        areaSize = 8;
        playArea = new GameObject[areaSize,areaSize,areaSize];
        rightFlag = 0;
        wrongFlag = 0;
        noFlag = 0;
        flagsLeft = 100;

    }


    // Start is called before the first frame update
    /* Generates the play area, and sets the bombs and hostages in the cube
     * Fills the cubes surrounding list with the cubes around a given cube
    */
    void Start()
    {
        playAreaGeneration(areaSize);
        cubeSurroundings(areaSize);
    }

    //Creates the play area for the player given the side length of the area
    private void playAreaGeneration(int length)
    {
        for(int i =0; i<length; i++)
        {
            for(int j =0; j<length; j++)
            {
                for(int k=0; k<length; k++)
                {
                    GameObject cube = Instantiate(prisons, new Vector3(500f + (2*i),50f + (j*2),2f+ (k*2)), Quaternion.identity);
                    playArea[i, j, k] = cube;
                }
            }
        }
        //While bombs still need dispersed, find an empty cube, set it to contain a bomb
        while(bombsNeedDispersed >0)
        {
            GameObject temp = playArea[rand.Next(0,length), rand.Next(0,length), rand.Next(0,length)];
            if (temp.GetComponent<cubeClass>().getContainsBomb() == false && temp.GetComponent<cubeClass>().getContainsHostages() == false)
            {
                temp.GetComponent<cubeClass>().setContainsBomb(true);
                bombsNeedDispersed--;
            }
            
        }

        //While hostages need dispersed, find an empty cube, give it 1-5 hostages
        while(hostagesNeedDispersed>0)
        {
            GameObject temp = playArea[rand.Next(0, length), rand.Next(0, length), rand.Next(0, length)];
            if (temp.GetComponent<cubeClass>().getContainsBomb() == false && temp.GetComponent<cubeClass>().getContainsHostages() == false)
            {
                temp.GetComponent<cubeClass>().setContainsHostages(true);
                int hostgNum = rand.Next(1, 5);
                if (hostgNum > hostagesNeedDispersed)
                {
                    hostgNum = hostagesNeedDispersed;
                }
                hostagesNeedDispersed = hostagesNeedDispersed - hostgNum;
                temp.GetComponent<cubeClass>().setNumberHostages(hostgNum);

            }
        }
    }
    
    //Fills the cubes surrounding list dependent upon what part of the play area the cube is a part of. x,y,z = i,j,k
    private void cubeSurroundings(int length)
    {
        int maxInd = length - 1;
        for (int i =0; i< length; i++)
        {
            for (int j=0; j<length; j++)
            {
                for (int k=0; k<length; k++)
                {
                    //If a vertex
                    if ( (i == 0 || i == maxInd) && (j == 0 || j == maxInd) && (k == 0 || k == maxInd) )
                    {
                        if(k == 0 && j == 0)
                        {
                            
                            if (i == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                            }
                            
                            else { 
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                            }
                        }else if(k == maxInd && j == 0)
                        {
                            
                            if (i == 0)
                            {

                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);

                            }
                            else {    
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                            }
                        }
                        else if(k == 0 && j == maxInd)
                        {
                            if (i == 0)
                            {

                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                            }
                            else { 
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                            }
                            else {   
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);
                                    playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                            }
                        }

                    //If an edge. Looking for a cube with only two values that are 0 or maxInd 
                    }else if ( ((i == 0 || i == maxInd) && (j == 0 || j == maxInd)) || ((i == 0 || i == maxInd) && (k == 0 || k == maxInd)) || ((k == 0 || k == maxInd) && (j == 0 || j == maxInd)) )
                    {

                        if(i != 0 && i != maxInd)
                        {
                            if(k == 0 && j == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                            }
                            else if(k == 0 && j == maxInd)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                            }
                            else if(k == maxInd && j == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                            }
                            else
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                            }

                        }else if (k != 0 && k != maxInd)
                        {
                            if (i == 0 && j == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k-1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                            }
                            else if (i == 0 && j == maxInd)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                            }
                            else if (i == maxInd && j == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                            }
                            else
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                            }

                        }else
                        {
                            if (k == 0 && i == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);

                            }
                            else if (k == 0 && i == maxInd)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                            }
                            else if (k == maxInd && i == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                            }
                            else
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                            }

                        }

                    //If on a face. Looking for cube with only one value that is 0 or maxInd
                    }else if ( ((i == 0 || i == maxInd) && (k != 0 && k!= maxInd) && (j != 0 && j!= maxInd)) || ((i != 0 && i != maxInd) && (k == 0 || k == maxInd) && (j != 0 && j != maxInd)) || ((i != 0 && i != maxInd) && (k != 0 && k != maxInd) && (j == 0 || j == maxInd)) )
                    {
                        if (i == 0 || i == maxInd)
                        {
                            if(i == 0) {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);
                            }
                            else
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);
                            }
                        }else if (j == 0 || j == maxInd)
                        {
                            if( j == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);

                            }
                            else
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);
                            }
                        }else
                        {
                            if(k == 0)
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);

                            }
                            else
                            {
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);
                                playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);

                            }
                        }
                    //If in the middle
                    }else
                    {
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j + 1), (k - 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j + 1), (k - 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j + 1), (k - 1)]);

                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, j, (k - 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), j, (k - 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), j, (k - 1)]);

                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), k]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[i, (j - 1), (k - 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i - 1), (j - 1), (k - 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k + 1)]);
                        playArea[i, j, k].GetComponent<cubeClass>().surroundings.Add(playArea[(i + 1), (j - 1), (k - 1)]);

                    }

                }
            }
        }
    }

    //Checks for cubes that were flagged correctly, incorrectly, or were not flagged when should have been. Sends those values to the static script that calculates players score. 
    public void checkFlagged()
    {
        for (int i =0; i < areaSize; i++)
        {
            for (int j=0; j< areaSize; j++)
            {
                for (int k=0; k<areaSize; k++)
                {
                    if (playArea[i, j, k].GetComponent<cubeClass>().getContainsBomb() == true && playArea[i, j, k].GetComponent<cubeClass>().getIsFlagged() == true)
                        rightFlag++;

                    if (playArea[i, j, k].GetComponent<cubeClass>().getContainsBomb() == true && playArea[i, j, k].GetComponent<cubeClass>().getIsFlagged() == false)
                        noFlag++;

                    if (playArea[i, j, k].GetComponent<cubeClass>().getContainsBomb() == false && playArea[i, j, k].GetComponent<cubeClass>().getIsFlagged() == true)
                        wrongFlag++;
                }
            }
        }

        saveBetweenScenes.setRightFlag(rightFlag);
        saveBetweenScenes.setWrongFlag(wrongFlag);
        saveBetweenScenes.setNoFlag(noFlag);
    }

    //getter for rightFlag
    public int  getRightFlag()
    {
        return rightFlag;
    }

    //getter for wrongFlag
    public int getWrongFlag()
    {
        return wrongFlag;
    }

    //getter for noFlag
    public int getNoFlag()
    {
        return noFlag;
    }

    //Changes flags left
    public void incFlagsLeft(int i) 
    { 
        if(i == 0)
        {
            flagsLeft--;
        }
        else
        {
            flagsLeft++;
        }
    }

    //getter for flagsLeft
    public int getFlagsLeft()
    {
        return flagsLeft;
    }

}
