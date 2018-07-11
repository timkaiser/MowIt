using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    // position and orientation
    public int x, y, rotation;

    //Gamestate
    private static short gamestate = RUNNING;
    public static short Gamestate { get { return gamestate; } }
    public const int RUNNING = 0;
    public const int WON = 1;
    public const int LOST = 2;
    
    //UI
    private Button button_up, button_down, button_left, button_right;
    public Button prefab_button;
    private GameObject ui_canvas;
    float buttonsize = 92;

    //size of lawnmower (for vibration animation)
    private float scale = 1;

    //sounf
    private AudioSource soundeffect;
    
    // Use this for initialization
    void Start () {
        //set game to running at start
        gamestate = RUNNING;

        //get sound
        soundeffect = this.GetComponentInChildren<AudioSource>();
        
        //UI
        ui_canvas = GameObject.FindWithTag("canvas");

        //Instantiate button up
        button_up = Instantiate(prefab_button);
        button_up.transform.SetParent(ui_canvas.transform);
        button_up.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        button_up.onClick.AddListener(up);

        //Instantiate button down
        button_down = Instantiate(prefab_button);
		button_down.transform.SetParent(ui_canvas.transform);
        button_down.GetComponent<Transform>().Rotate(new Vector3(0, 0, 270));
        button_down.onClick.AddListener(down);

        //Instantiate button left
        button_left = Instantiate(prefab_button);
		button_left.transform.SetParent(ui_canvas.transform);
        button_left.GetComponent<Transform>().Rotate(new Vector3(0, 0, 180));
        button_left.onClick.AddListener(left);

        //Instantiate button right
        button_right = Instantiate(prefab_button);
		button_right.transform.SetParent(ui_canvas.transform);
        button_right.GetComponent<Transform>().Rotate(new Vector3(0, 0, 0));
        button_right.onClick.AddListener(right);

        //deactivate all movement buttons at beginning
        button_up.gameObject.SetActive(false);
        button_down.gameObject.SetActive(false);
        button_left.gameObject.SetActive(false);
        button_right.gameObject.SetActive(false);

        //activate only buttons for possible moves
        if (isFree(x, y + 1)) {
            button_up.gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
		if (isFree(x, y - 1)) {
            button_down.gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 180));
        }
		if (isFree(x - 1, y)) {
            button_left.gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 270));
        }
		if (isFree(x + 1, y)) {
            button_right.gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        //main game
        if (gamestate == RUNNING) {
            //vibration animation
            scale = (scale == 1) ? 1.02f : 1;
            transform.localScale = new Vector3(scale, scale, 1);

            //if lawnmower is moving
            if (transform.position.x != x || transform.position.y != y) {
                //increase noise while moving
                soundeffect.volume = 0.5f;

                //deactivate buttons while moving
                button_up.gameObject.SetActive(false);
                button_down.gameObject.SetActive(false);
                button_left.gameObject.SetActive(false);
                button_right.gameObject.SetActive(false);
                
                //xDif, yDif elementOf {-0.1, 0 ,0.1} => movement direction and speed
                float xDif = 0.1f * ((x - transform.position.x) < 0 ? -1 : (x - transform.position.x) == 0 ? 0 : 1);
                float yDif = 0.1f * ((y - transform.position.y) < 0 ? -1 : (y - transform.position.y) == 0 ? 0 : 1);
                //new position
                float xNew = Mathf.Round((transform.position.x + xDif) * 1000) / 1000f;
                float yNew = Mathf.Round((transform.position.y + yDif) * 1000) / 1000f;
                transform.position = new Vector3(xNew, yNew, 0);

                //set gras under lawnmower to mowed
                GlobalVariables.setMowed((int)xNew + (int)(GlobalVariables.Level.Length / 2), (int)yNew + (int)(GlobalVariables.Level[0].Length / 2));

                //start losescreen if flowers are hit
                if (GlobalVariables.Level[(int)xNew + (int)(GlobalVariables.Level.Length / 2)][(int)yNew + (int)(GlobalVariables.Level[0].Length / 2)] == LevelGenerator.FLOWER) {
                soundeffect.volume = 0.2f;
                gamestate = LOST;
            }
        //if not moving
        } else {
                //Keyboard Control
                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    left();
                } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    right();
                } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    up();
                } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                    down();
                }

                //idecrease noise while moving
                soundeffect.volume = 0.2f;

                //activate buttons while not moving
                if (isFree(x - 1, y)) { button_left.gameObject.SetActive(true); }
                if (isFree(x + 1, y)) { button_right.gameObject.SetActive(true); }
                if (isFree(x, y - 1)) { button_down.gameObject.SetActive(true); }
                if (isFree(x, y + 1)) { button_up.gameObject.SetActive(true); }

                //start winscreen if everything is mowed
                if (GlobalVariables.isMowed()) {
                    GlobalVariables.UnlockedLevels[GlobalVariables.LevelIndex-1] = GlobalVariables.RoundCounter;
                    SaveManagement.SaveLevels();
                    soundeffect.volume = 0.2f;
                    gamestate = WON;
                }
            }

            //mute sound if wanted
            //soundeffect.mute = GlobalVariables.isMuted;

            //resize buttons
            buttonsize = (Screen.height / 1000f);

            button_up.transform.localScale = new Vector3(buttonsize, buttonsize, 1);
            button_down.transform.localScale = new Vector3(buttonsize, buttonsize, 1);
            button_left.transform.localScale = new Vector3(buttonsize, buttonsize, 1);
            button_right.transform.localScale = new Vector3(buttonsize, buttonsize, 1);

            //set new positon for buttons
            button_up.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x, y * buttonsize * 100 + ui_canvas.transform.position.y + buttonsize * 100, 0);
            button_down.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x, y * buttonsize * 100 + ui_canvas.transform.position.y - buttonsize * 100, 0);
            button_left.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x - buttonsize * 100, y * buttonsize * 100 + ui_canvas.transform.position.y, 0);
            button_right.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x + buttonsize * 100, y * buttonsize * 100 + ui_canvas.transform.position.y, 0);

        }

    }

    //test if player can move over tile (this inclueds flowers)
    public bool isFree(int x, int y){
        x = x + (int)(GlobalVariables.Level.Length / 2);
        y = y + (int)(GlobalVariables.Level[0].Length / 2);
        return !(x<0 || y<0 || x>= GlobalVariables.Level.Length || y >= GlobalVariables.Level[0].Length) &&
			GlobalVariables.Level[x][y] != LevelGenerator.TREE &&
			GlobalVariables.Level[x][y] != LevelGenerator.EMPTY;
    }

    //move player in direction (h,v); h,v elementOf{-1,0,1}
    public void move(int h, int v) {
        GlobalVariables.RoundCounter++;
        while (isFree(x + h, y + v)) {
            x+=h;
            y+=v;
        }
    }
    
    //methods for movement in all four directions
    private void up(){
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        move(0, 1);
    }
    private void down(){
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 180));
        move(0, -1);
    }
    private void left(){
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 270));
        move(-1, 0);
    }
    private void right(){
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
        move(1, 0);
    }

    //set position
    public void setPosition(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
