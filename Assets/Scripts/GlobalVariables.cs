using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages global variables
public class GlobalVariables : MonoBehaviour{
    //### Settings #########################
    //weather or not sound is muted
    public static bool isMuted = false;
    public static string language = "DE";


    //### Game #############################
    //unlocked levels
    private static int[] unlockedLevels;
    public static int[] UnlockedLevels {
        get {
            if(unlockedLevels == null) {
                unlockedLevels = new int[LevelGenerator.NUMBER_OF_LEVELS];
            }
            return unlockedLevels;
        }
        set { unlockedLevels = value; }
    }
    
    //index of level
    private static int levelIndex;
    public static int LevelIndex {
        get { return levelIndex; }
        set { levelIndex = value; }
    }

    //array with level plan
    private static int[][] level = new int[0][];
    public static int[][] Level {
        get { return level; }
        set { level = value; }
    }

    //roundcounter
    private static int roundCounter = 0;
    public static int RoundCounter {
        get { return roundCounter;  }
        set { roundCounter = value; }
    }

    //test if whole level is mowed
    public static bool isMowed() {
        bool result = true;
        for (int i = 0; i < level.Length; i++){
            for (int j = 0; j < level[i].Length; j++){
                result = result && (level[i][j] != LevelGenerator.HIGH_GRAS);
            }
        }
        return result;
    }

    //test if single tile is mowed
    public static bool isMowed(int x, int y) {
                return level[x][y] != LevelGenerator.HIGH_GRAS;           
    }

    //set tile to mowed
    public static void setMowed(int x, int y) {
        if (level[x][y] == LevelGenerator.HIGH_GRAS)
        {
            level[x][y] = LevelGenerator.LOW_GRAS;
        }
    }

    //reset all variables
    public static void reset() {
        LevelIndex = 0;
        level = new int [0][];
        RoundCounter = 0;
    }

}
