using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI_Menu : MonoBehaviour {
    //boolean to see if this is the first start (for game back buttons)
    private static bool firstTimeLoading = true;

    //PREFAB UI ELEMENTS
    private GameObject start_overlay;
    private GameObject credits_overlay;

    //STATES
    public short state = 0;
    private const int MAINMENU = 0;
    private const int CREDITS = 1;
    private const int LEVELMENU = 2;

    //level menu page
    private static int current_page = 0;

    //Initialization
    void Start() {
        //load settings and levels
        SaveManagement.LoadSettings();
        SaveManagement.LoadLevels();

        //If this is not the first time loading start at level menu
        if (!firstTimeLoading) {
            state = LEVELMENU;
        }
        firstTimeLoading = false;
        
        //Start button overlay
        start_overlay = Instantiate((GameObject)Resources.Load("Prefabs/UI/StartOverlay"));
        start_overlay.GetComponentInChildren<Button>().onClick.AddListener(start);

        //Credits overlay
        credits_overlay = Instantiate((GameObject)Resources.Load("Prefabs/UI/CreditsOverlay"));
        credits_overlay.SetActive(false);

    }

    //GUI
    void OnGUI(){
        //Hide Background for all Buttons
        GUI.backgroundColor = Color.clear;

        //Back Button
        int buttonsize = (int)Mathf.Max(Screen.height / 10, Screen.width / 10);
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"))) {
            back();
        }
        
        //Mainmenu
         start_overlay.SetActive(state == MAINMENU);
        //Credits
        credits_overlay.SetActive(state == CREDITS);

        switch (state) {
            //Mainmenu
            case MAINMENU:
                //Credits Button
                GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
                btnStyle.font = (Font)Resources.Load("Fonts/MostlyMono");
                btnStyle.fontSize = (int)(1 * buttonsize);
                if (GUI.Button(new Rect(Screen.width - buttonsize, 0, buttonsize, buttonsize), "i", btnStyle)) {
                    state = CREDITS;
                }

                //Sound settings
                if (GUI.Button(new Rect(Screen.width - buttonsize, (int)(0.97 * Screen.height) - 2*buttonsize, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/"+ (GlobalVariables.isMuted?"Icon_Muted":"Icon_NotMuted")))) {
                    GlobalVariables.isMuted = !GlobalVariables.isMuted;
                    SaveManagement.SaveSettings();
                }
                //Langugae settings
                btnStyle.fontSize = (int)(0.75 * buttonsize);
                if (GUI.Button(new Rect(Screen.width - buttonsize, (int)(0.97 * Screen.height) - buttonsize, buttonsize, buttonsize), GlobalVariables.language, btnStyle)) {
                    GlobalVariables.language = (GlobalVariables.language.Equals("EN") ? "DE" : "EN");
                    SaveManagement.SaveSettings();
                }
                break;
            //Levelmenu
            case LEVELMENU:
                //Parameters for the Level selection panel
                int upperCorner_x = (int)(1.5f * Screen.width / 10);
                int upperCorner_y = (int)(1 * Screen.height / 10);

                int bottomCorner_x = (int)(8.5f * Screen.width / 10);
                int bottomCorner_y = (int)(7 * Screen.height / 10);

                int button_width = (int)(1.8 * Screen.width / 10);
                int button_height = 1 * Screen.height / 10;

                int buttons_per_row = 3;
                int rows = 5;

                int buttons_per_page = buttons_per_row * rows;
                int pages = (LevelGenerator.NUMBER_OF_LEVELS + buttons_per_page - 1) / buttons_per_page;

                //Button style for the Level selection panel
                GUI.skin = (GUISkin)Resources.Load("LevelButtons");
                GUIStyle btnStyle_LevelMenue= new GUIStyle(GUI.skin.button);
                btnStyle_LevelMenue.font = (Font)Resources.Load("Fonts/MostlyMono");
                btnStyle_LevelMenue.fontSize = (int)(0.5 * buttonsize);

                //Initilize Level selection panel
                for (int i = buttons_per_page * current_page; i < buttons_per_page * (current_page + 1) && i < LevelGenerator.NUMBER_OF_LEVELS; i++) {
                    //select color
                    //finished
                    if (GlobalVariables.UnlockedLevels[i] > 0) {
                        GUI.backgroundColor = new Color(1, 0, 0, 0.5f);
                        GUI.contentColor = new Color(1, 1, 1, 1);
                        //unlocked
                    } else if (i == 0 || (GlobalVariables.UnlockedLevels[i-1] > 0) || (i>1 && GlobalVariables.UnlockedLevels[i - 2] > 0) || (i > 3 && GlobalVariables.UnlockedLevels[i - 3] > 0)) { 
                        GUI.backgroundColor = Color.white;
                        GUI.contentColor = new Color(0, 0, 0, 1);
                        //locked
                    } else {
                        GUI.backgroundColor = new Color(0, 0, 0, 0.5f);
                        GUI.contentColor = new Color(0, 0, 0, 1);
                    }

                    int position_in_row = i % buttons_per_row;
                    int position_int_column = (int)((i% buttons_per_page) / buttons_per_row);

                    int x = upperCorner_x + (bottomCorner_x - upperCorner_x) / buttons_per_row * position_in_row + (bottomCorner_x - upperCorner_x) / buttons_per_row / 2 - button_width / 2;
                    int y = upperCorner_y + (bottomCorner_y - upperCorner_y) / rows * position_int_column + (bottomCorner_y - upperCorner_y) / rows / 2 - button_height / 2;
                    //int y = upperCorner_y + (button_height + distance_between_rows) * position_int_column - button_height / 2;

                    if (GUI.Button(new Rect(x, y, button_width, button_height), "" + (i + 1), btnStyle_LevelMenue)) {
                        if ((i == 0 || GlobalVariables.UnlockedLevels[i - 1] > 0) || (i > 1 && GlobalVariables.UnlockedLevels[i - 2] > 0) || (i > 3 && GlobalVariables.UnlockedLevels[i - 3] > 0)) {
                            Instantiate(Resources.Load("Prefabs/UI/LoadingOverlay_" + GlobalVariables.language));
                            SceneLoader.LoadScene("Level", i + 1);
                        }
                    }
                }

                //Next page button
                GUI.backgroundColor = Color.clear;
                GUI.contentColor = Color.white;
                btnStyle_LevelMenue.fontSize = (int)(0.75 * buttonsize);
                if (current_page < pages-1) {
                    if (GUI.Button(new Rect(bottomCorner_x - buttonsize, bottomCorner_y, button_width, button_height), ">", btnStyle_LevelMenue)) {
                        current_page++;
                    }
                }

                //Next page button
                if (current_page > 0) {
                    if (GUI.Button(new Rect(upperCorner_x, bottomCorner_y, button_width, button_height), "<", btnStyle_LevelMenue)) {
                        current_page--;
                    }
                }
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

    //Keyboard controls (ESC = back)
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            back();
        }
    }
}