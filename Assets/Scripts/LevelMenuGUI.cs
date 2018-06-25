using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuGUI : MonoBehaviour 
{
	int[][][] level;

	void Start(){
		level = new int[6][][];

		//level 1
		level [0] = new[] {
			new[]{ 2 },
			new[]{ 0 },
			new[]{ 0 },
			new[]{ 0 },
			new[]{ 0 }
		};
			
		level [1] = new[] {
			new[]{ 2, 0 },
			new[]{ 0, 0},
		};
		level[2] = new[]{
			new[]{ 2 , 3},
			new[]{ 0 , 0},
			new[]{ 0 , 0},
			new[]{ 3 , 0},
			new[]{ 0 , 0}
		};
		level [3] = new[] {
			new[]{ 3, 0, 0, 0, 0 },
			new[]{ 0, 0, 0, 0, 3 },
			new[]{ 0, 0, 0, 0, 0 },
			new[]{ 0, 0, 0, 3, 0 },
			new[]{ 3, 0, 0, 0, 2 }
		};
		level [4] = new[] {
			new[]{ 3, 0, 0, 0, 0 },
			new[]{ 0, 0, 0, 0, 0 },
			new[]{ 0, 0, 0, 0, 3 },
			new[]{ 0, 3, 0, 0, 0 },
			new[]{ 0, 0, 0, 0, 2 }
		};
		level [5] = new[] {
			new[]{ 2, 0, 0, 0, 0 },
			new[]{ 0, 0, 0, 0, 0 },
			new[]{ 0, 0, 0, 0, 3 },
			new[]{ 3, 0, 0, 0, 0 },
			new[]{ 0, 0, 0, 0, 0 }
		};
			


	}

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
		if (SceneManager.GetActiveScene().name == "LevelMenu")
		{
			// "Start game" button
			if(GUI.Button(new Rect(Screen.width /3 - 2*Screen.width/10, Screen.height/3 - 2 * Screen.height / 10, Screen.width/5, Screen.height/10), "1", btnStyle))

			{
				GlobalVariables.get ().level = level [0];
				SceneManager.LoadScene("Level", LoadSceneMode.Single);
			}
			if(GUI.Button(new Rect(Screen.width /2 - Screen.width/10, Screen.height/3 - 2 * Screen.height / 10, Screen.width/5, Screen.height/10), "2", btnStyle))

			{
				GlobalVariables.get ().level = level [1];
				SceneManager.LoadScene("Level", LoadSceneMode.Single);
			}
			if(GUI.Button(new Rect(2*Screen.width /3, Screen.height/3 - 2 * Screen.height / 10, Screen.width/5, Screen.height/10), "3", btnStyle))

			{
				GlobalVariables.get ().level = level [2];
				SceneManager.LoadScene("Level", LoadSceneMode.Single);
			}


			// "Start game" button
			if(GUI.Button(new Rect(Screen.width /3 - 2*Screen.width/10, Screen.height/3, Screen.width/5, Screen.height/10), "4", btnStyle))

			{
				GlobalVariables.get ().level = level [3];
				SceneManager.LoadScene("Level", LoadSceneMode.Single);
			}
			if(GUI.Button(new Rect(Screen.width /2 - Screen.width/10, Screen.height/3 , Screen.width/5, Screen.height/10), "5", btnStyle))

			{
				GlobalVariables.get ().level = level [4];
				SceneManager.LoadScene("Level", LoadSceneMode.Single);
			}
			if(GUI.Button(new Rect(2*Screen.width /3, Screen.height/3, Screen.width/5, Screen.height/10), "6", btnStyle))

			{
				GlobalVariables.get ().level = level [5];
				SceneManager.LoadScene("Level", LoadSceneMode.Single);
			}

			// "Quit game" button
			if (GUI.Button(new Rect(Screen.width / 2 - Screen.width/6, 7*Screen.height/10, Screen.width/3, Screen.height/10), "Back",btnStyle))
			{

				SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
			}

		}
	}
}