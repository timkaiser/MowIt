﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gras : MonoBehaviour {
    //Position in GlobalVariables.Level array
    int xIndex, yIndex;
    
    //sprites for low gras and high gras in all variations
    private static Sprite s_low;
    private static Sprite[] s_high;

    // Update is called once per frame
    private void Start()
    {
        //load sprites
        if(s_low == null){ s_low = Resources.Load<Sprite>("Sprites/graslow"); }
        if(s_high == null) { s_high = Resources.LoadAll<Sprite>("Sprites/gras_high");  }
    }

    void Update () {
        //cancel if level is empty (to avoid errors)
        if(GlobalVariables.Level.Length == 0) { return; }

        //Test wether gras is high or low
        if (GlobalVariables.Level[xIndex][yIndex] == LevelGenerator.LOW_GRAS || GlobalVariables.Level[xIndex][yIndex] == LevelGenerator.PLAYER) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = s_low;
        } else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = s_high.Single(s => s.name == ("gras_high_"+ getSurroundingGras(GlobalVariables.Level, xIndex,yIndex)));
        }

    }

    //returns surrounding low gras (to find the right high gras tile)
    private static string getSurroundingGras(int[][] level, int x, int y) {
        return "" + (isLowGras(level,x,y+1)? "u" : "") 
            + (isLowGras(level, x, y - 1) ? "d" : "") 
            + (isLowGras(level, x-1, y) ? "l" : "") 
            + (isLowGras(level, x+1, y) ? "r" : "");
    }
    
    //test if gras is low
    private static bool isLowGras(int[][] level, int x, int y) {
        return (x >= 0) && (x<level.Length) && (y >= 0) && (y < level[0].Length) && (level[x][y] == LevelGenerator.LOW_GRAS || level[x][y] == LevelGenerator.PLAYER);
    }

    //sets position
    public void setPosition(int x, int y) {
        this.xIndex = x;
        this.yIndex = y;
    }
}
