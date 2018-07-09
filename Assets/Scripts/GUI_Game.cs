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
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"))) {
            back();
        }

    }

    private void Update(){
        //roundcounter.fontSize = 10;
        if (Input.GetKey(KeyCode.Escape)) {
            back();
        }

        if (roundcounter != null){
            roundcounter.text = "" + GlobalVariables.RoundCounter;
        }
    }

    //back to mainmenu
    public void back() {
        Instantiate((GameObject)Resources.Load("Prefabs/UI/LoadingOverlay"));
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}