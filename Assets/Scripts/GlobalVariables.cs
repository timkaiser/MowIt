using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {
    private static GlobalVariables instance;
    public int[][] level;
    // Use this for initialization
    private void Start()
    {
        get();
		DontDestroyOnLoad (this.gameObject);
    }

    private void Setup () {
        instance = this;
		if(level == null){
        level = new int[5][]; // up -->
	        int[] level0 = { 5, 5, 0, 0, 0 }; level[0] = level0;
	        int[] level1 = { 5, 5, 0, 0, 0 }; level[1] = level1;
	        int[] level2 = { 0, 0, 2, 0, 0 }; level[2] = level2;
	        int[] level3 = { 0, 0, 0, 0, 3 }; level[3] = level3;
	        int[] level4 = { 0, 0, 3, 0, 0 }; level[4] = level4;
		}   
    }

    private void Update()
    {
    }

    public static GlobalVariables get()
    {
        if (instance == null) {
            instance = GameObject.FindObjectOfType<GlobalVariables>();
            instance.Setup();
        }
        return instance;
    }

    public bool isMowed()
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

    public bool isMowed(int x, int y)
    {
                return level[x][y] != LevelGenerator.HIGH_GRAS;           
    }

    public void setMowed(int x, int y)
    {
        if (level[x][y] == LevelGenerator.HIGH_GRAS)
        {
            level[x][y] = LevelGenerator.LOW_GRAS;
        }
    }

	public void setLevel(int[][] lvl){
		level = lvl;
	}

}
