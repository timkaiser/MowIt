using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public int x, y;

    //UI
    private Button button_up, button_down, button_left, button_right;
    public Button prefab_button;
    private GameObject ui_canvas;
    private const int buttonsize = 92;
    private Transform transform;
    
    // Use this for initialization
    void Start () {
        transform = this.gameObject.transform;

        //UI
        ui_canvas = GameObject.FindWithTag("canvas");
        button_up = Instantiate(prefab_button);
        button_up.transform.parent = ui_canvas.transform;
        button_up.onClick.AddListener(up);
        button_down = Instantiate(prefab_button);
        button_down.transform.parent = ui_canvas.transform;
        button_down.onClick.AddListener(down);
        button_left = Instantiate(prefab_button);
        button_left.transform.parent = ui_canvas.transform;
        button_left.onClick.AddListener(left);
        button_right = Instantiate(prefab_button);
        button_right.transform.parent = ui_canvas.transform;
        button_right.onClick.AddListener(right);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x != x || transform.position.y != y)
        {
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
            GlobalVariables.get().setMowed((int)xNew + (int)(GlobalVariables.get().level.Length / 2), (int)yNew + (int)(GlobalVariables.get().level[0].Length / 2));
        }
        else
        {
            if (isFree(x, y + 1)) { button_up.gameObject.SetActive(true); }
            if (isFree(x, y - 1)) { button_down.gameObject.SetActive(true); }
            if (isFree(x - 1, y)) { button_left.gameObject.SetActive(true); }
            if (isFree(x + 1, y)) { button_right.gameObject.SetActive(true); }
        }
        button_up.transform.position = new Vector3(x * buttonsize + ui_canvas.transform.position.x, y * buttonsize + ui_canvas.transform.position.y + buttonsize, 0);
        button_down.transform.position = new Vector3(x * buttonsize + ui_canvas.transform.position.x, y * buttonsize + ui_canvas.transform.position.y - buttonsize, 0);
        button_left.transform.position = new Vector3(x * buttonsize + ui_canvas.transform.position.x - buttonsize, y * buttonsize + ui_canvas.transform.position.y, 0);
        button_right.transform.position = new Vector3(x * buttonsize + ui_canvas.transform.position.x + buttonsize, y * buttonsize + ui_canvas.transform.position.y, 0);

        if (GlobalVariables.get().isMowed())
        {
            SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
        }
    }

    public bool isFree(int x, int y)
    {
        x = x + (int)(GlobalVariables.get().level.Length / 2);
        y = y + (int)(GlobalVariables.get().level[0].Length / 2);
        return !(x<0 || y<0 || x>= GlobalVariables.get().level.Length || y >= GlobalVariables.get().level[0].Length) &&
            GlobalVariables.get().level[x][y] != LevelGenerator.TREE;
    }

    public void move(int h, int v)
    {
        while (isFree(x + h, y + v)) {
            x+=h;
            y+=v;
        }
    }
    
    private void up()    { move(0, 1); }
    private void down()  { move(0, -1);  }
    private void left()  { move(-1, 0); }
    private void right() { move(1, 0);  }

    public void setPosition(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
