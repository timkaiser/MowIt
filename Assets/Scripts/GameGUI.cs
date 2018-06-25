using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour 
{
	void OnGUI()
	{

		//Style for buttons
		GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
		//custom font
		Font sa = (Font)Resources.Load("Fonts/space_age/space_age", typeof(Font));
		GUI.skin.font = sa;
		btnStyle.fontSize = 45;
		btnStyle.normal.textColor = Color.white;

		GUI.contentColor = Color.cyan;
		Color newClr = new Color (0,0,1,1.0f);
		GUI.backgroundColor = newClr;      

		//Make sure the current scene is the main menu
			// "Quit game" button
		if (GUI.Button(new Rect(Screen.width / 2 - Screen.width/6, 9*Screen.height/10, Screen.width/3, Screen.height/10), "Back",btnStyle))
		{

			SceneManager.LoadScene("LevelMenu", LoadSceneMode.Single);
		}
			
	}
}