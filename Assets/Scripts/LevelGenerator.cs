using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public const int EMPTY  = 0;
    public const int PLAYER = 1;
    public const int TREE   = 2;
    public const int FLOWER = 3;

    public GameObject prefab_floor;
    public GameObject prefab_player;
    public GameObject prefab_tree;
    public GameObject prefab_flower;

    // Use this for initialization
    void Start() {
        int[][] level = new int[5][];
        int[] level0 = { 0, 0, 0, 0, 0 }; level[0] = level0;
        int[] level1 = { 0, 0, 2, 0, 0 }; level[1] = level1;
        int[] level2 = { 2, 0, 0, 0, 0 }; level[2] = level2;
        int[] level3 = { 0, 0, 0, 1, 0 }; level[3] = level3;
        int[] level4 = { 0, 3, 0, 0, 0 }; level[4] = level4;


        generate(level);
	}
	
    void generate(int[][] level)
    {
        for (int i = 0; i < level.Length; i++){
            for (int j = 0; j < level[i].Length; j++)
            {
                Instantiate(prefab_floor, new Vector3(i, j, 1), Quaternion.identity);
                switch (level[i][j])
                {
                    case PLAYER:
                        Instantiate(prefab_player, new Vector3(i, j, 0), Quaternion.identity);
                        break;
                    case TREE:
                        Instantiate(prefab_tree, new Vector3(i, j, 0), Quaternion.identity);
                        break;
                    case FLOWER:
                        Instantiate(prefab_flower, new Vector3(i, j, 0), Quaternion.identity);
                        break;
                }
            }
        }
    }
}
