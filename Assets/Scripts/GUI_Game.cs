using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI_Game : MonoBehaviour 
{
    private Text roundcounter;
    private void Start() {
        roundcounter = Instantiate((GameObject)Resources.Load("Prefabs/UI/GameOverlay")).GetComponentInChildren<Text>();
    }
    void OnGUI(){   
        //Back Button
        GUI.backgroundColor = Color.clear;
        int buttonsize = (int)Mathf.Max(Screen.height / 10, Screen.width / 10);
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"), GUI_MainMenu.BtnStyle_Back)) {
            SceneManager.LoadScene("LevelMenu", LoadSceneMode.Single);
        }

    }

    private void Update(){
        //roundcounter.fontSize = 10;
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("LevelMenu", LoadSceneMode.Single);
        }

        if (roundcounter != null){
            roundcounter.text = "" + GlobalVariables.RoundCounter;
        }
    }
}