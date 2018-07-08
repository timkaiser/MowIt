using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public const int HIGH_GRAS  = 0;
    public const int LOW_GRAS = 1;
    public const int PLAYER = 2;
    public const int TREE   = 3;
	public const int FLOWER = 4;
	public const int EMPTY  = 5;

    public static string language = "DE";

    private GameObject prefab_floor;
    private GameObject prefab_tree;
    private GameObject prefab_flower;
    private GameObject prefab_player;

    // Use this for initialization
    void Start() {
        prefab_floor = (GameObject)Resources.Load("Prefabs/Floortile"); ;
        prefab_tree = (GameObject)Resources.Load("Prefabs/Tree");
        prefab_flower = (GameObject)Resources.Load("Prefabs/Flower");
        prefab_player = (GameObject)Resources.Load("Prefabs/Player");

    int[][] level = getLevel(SceneLoader.Level);
        GlobalVariables.Level = level;
        generate(level);
        switch (SceneLoader.Level) {
            case 1:
                Instantiate(Resources.Load("Prefabs/UI/TutorialOverlay_1_" + language));
                break;
            case 2:
                Instantiate(Resources.Load("Prefabs/UI/TutorialOverlay_2_" + language));
                break;
            case 3:
                Instantiate(Resources.Load("Prefabs/UI/TutorialOverlay_3_" + language));
                break;
        }
	}
	
    void generate(int[][] level) {
        for (int i = 0; i < level.Length; i++){
            for (int j = 0; j < level[i].Length; j++)
            {	
				if (level [i] [j] != EMPTY) { 
					Gras g = Instantiate (prefab_floor, new Vector3 (i - ((int)(level.Length / 2)), j - ((int)(level [i].Length / 2)), 1), Quaternion.identity).GetComponent<Gras> ();
					g.setPosition (i, j);
				}
                switch (level[i][j])
                {
                    case PLAYER:
                        Player p = Instantiate(prefab_player, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), 0), Quaternion.identity).GetComponent<Player>();
                        p.setPosition(i - ((int)(level.Length / 2)), j - ((int)(level[i].Length / 2)));
                        break;
                    case TREE:
                        Instantiate(prefab_tree, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), -1), Quaternion.identity);
                        break;
                    case FLOWER:
                        Instantiate(prefab_flower, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), 0.5f), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private int[][] getLevel(int i) {
        switch (i) {
            case 1:
                return new[] {
                        new[]{ 2 },
                        new[]{ 0 },
                        new[]{ 0 },
                        new[]{ 0 },
                        new[]{ 0 }
                    };
            case 2:
                return new[] {
                        new[]{ 2, 0 },
                        new[]{ 0, 0 }
                    };
            case 3:
                return new[]{
                        new[]{ 2 , 3},
                        new[]{ 0 , 0},
                        new[]{ 0 , 0},
                        new[]{ 3 , 0},
                        new[]{ 0 , 0}
                    };
            case 4:
                return new[] {
                        new[]{ 3, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 3 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 3, 0 },
                        new[]{ 3, 0, 0, 0, 2 }
                    };
            case 5:
               return new[] {
                        new[]{ 3, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 3 },
                        new[]{ 0, 3, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 2 }
                    };
            case 6:
                return new[] {
                        new[]{ 2, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 3 },
                        new[]{ 3, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            default:
                return new[]{
                    new[]{ 2 , 0, 0},
                    new[]{ 0 , 3, 0},
                    new[]{ 0 , 0, 0}
                };

        }
            
    }
}

