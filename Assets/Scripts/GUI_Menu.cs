using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI_Menu : MonoBehaviour {
    private static bool firstTimeLoading = true;

    //PREFAB UI ELEMENTS
    private GameObject start_overlay;
    private GameObject credits_overlay;

    //STATES
    public short state = 0;
    private const int MAINMENU = 0;
    private const int CREDITS = 1;
    private const int LEVELMENU = 2;

    void Start() {
        if (!firstTimeLoading) {
            state = LEVELMENU;
        }
        firstTimeLoading = false;
        
        //Start button
        start_overlay = Instantiate((GameObject)Resources.Load("Prefabs/UI/StartOverlay"));
        start_overlay.GetComponentInChildren<Button>().onClick.AddListener(start);

        //Credits
        credits_overlay = Instantiate((GameObject)Resources.Load("Prefabs/UI/CreditsOverlay"));
        credits_overlay.SetActive(false);

    }

    void OnGUI(){
        //Hide Background for all Buttons
        GUI.backgroundColor = Color.clear;
        //Back Button
        int buttonsize = (int)Mathf.Max(Screen.height / 10, Screen.width / 10);
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"))) {
            back();
        }

        //Hide prefab Buttons not on screen
        //Mainmenu
         start_overlay.SetActive(state == MAINMENU);
        //Credits
        credits_overlay.SetActive(state == CREDITS);

        switch (state) {
            case MAINMENU:
                //Credits Button
                GUIStyle btnStyle_Credits = new GUIStyle(GUI.skin.button);
                btnStyle_Credits.fontSize = (int)(0.75 * buttonsize);
                if (GUI.Button(new Rect(Screen.width - buttonsize, 0, buttonsize, buttonsize), "i", btnStyle_Credits)) {
                    state = CREDITS;
                }
                break;
            case LEVELMENU:
                //Level selection
                int upperCorner_x = 2 * Screen.width / 10;
                int upperCorner_y = 1 * Screen.height / 10;

                int bottomCorner_x = 8 * Screen.width / 10;

                int button_width = (int)(1.5 * Screen.width / 10);
                int button_height = 1 * Screen.height / 10;

                int buttons_per_row = 3;
                int distance_between_rows = (int)(0.5f * Screen.width / 10);

                GUI.backgroundColor = Color.white;
                GUIStyle btnStyle_LevelMenue= new GUIStyle(GUI.skin.button);
                btnStyle_LevelMenue.fontSize = (int)(0.5 * buttonsize);
                for (int i = 0; i < 6; i++) {
                    int position_in_row = i % buttons_per_row;
                    int position_int_column = (int)(i / buttons_per_row);

                    int x = upperCorner_x + (bottomCorner_x - upperCorner_x) / buttons_per_row * position_in_row + (bottomCorner_x - upperCorner_x) / buttons_per_row / 2 - button_width / 2;
                    int y = upperCorner_y + (button_height + distance_between_rows) * position_int_column - button_height / 2;

                    //Debug.Log("i: " + (i+1) + "; width: " + Screen.width + "; height: " + Screen.height + "; x: " + x + "; y: " + y + "; x%: " + (float)x/Screen.width + "; y%: " + (float)y /Screen.height);

                    if (GUI.Button(new Rect(x, y, button_width, button_height), "" + (i + 1), btnStyle_LevelMenue)) {
                        Instantiate(Resources.Load("Prefabs/UI/LoadingOverlay"));
                        SceneLoader.LoadScene("Level", i + 1);
                    }
                }
                GUI.backgroundColor = Color.clear;
                break;
        }   

    }

    //What happens wenn start button is pressed
    public void start() {
        state = LEVELMENU;
    }

    //What happens when backbutton is pressed
    public void back() {
        if(state == MAINMENU) {
            Application.Quit();
        } else {
            state = MAINMENU;
        }
        
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            back();
        }
    }
}