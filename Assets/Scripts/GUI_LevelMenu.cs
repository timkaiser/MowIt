using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_LevelMenu : MonoBehaviour {
    void OnGUI() {
        //Level selection
        int upperCorner_x = 2 * Screen.width / 10;
        int upperCorner_y = 1 * Screen.height / 10;

        int bottomCorner_x = 8 * Screen.width / 10;
        //int bottomCorner_y = 8 * Screen.height / 10;

        int button_width = (int)(1.5 * Screen.width / 10);
        int button_height = 1 * Screen.height / 10;

        int buttons_per_row = 3;
        int distance_between_rows = (int)(0.5f * Screen.width / 10);

        for (int i = 0; i < 6; i++) {
            int position_in_row = i % buttons_per_row;
            int position_int_column = (int)(i / buttons_per_row);

            int x = upperCorner_x + (bottomCorner_x - upperCorner_x) / buttons_per_row * position_in_row + (bottomCorner_x - upperCorner_x) / buttons_per_row / 2 - button_width / 2;
            int y = upperCorner_y + (button_height + distance_between_rows) * position_int_column - button_height / 2;

            //Debug.Log("i: " + (i+1) + "; width: " + Screen.width + "; height: " + Screen.height + "; x: " + x + "; y: " + y + "; x%: " + (float)x/Screen.width + "; y%: " + (float)y /Screen.height);

            if (GUI.Button(new Rect(x, y, button_width, button_height), "" + (i + 1), GUI_MainMenu.BtnStyle)) {
                Instantiate(Resources.Load("Prefabs/UI/LoadingOverlay"));
                SceneLoader.LoadScene("Level", i + 1);
            }
        }

        //Back Button
        GUI.backgroundColor = Color.clear;
        int buttonsize = (int)Mathf.Max(Screen.height / 10, Screen.width / 10);
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture)Resources.Load("Sprites/Arrow_back"), GUI_MainMenu.BtnStyle_Back)) {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}