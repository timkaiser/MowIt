using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public int x, y, rotation;

    //Gamestate
    private short gamestate = RUNNING;
    private const int RUNNING = 0;
    private const int WON = 1;
    private const int LOST = 2;
    
    //UI
    private Button button_up, button_down, button_left, button_right;
    public Button prefab_button;
    private GameObject ui_canvas;
    float buttonsize = 92;
    private float scale = 1;
    public GameObject winoverlay;


    private AudioSource soundeffect;
    
    // Use this for initialization
    void Start () {
        soundeffect = this.GetComponentInChildren<AudioSource>();
        //UI
        winoverlay = Instantiate((GameObject)Resources.Load("Prefabs/UI/WinOverlay"));
        winoverlay.SetActive(false);

        ui_canvas = GameObject.FindWithTag("canvas");

        button_up = Instantiate(prefab_button);
        button_up.transform.SetParent(ui_canvas.transform);
        button_up.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        button_up.onClick.AddListener(up);

        button_down = Instantiate(prefab_button);
		button_down.transform.SetParent(ui_canvas.transform);
        button_down.GetComponent<Transform>().Rotate(new Vector3(0, 0, 270));
        button_down.onClick.AddListener(down);

        button_left = Instantiate(prefab_button);
		button_left.transform.SetParent(ui_canvas.transform);
        button_left.GetComponent<Transform>().Rotate(new Vector3(0, 0, 180));
        button_left.onClick.AddListener(left);

        button_right = Instantiate(prefab_button);
		button_right.transform.SetParent(ui_canvas.transform);
        button_right.GetComponent<Transform>().Rotate(new Vector3(0, 0, 0));
        button_right.onClick.AddListener(right);

        button_up.gameObject.SetActive(false);
        button_down.gameObject.SetActive(false);
        button_left.gameObject.SetActive(false);
        button_right.gameObject.SetActive(false);
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
        //winscreen
        winoverlay.SetActive(gamestate == WON);

        //main game
        switch (gamestate) {
            case RUNNING:
                scale = (scale == 1) ? 1.02f : 1;
                transform.localScale = new Vector3(scale, scale, 1);

                if (transform.position.x != x || transform.position.y != y) {
                    soundeffect.volume = 0.3f;

                    button_up.gameObject.SetActive(false);
                    button_down.gameObject.SetActive(false);
                    button_left.gameObject.SetActive(false);
                    button_right.gameObject.SetActive(false);

                    //xCurrent =
                    //transform.position = new Vector3(xCurrent, yCurrent, 0);
                    float xDif = 0.1f * ((x - transform.position.x) < 0 ? -1 : (x - transform.position.x) == 0 ? 0 : 1);
                    float yDif = 0.1f * ((y - transform.position.y) < 0 ? -1 : (y - transform.position.y) == 0 ? 0 : 1);
                    float xNew = Mathf.Round((transform.position.x + xDif) * 1000) / 1000f;
                    float yNew = Mathf.Round((transform.position.y + yDif) * 1000) / 1000f;
                    transform.position = new Vector3(xNew, yNew, 0);
                    GlobalVariables.setMowed((int)xNew + (int)(GlobalVariables.Level.Length / 2), (int)yNew + (int)(GlobalVariables.Level[0].Length / 2));
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

                    soundeffect.volume = 0.1f;

                    if (isFree(x - 1, y)) { button_left.gameObject.SetActive(true); }
                    if (isFree(x + 1, y)) { button_right.gameObject.SetActive(true); }
                    if (isFree(x, y - 1)) { button_down.gameObject.SetActive(true); }
                    if (isFree(x, y + 1)) { button_up.gameObject.SetActive(true); }
                }

                soundeffect.mute = GlobalVariables.Mute;


                buttonsize = (Screen.height / 1000f);


                button_up.transform.localScale = new Vector3(buttonsize, buttonsize, 1);
                button_down.transform.localScale = new Vector3(buttonsize, buttonsize, 1);
                button_left.transform.localScale = new Vector3(buttonsize, buttonsize, 1);
                button_right.transform.localScale = new Vector3(buttonsize, buttonsize, 1);


                button_up.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x, y * buttonsize * 100 + ui_canvas.transform.position.y + buttonsize * 100, 0);
                button_down.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x, y * buttonsize * 100 + ui_canvas.transform.position.y - buttonsize * 100, 0);
                button_left.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x - buttonsize * 100, y * buttonsize * 100 + ui_canvas.transform.position.y, 0);
                button_right.transform.position = new Vector3(x * buttonsize * 100 + ui_canvas.transform.position.x + buttonsize * 100, y * buttonsize * 100 + ui_canvas.transform.position.y, 0);

                if (GlobalVariables.isMowed()) {
                    gamestate = WON;
                }
                break;
        }

    }

    public bool isFree(int x, int y)
    {
        x = x + (int)(GlobalVariables.Level.Length / 2);
        y = y + (int)(GlobalVariables.Level[0].Length / 2);
        return !(x<0 || y<0 || x>= GlobalVariables.Level.Length || y >= GlobalVariables.Level[0].Length) &&
			GlobalVariables.Level[x][y] != LevelGenerator.TREE &&
			GlobalVariables.Level[x][y] != LevelGenerator.EMPTY;
    }

    public void move(int h, int v)
    {
        GlobalVariables.RoundCounter++;
        while (isFree(x + h, y + v)) {
            x+=h;
            y+=v;
        }
    }
    
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

    public void setPosition(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
