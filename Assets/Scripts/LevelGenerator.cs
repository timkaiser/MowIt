using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public const int HIGH_GRAS  = 0;
    public const int LOW_GRAS = 1;
    public const int PLAYER = 2;
    public const int TREE   = 3;
    public const int FLOWER = 4;

    public GameObject prefab_floor;
    public GameObject prefab_player;
    public GameObject prefab_tree;
    public GameObject prefab_flower;
    
    // Use this for initialization
    void Start() {
        generate(GlobalVariables.get().level);
	}
	
    void generate(int[][] level)
    {
        for (int i = 0; i < level.Length; i++){
            for (int j = 0; j < level[i].Length; j++)
            {
                Gras g = Instantiate(prefab_floor, new Vector3(i - ((int)(level.Length / 2)), j - ((int)(level[i].Length / 2)), 1), Quaternion.identity).GetComponent<Gras>();
                g.setPosition(i, j);
                switch (level[i][j])
                {
                    case PLAYER:
                        Player p = Instantiate(prefab_player, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), 0), Quaternion.identity).GetComponent<Player>();
                        p.setPosition(i - ((int)(level.Length / 2)), j - ((int)(level[i].Length / 2)));
                        break;
                    case TREE:
                        Instantiate(prefab_tree, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), 0), Quaternion.identity);
                        break;
                    case FLOWER:
                        Instantiate(prefab_flower, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), 0), Quaternion.identity);
                        break;
                }
            }
        }
    }
}
