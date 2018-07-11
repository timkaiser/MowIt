using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI_Game : MonoBehaviour 
{
    //UI elements for roundcounter, winscreen and losescreen
    private Text roundcounter;
    public GameObject winoverlay;
    public GameObject loseoverlay;

    //Initialization
    private void Start() {
        //load prefabs for UI
        roundcounter = Instantiate((GameObject)Resources.Load("Prefabs/UI/GameOverlay")).GetComponentInChildren<Text>();

        winoverlay = Instantiate((GameObject)Resources.Load("Prefabs/UI/WinOverlay_" + GlobalVariables.language));
        winoverlay.SetActive(false);

        loseoverlay = Instantiate((GameObject)Resources.Load("Prefabs/UI/LoseOverlay_" + GlobalVariables.language));
        winoverlay.SetActive(false);
    }

    //GUI
    void OnGUI(){   
        //Back Button
        GUI.backgroundColor = Color.clear;
        int buttonsize = (int)Mathf.Max(Screen.height / 10, Screen.width / 10);
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"))) {
            back();
        }

        //Reset Button
        if (GUI.Button(new Rect((Screen.width-buttonsize)/2, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_replay"))) {
            restartLevel();
        }

        //Winscreen
        if (Player.Gamestate == Player.WON) {
            int x = (int)((Screen.width - buttonsize) / 2);
            int y = (int)(0.6 * Screen.height);
            if (GlobalVariables.LevelIndex < LevelGenerator.NUMBER_OF_LEVELS) {
                if (GUI.Button(new Rect(x, y, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_forward"))) {
                    nextLevel();
                }
            } else {
                if (GUI.Button(new Rect(x, y, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"))) {
                    back();
                }
            }
        }

        //Losescreen
        if (Player.Gamestate == Player.LOST) {
            int x = (int)((Screen.width - buttonsize) / 2);
            int y = (int)(0.6 * Screen.height);
            if (GUI.Button(new Rect(x, y, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_replay"))) {
                restartLevel();
            }
        }
    }

    private void Update() {
        //winscreen
        winoverlay.SetActive(Player.Gamestate == Player.WON);

        //losescreen
        loseoverlay.SetActive(Player.Gamestate == Player.LOST);

        //Keyboard input (ESC = back)
        if (Input.GetKey(KeyCode.Escape)) {
            back();
        }

        //roundcounter
        if (roundcounter != null){
            roundcounter.text = "" + GlobalVariables.RoundCounter;
        }

        foreach (AudioSource audio in GameObject.FindObjectsOfType<AudioSource>()) {
            audio.mute = GlobalVariables.isMuted;
        }
        

    }

    //back to mainmenu
    public void back() {
        //Instantiate((GameObject)Resources.Load("Prefabs/UI/LoadingOverlay"));
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    //to next level
    public void nextLevel() {
        if(GlobalVariables.LevelIndex == LevelGenerator.NUMBER_OF_LEVELS) { return; }
        winoverlay.SetActive(false);
        //Instantiate(Resources.Load("Prefabs/UI/LoadingOverlay"));
        SceneLoader.LoadScene("Level", (GlobalVariables.LevelIndex + 1)); 
    }

    //restart level
    public void restartLevel() {
        loseoverlay.SetActive(false);
        //Instantiate(Resources.Load("Prefabs/UI/LoadingOverlay"));
        SceneLoader.LoadScene("Level", (GlobalVariables.LevelIndex));
    }
}