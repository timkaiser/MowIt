using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    //Constants for level object indices
    public const int HIGH_GRAS  = 0, H = 0;
    public const int LOW_GRAS = 1, L = 1;
    public const int PLAYER = 2, P = 2;
    public const int TREE   = 3, T = 3;
	public const int FLOWER = 4, F = 4;
	public const int EMPTY  = 5, E = 5;

    //Game object prefabs
    private GameObject prefab_floor;
    private GameObject prefab_tree;
    private GameObject prefab_flower;
    private GameObject prefab_player;

    //Number of levels in the game
    public const int NUMBER_OF_LEVELS = 21;

    //initialization
    void Start() {
        //load prefabs
        prefab_floor = (GameObject)Resources.Load("Prefabs/Floortile"); ;
        prefab_tree = (GameObject)Resources.Load("Prefabs/Tree");
        prefab_flower = (GameObject)Resources.Load("Prefabs/Flower");
        prefab_player = (GameObject)Resources.Load("Prefabs/Player");

        //load level
        GlobalVariables.LevelIndex = SceneLoader.Level;
        int[][] level = getLevel(GlobalVariables.LevelIndex);
        GlobalVariables.Level = level;
        generate(level);

        //load tutorial if necessary
        switch (SceneLoader.Level) {
            case 1:
                Instantiate(Resources.Load("Prefabs/UI/TutorialOverlay_1_" + GlobalVariables.language));
                break;
            case 2:
                Instantiate(Resources.Load("Prefabs/UI/TutorialOverlay_2_" + GlobalVariables.language));
                break;
            case 3:
                Instantiate(Resources.Load("Prefabs/UI/TutorialOverlay_3_" + GlobalVariables.language));
                break;
            case 4:
                Instantiate(Resources.Load("Prefabs/UI/TutorialOverlay_4_" + GlobalVariables.language));
                break;
        }
	}
	
    //generates level as safed in GlobalVariables.Level
    void generate(int[][] level) {
        //iterate level[][]
        for (int i = 0; i < level.Length; i++){
            for (int j = 0; j < level[i].Length; j++) {
                //Instatiate gras
                if (level [i] [j] != EMPTY) { 
					Gras g = Instantiate (prefab_floor, new Vector3 (i - ((int)(level.Length / 2)), j - ((int)(level [i].Length / 2)), 1), Quaternion.identity).GetComponent<Gras> ();
					g.setPosition (i, j);
				}

                //Instatiate rest of gameobjects
                switch (level[i][j])
                {
                    //Instatiate player
                    case PLAYER:
                        Player p = Instantiate(prefab_player, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), 0), Quaternion.identity).GetComponent<Player>();
                        p.setPosition(i - ((int)(level.Length / 2)), j - ((int)(level[i].Length / 2)));
                        break;
                    //Instatiate trees
                    case TREE:
                        Instantiate(prefab_tree, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), -1), Quaternion.identity);
                        break;
                    //Instatiate flowers
                    case FLOWER:
                        Instantiate(prefab_flower, new Vector3(i - ((int)(level.Length / 2)), j-((int)(level[i].Length / 2)), 0.5f), Quaternion.identity);
                        break;
                }
            }
        }
    }

    //returns level (TODO: move to external file)
    private int[][] getLevel(int i) {
        switch (i) {
            //Tutorial
            case 1:
                return new[] {
                        new[]{ P },
                        new[]{ 0 },
                        new[]{ 0 },
                        new[]{ 0 },
                        new[]{ 0 }
                    };
            case 2:
                return new[] {
                        new[]{ P, 0 },
                        new[]{ 0, 0 }
                    };
            case 3:
                return new[]{
                        new[]{ P , T},
                        new[]{ 0 , 0},
                        new[]{ 0 , 0},
                        new[]{ T , 0},
                        new[]{ 0 , 0}
                    };
            case 4:
                return new[] {
                        new[]{ 0, P },
                        new[]{ F, 0 },
                        new[]{ 0, 0 },
                    };
            //Main game
            case 5:
                return new[] {
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, T },
                        new[]{ 0, T, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, P }
                    };
            case 6:
                return new[] {
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ T, F, F, F, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ P, 0, 0, 0, 0 }
                    };
            case 7:
                return new[] {
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, F, 0, 0 },
                        new[]{ 0, 0, 0, P, 0 },
                        new[]{ 0, 0, 0, 0, T }
                    };
            case 8:
                return new[] {
                        new[]{ P, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, T },
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            case 9:
                return new[] {
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, T },
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, P },
                        new[]{ 0, 0, 0, 0, T }
                    };
            case 10:
                return new[] {
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, F, 0, 0 },
                        new[]{ 0, 0, T, 0, 0 },
                        new[]{ 0, 0, F, 0, 0 },
                        new[]{ P, 0, 0, 0, T }
                    };
            case 11:
                return new[] {
                        new[]{ 0, 0, T, 0, 0 },
                        new[]{ 0, P, F, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, F, F, T, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            case 12:
                return new[] {
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, T },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, T, 0 },
                        new[]{ T, 0, 0, 0, P }
                    };
            case 13:
                return new[] {
                        new[]{ 0, 0, T, 0, 0 },
                        new[]{ 0, 0, F, 0, 0 },
                        new[]{ 0, 0, P, 0, 0 },
                        new[]{ 0, 0, F, 0, T },
                        new[]{ 0, 0, T, 0, 0 }
                    };
            case 14:
                return new[] {
                        new[]{ 0, 0, T, 0, 0 },
                        new[]{ 0, P, T, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, T, T, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            case 15:
                return new[] {
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ F, 0, P, 0, T },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            case 16:
                return new[] {
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, P, T },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ T, 0, 0, 0, 0 }
                    };
            case 17:
                return new[] {
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, F, F, T },
                        new[]{ 0, P, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            case 18:
                return new[] {
                        new[]{ T, 0, 0, 0, 0 },
                        new[]{ 0, 0, T, 0, 0 },
                        new[]{ 0, 0, T, 0, T },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ P, 0, 0, 0, 0 }
                    };
            case 19:
                return new[] {
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, F, T, F, 0 },
                        new[]{ 0, 0, P, 0, 0 },
                        new[]{ 0, F, T, F, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            case 20:
                return new[] {
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, P, T },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 }
                    };
            case 21:
                return new[] {
                        new[]{ 0, 0, T, 0, 0 },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ T, 0, P, 0, T },
                        new[]{ 0, 0, 0, 0, 0 },
                        new[]{ 0, 0, T, 0, 0 }
                    };
            default:
                return new[]{
                    new[]{ P , 0, F, 0, 0},
                    new[]{ 0 , 0, 0, 0, 0},
                    new[]{ 0 , 0, 0, 0, T}
                };

        }
            
    }
}

