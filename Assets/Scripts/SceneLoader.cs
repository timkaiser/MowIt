using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class loads screen and takes additional parameters for it
public class SceneLoader{

    //level parameter
    private static int level;
    public static int Level {
        get { return level; }
    }

    //Load scene with parameter
    public static void LoadScene(string name, int lvl) {
        level = lvl;
        GlobalVariables.reset();
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    //Load scene without parameter
    public static void LoadScene(string name) {
        LoadScene(name,0);
    }
}
