using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private static int level;
    public static int Level {
        get { return level; }
    }

    public static void LoadScene(string name, int lvl) {
        level = lvl;
        GlobalVariables.reset();
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public static void LoadScene(string name) {
        LoadScene(name,0);
    }
}
