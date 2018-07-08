using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour{
    public static bool Mute {
        get; set;
    }

    private static int[][] level = new int[0][];
    public static int[][] Level {
        get { return level; }
        set { level = value; }
    }

    private static int roundCounter = 0;
    public static int RoundCounter
    {
        get { return roundCounter;  }
        set { roundCounter = value; }
    }


    public static bool isMowed()
    {
        bool result = true;
        for (int i = 0; i < level.Length; i++)
        {
            for (int j = 0; j < level[i].Length; j++)
            {
                result = result && (level[i][j] != LevelGenerator.HIGH_GRAS);
            }
        }
        return result;
    }

    public static bool isMowed(int x, int y)
    {
                return level[x][y] != LevelGenerator.HIGH_GRAS;           
    }

    public static void setMowed(int x, int y)
    {
        if (level[x][y] == LevelGenerator.HIGH_GRAS)
        {
            level[x][y] = LevelGenerator.LOW_GRAS;
        }
    }

    public static void reset()
    {
        level = null;
        RoundCounter = 0;
    }

}
