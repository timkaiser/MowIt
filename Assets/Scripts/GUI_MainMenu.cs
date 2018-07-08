using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_MainMenu : MonoBehaviour 
{
    private static GUIStyle btnStyle;
    public static GUIStyle BtnStyle {
        get {
            if(btnStyle == null) {
                //Style for buttons
                btnStyle = new GUIStyle(GUI.skin.button);
                //custom font
                Font sa = (Font)Resources.Load("Fonts/MostlyMono", typeof(Font));
                GUI.skin.font = sa;
                btnStyle.fontSize = 45;
                btnStyle.normal.textColor = Color.white;

                GUI.contentColor = Color.cyan;
                Color newClr = new Color(0, 0, 1, 1.0f);
                GUI.backgroundColor = newClr;
            }
            return btnStyle;
        }
    }

    private static GUIStyle btnStyle_Back;
    public static GUIStyle BtnStyle_Back {
        get {
            if (btnStyle_Back == null) {
                //Style for buttons
                btnStyle_Back = new GUIStyle(GUI.skin.button);
                //custom font
                Font sa = (Font)Resources.Load("Fonts/MostlyMono", typeof(Font));
                GUI.skin.font = sa;
                btnStyle_Back.fontSize = 45;
                btnStyle_Back.normal.textColor = Color.white;

                GUI.contentColor = Color.white;
                GUI.backgroundColor = Color.clear;
            }
            return btnStyle_Back;
        }
    }

    void OnGUI(){
        GUI.backgroundColor = Color.clear;
        int buttonsize = (int)Mathf.Max(Screen.height / 10, Screen.width / 10);
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"), GUI_MainMenu.BtnStyle_Back)) {
            Application.Quit();
        }


        GUIStyle btnStyle_Credits = new GUIStyle(GUI.skin.button);
        btnStyle_Credits.fontSize = (int)(0.75*buttonsize);
        if (GUI.Button(new Rect(Screen.width-buttonsize, 0, buttonsize, buttonsize), "i", btnStyle_Credits)) {
            SceneManager.LoadScene("Credits");
        }

    }

    public void start() {
        SceneManager.LoadScene("LevelMenu");
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}