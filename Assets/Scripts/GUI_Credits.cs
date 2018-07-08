using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Credits : MonoBehaviour {
    void OnGUI() {    
        //Back Button
        GUI.backgroundColor = Color.clear;
        int buttonsize = (int)Mathf.Max(Screen.height / 10, Screen.width / 10);
        if (GUI.Button(new Rect(0, 0, buttonsize, buttonsize), (Texture) Resources.Load("Sprites/Arrow_back"), GUI_MainMenu.BtnStyle_Back)) {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
